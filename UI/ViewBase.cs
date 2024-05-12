using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Composer.UI
{
    public abstract class ViewBase : IView 
    {
        public UIManager UI { get; set; }
        public IView Parent { get; set; }
        public Vector2 Pos { get; set; }

        private Vector2 size;
        public Vector2 Size
        {
            get
            {
                return size;
            }
            set
            {
                this.size = value;
                this.RenderTarget = new RenderTarget2D(this.UI.Game.GraphicsDevice, (int)this.size.X, (int)this.size.Y);
            }
        }

        public int Width
        {
            get
            {
                return (int)this.size.X;
            }
        }

        public int Height
        {
            get
            {
                return (int)this.size.Y;
            }
        }

        protected List<IView> subViews = new List<IView>();

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
            subViews.Add(view);
        }

        public virtual void Update(GameTime time)
        {

        }

        public virtual void Draw(GameTime time, SpriteBatch spriteBatch)
        {
            
            // Ensure the sub views update their render target
            
            foreach (var view in this.subViews)
                view.Draw(time, spriteBatch);


            // Draw this element's content to the render target

            this.UI.PushRenderTarget(this.RenderTarget);
            this.UI.Clear(this.Color);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            DrawContent(spriteBatch);


            // Then draw the sub views to the render target
           
            foreach (var view in this.subViews)
                spriteBatch.Draw(view.RenderTarget, view.Pos, Color.White);

            spriteBatch.End();


            this.UI.PopRenderTarget();
  
        }

        protected virtual void DrawContent(SpriteBatch spriteBatch) { }
      
        public virtual bool ProcessInput(InputState inputState)
        {
            foreach (var view in this.subViews)
            {
                if (view.ProcessInput(inputState))
                    return true;
            }

            return false;
        }
    }
}
