using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace Composer.UI
{
    public struct MouseTracking
    {
        public IUIElement Captured;
        public IUIElement LastTarget;
    }

    public class UIManager : IUIElementContainer
    {
        public Game Game { get; private set; }
        public FontSprite DefaultFontSprite { get; set; }
        public SpriteBatch SpriteBatch => Game.Services.GetService<SpriteBatch>();

        private IUIElement mouseCaptured;

        public MouseTracking MouseTracking;

        private Stack<RenderTarget2D> renderTargets = new Stack<RenderTarget2D>();
        private RenderTarget2D currRenderTarget = null;
        private List<IUIElement> elements = new List<IUIElement>();
        
        public IEnumerable<IUIElement> Elements => elements;

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

            // Update the elements

            foreach (var element in this.elements)
                element.Update(time);
        }


        /// <summary>
        /// Draws the UI and all the views
        /// </summary>
        /// <param name="time"></param>
        public void Draw(GameTime time)
        {
            
            // Render the sub contents of each element

            foreach (var element in this.elements)
                element.Draw(time, this.SpriteBatch);


            // Draw the views to the screen

            this.SpriteBatch.Begin();

            foreach (var element in this.elements)
                this.SpriteBatch.Draw(element.RenderTarget, element.Pos.ToVector2(), Color.White);

            this.SpriteBatch.End();
            
        }


        /// <summary>
        /// Adds an element to the UI
        /// </summary>
        /// <param name="element"></param>
        public void AddElement(IUIElement element)
        {
            this.elements.Add(element);
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
            //this.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            this.DefaultFontSprite.DrawString(this.SpriteBatch, text, x, y, color);

            //this.SpriteBatch.End();
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


        /// <summary>
        /// Draws a line
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="color"></param>
        public void DrawLine(Point p1, Point p2, Color color)
        {
            this.DrawLine(p1.X, p1.Y, p2.X, p2.Y, color);
        }


        /// <summary>
        /// Draws a line 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="color"></param>
        public void DrawLine(int x1, int y1, int x2, int y2, Color color)
        {
            this.SpriteBatch.DrawLine(x1, y1, x2, y2, color);
        }


        /// <summary>
        /// Draws a rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        /// <param name="layerDepth"></param>
        public void DrawRectangle(Rectangle rectangle, Color color, float thickness = 1f, float layerDepth = 0f)
        {
            this.DrawRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, color, thickness, layerDepth);
        }


        /// <summary>
        /// Draws a rectangle
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        /// <param name="layerDepth"></param>
        public void DrawRectangle(Point location, Size2 size, Color color, float thickness = 1f, float layerDepth = 0f)
        {
            this.DrawRectangle(location.X, location.Y, (int)size.Width, (int)size.Height, color, thickness, layerDepth);
        }


        /// <summary>
        /// Draws a rectangle
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        /// <param name="layerDepth"></param>
        public void DrawRectangle(int x, int y, int width, int height, Color color, float thickness = 1f, float layerDepth = 0f)
        {
            this.SpriteBatch.DrawRectangle(x, y, width, height, color, thickness, layerDepth);
        }


        /// <summary>
        /// Draws a rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="color"></param>
        /// <param name="layerDepth"></param>
        public void DrawFilledRectangle(Rectangle rectangle, Color color, float layerDepth = 0f)
        {
            this.DrawFilledRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, color, layerDepth);
        }


        /// <summary>
        /// Draws a rectangle
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <param name="layerDepth"></param>
        public void DrawFilledRectangle(Point location, Size2 size, Color color, float layerDepth = 0f)
        {
            this.DrawFilledRectangle(location.X, location.Y, (int)size.Width, (int)size.Height, color, layerDepth);
        }


        /// <summary>
        /// Draws a rectangle
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <param name="layerDepth"></param>
        public void DrawFilledRectangle(int x, int y, int width, int height, Color color, float layerDepth = 0f)
        {
            this.SpriteBatch.FillRectangle(x, y, width, height, color, layerDepth);
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
        /// Sets the input capture to a specified element
        /// </summary>
        /// <param name="element"></param>
        public void SetMouseCapture(IUIElement element)
        {
            this.MouseTracking.Captured = element;
        }

        /// <summary>
        /// Releases the input capture from all views
        /// </summary>
        public void ReleaseMouseCapture(IUIElement caller = null)
        { 
            if (caller == null || this.MouseTracking.Captured == caller)
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

            IUIElement target = this.MouseTracking.Captured ?? this.elements.FindElementAtScreenPos(mouseState.Position);


            // Check if we have exited last target

            if (this.MouseTracking.LastTarget != null && this.MouseTracking.LastTarget != target)
                this.MouseTracking.LastTarget.OnMouseExit(mouseState);


            // Handle mouse for current target

            if (target != null)
            {
                if (this.MouseTracking.LastTarget != target)
                    target.OnMouseEnter(mouseState);

                target.OnMouseMove(mouseState);
            }

            this.MouseTracking.LastTarget = target;
        }

        #endregion

    }
}
