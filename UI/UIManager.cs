using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Composer.UI
{
    public class UIManager : IViewContainer
    {
        public Game Game { get; private set; }
        public FontSprite DefaultFontSprite { get; set; }
        
        public SpriteBatch SpriteBatch => Game.Services.GetService<SpriteBatch>();

        private IView captured;
        public IView Captured
        {
            get
            {
                return this.captured;
            }
        }

        private Stack<RenderTarget2D> renderTargets = new Stack<RenderTarget2D>();
        private RenderTarget2D currRenderTarget = null;
        private List<IView> views = new List<IView>();
        
        public UIManager(Game game) 
        {
            this.Game = game;
        }

        public void Update(GameTime time)
        {
            foreach (var view in this.views)
                view.Update(time);
        }

        public void Draw(GameTime time)
        {
            
            // Render the sub contents of each view

            foreach (var view in this.views)
                view.Draw(time, this.SpriteBatch);


            // Draw the views to the screen

            this.SpriteBatch.Begin();

            foreach (var view in this.views)
                this.SpriteBatch.Draw(view.RenderTarget, view.Pos, Color.White);

            this.SpriteBatch.End();


            // Draw test text

            DrawStringCentered("z{|}", 50, 50, Color.White);
            
        }

        public void DrawString(string text, int x, int y, Color color)
        {
            this.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            this.DefaultFontSprite.DrawString(this.SpriteBatch, text, x, y, color);

            this.SpriteBatch.End();
        }

        public void DrawStringCentered(string text, int x, int y, Color color)
        {
            var size = this.DefaultFontSprite.MeasureString(text);

            DrawString(text, x - ((int)size.X / 2), y - ((int)size.Y / 2), color);
        }

        public void PushRenderTarget(RenderTarget2D renderTarget)
        {
            this.Game.GraphicsDevice.SetRenderTarget(renderTarget);

            if (this.currRenderTarget == null)
                this.renderTargets.Push(this.currRenderTarget);

            this.currRenderTarget= renderTarget;
        }

        public void PopRenderTarget()
        {
            this.Game.GraphicsDevice.SetRenderTarget(this.renderTargets.Pop());

            if (this.renderTargets.Count > 0)
                this.currRenderTarget = this.renderTargets.Peek();
            else
                this.currRenderTarget = null;
        }

        public void ClearRenderTargets()
        { 
            this.renderTargets.Clear();
            this.currRenderTarget = null;
            this.Game.GraphicsDevice.SetRenderTarget(null);
        }

        public void Clear(Color color)
        {
            this.Game.GraphicsDevice.Clear(color);
        }

        public void AddView(IView view)
        {
            this.views.Add(view);
        }


        public void SetCapture(IView view)
        {
            this.captured = view;
        }

        public void ReleaseCapture()
        { 
            this.captured = null; 
        }

        public bool ProcessInput(InputState inputState)
        {
            if (this.captured != null)
            {
                this.captured.ProcessInput(inputState);
            }
            else
            {
                foreach (var view in this.views)
                {
                    if (view.ProcessInput(inputState))
                        return true;
                }
            }

            return false;
        }
    }
}
