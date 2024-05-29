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


        /// <summary>
        /// Update handler for the UI
        /// </summary>
        /// <param name="time"></param>
        public void Update(GameTime time)
        {
            foreach (var view in this.views)
                view.Update(time);
        }


        /// <summary>
        /// Draws the UI and all the views
        /// </summary>
        /// <param name="time"></param>
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
            
        }


        /// <summary>
        /// Adds a sub view to the UI
        /// </summary>
        /// <param name="view"></param>
        public void AddView(IView view)
        {
            this.views.Add(view);
        }

        #region Drawing Routines

        /// <summary>
        /// Clears the UI to the specified color
        /// </summary>
        /// <param name="color"></param>
        public void Clear(Color color)
        {
            this.Game.GraphicsDevice.Clear(color);
        }


        /// <summary>
        /// Draws text at a given local
        /// </summary>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void DrawString(string text, int x, int y, Color color)
        {
            this.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            this.DefaultFontSprite.DrawString(this.SpriteBatch, text, x, y, color);

            this.SpriteBatch.End();
        }


        /// <summary>
        /// Draws text centered around the specified location
        /// </summary>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void DrawStringCentered(string text, int x, int y, Color color)
        {
            var size = this.DefaultFontSprite.MeasureString(text);

            DrawString(text, x - ((int)size.X / 2), y - ((int)size.Y / 2), color);
        }

        #endregion


        #region Render Target Routines

        /// <summary>
        /// Pushes a new render target to the stack
        /// </summary>
        /// <param name="renderTarget"></param>
        public void PushRenderTarget(RenderTarget2D renderTarget)
        {
            this.Game.GraphicsDevice.SetRenderTarget(renderTarget);

            if (this.currRenderTarget == null)
                this.renderTargets.Push(this.currRenderTarget);

            this.currRenderTarget= renderTarget;
        }


        /// <summary>
        /// Pops a render target from the stack
        /// </summary>
        public void PopRenderTarget()
        {
            this.Game.GraphicsDevice.SetRenderTarget(this.renderTargets.Pop());

            if (this.renderTargets.Count > 0)
                this.currRenderTarget = this.renderTargets.Peek();
            else
                this.currRenderTarget = null;
        }


        /// <summary>
        /// Clears all render targets from the stack
        /// </summary>
        public void ClearRenderTargets()
        {
            this.renderTargets.Clear();
            this.currRenderTarget = null;
            this.Game.GraphicsDevice.SetRenderTarget(null);
        }

        #endregion

        
        #region Input Handling Routines

        /// <summary>
        /// Sets the input capture to a specified view
        /// </summary>
        /// <param name="view"></param>
        public void SetCapture(IView view)
        {
            this.captured = view;
        }

        /// <summary>
        /// Releases the input capture from all views
        /// </summary>
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

        #endregion

    }
}
