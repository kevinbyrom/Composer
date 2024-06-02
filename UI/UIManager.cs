using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Composer.UI
{
    public struct MouseTracking
    {
        public IView Captured;
        public IView LastTarget;
    }

    public class UIManager : IViewContainer
    {
        public Game Game { get; private set; }
        public FontSprite DefaultFontSprite { get; set; }
        public SpriteBatch SpriteBatch => Game.Services.GetService<SpriteBatch>();

        private IView mouseCaptured;

        public MouseTracking MouseTracking;

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
            ProcessInput();

            // Update the views

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
                this.SpriteBatch.Draw(view.RenderTarget, view.Pos.ToVector2(), Color.White);

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
        public void SetMouseCapture(IView view)
        {
            this.MouseTracking.Captured = view;
        }

        /// <summary>
        /// Releases the input capture from all views
        /// </summary>
        public void ReleaseMouseCapture()
        { 
            this.MouseTracking.Captured = null; 
        }

        public void ProcessInput()
        {
            ProcessMouseInput();
        }


        private void ProcessMouseInput()
        {
            var mouseState = Mouse.GetState();

            // Determine which control the pointer is over (or used captured, if set)

            IView target = this.MouseTracking.Captured ?? this.views.FindViewAtScreenPos(mouseState.Position);


            // Check if we have exited last target

            if (this.MouseTracking.LastTarget != null && this.MouseTracking.LastTarget != target)
                this.MouseTracking.LastTarget.MouseExit(mouseState);


            // Handle mouse for current target

            if (target != null)
            {
                if (this.MouseTracking.LastTarget != target)
                    target.MouseEnter(mouseState);

                target.MouseMove(mouseState);
            }

            this.MouseTracking.LastTarget = target;
        }

        #endregion

    }
}
