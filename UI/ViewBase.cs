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
    public abstract class ViewBase : IView
    {
        public UIManager UI { get; set; }
        public IView Parent { get; set; }

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

        private List<IView> subViews = new List<IView>();
        public IEnumerable<IView> SubViews 
        { 
            get 
            {
                return this.subViews;
            }
        }

        private bool hovering = false;

        public RenderTarget2D RenderTarget { get; private set; }

        public Color Color { get; set; }

        public ViewBase(UIManager ui) : this(ui, null)
        {
        }

        public ViewBase(UIManager ui, IView parent)
        {
            this.UI = ui;
            this.Parent = parent;
            this.Color = Color.Transparent;
        }

        public void AddView(IView view)
        {
            this.subViews.Add(view);
        }

        public virtual void Update(GameTime time)
        {

        }

        public virtual void Draw(GameTime time, SpriteBatch spriteBatch)
        {

            // Ensure the sub views update their render target

            foreach (var view in this.SubViews)
                view.Draw(time, spriteBatch);


            // Draw this element's content to the render target

            this.UI.PushRenderTarget(this.RenderTarget);
            this.UI.Clear(this.hovering ? Color.White : this.Color);

            DrawContent(spriteBatch);


            // Then draw the sub views to the render target

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            foreach (var view in this.SubViews)
                spriteBatch.Draw(view.RenderTarget, view.Pos.ToVector2(), Color.White);

            spriteBatch.End();

            this.UI.PopRenderTarget();

        }

        protected virtual void DrawContent(SpriteBatch spriteBatch) { }

        public void UpdateScreenPos()
        {
            if (this.Parent != null)
                this.screenPos = this.Parent.ScreenPos + this.Pos;
            else
                this.screenPos = this.Pos;

            foreach (var view in this.SubViews)
                view.UpdateScreenPos();
        }

        public virtual void MouseEnter(MouseState state)
        {
            this.hovering = true;
        }

        public virtual void MouseMove(MouseState state)
        {
        }

        public virtual void MouseExit(MouseState state)
        {
            this.hovering = false;
        }
    }
}
