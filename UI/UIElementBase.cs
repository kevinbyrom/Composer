using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Composer.UI
{
    public abstract class UIElementBase : IUIElement
    {
        public UIManager UI { get; set; }
        public IUIElement Parent { get; set; }

        private List<IUIElement> elements = new List<IUIElement>();
        public IEnumerable<IUIElement> Elements => elements;

        private Point pos;

        public Point Pos
        {
            get
            {
                return this.pos;
            }
            set
            {
                this.pos = value;
                UpdateScreenPos();
            }
        }

        private Point screenPos;

        public Point ScreenPos
        {
            get
            {
                return this.screenPos;
            }
            set
            {
                if (this.Parent != null)
                    this.pos = value - this.Parent.ScreenPos;
                else
                    this.pos = value;

                UpdateScreenPos();
            }
        }

        public Rectangle ScreenRect
        {
            get
            {
                return new Rectangle(this.ScreenPos, this.Size);
            }
        }

        private Point size;
        public Point Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
                this.RenderTarget = new RenderTarget2D(this.UI.Game.GraphicsDevice, this.size.X, this.size.Y);
            }
        }

        public int Width
        {
            get
            {
                return this.size.X;
            }
        }

        public int Height
        {
            get
            {
                return this.size.Y;
            }
        }
        
        public RenderTarget2D RenderTarget { get; private set; }

        public Color Color { get; set; }

        public UIElementBase(UIManager ui) : this(ui, null)
        {
        }

        public UIElementBase(UIManager ui, IUIElement parent)
        {
            this.UI = ui;
            this.Parent = parent;
            this.Color = Color.Transparent;
        }

        public void AddElement(IUIElement child)
        {
            this.elements.Add(child);
        }

        public virtual void Update(GameTime time)
        {

        }

        public virtual void Draw(GameTime time, SpriteBatch spriteBatch)
        {

            // Ensure the sub elements update their render target

            foreach (var child in this.elements)
                child.Draw(time, spriteBatch);


            // Draw this element's content to the render target

            this.UI.PushRenderTarget(this.RenderTarget);
            this.UI.Clear(this.Color);

            OnDrawContent(spriteBatch);


            // Then draw the sub elements to the render target

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            foreach (var child in this.elements)
                spriteBatch.Draw(child.RenderTarget, child.Pos.ToVector2(), Color.White);

            spriteBatch.End();

            this.UI.PopRenderTarget();

        }


        public void UpdateScreenPos()
        {
            if (this.Parent != null)
                this.screenPos = this.Parent.ScreenPos + this.Pos;
            else
                this.screenPos = this.Pos;

            foreach (var element in this.Elements)
                element.UpdateScreenPos();
        }

        protected virtual void OnDrawContent(SpriteBatch spriteBatch) { }

        public virtual void OnMouseEnter(MouseState state) { }
        
        public virtual void OnMouseMove(MouseState state) { }
        
        public virtual void OnMouseExit(MouseState state) { }
    }
}
