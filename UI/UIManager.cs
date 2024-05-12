using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Composer.UI
{
    public class UIManager
    {
        public Game Game { get; private set; }
        private UIElement captured;
        public UIElement Captured
        {
            get
            {
                return this.captured;
            }
        }

        private Stack<RenderTarget2D> renderTargets = new Stack<RenderTarget2D>();
        private RenderTarget2D currRenderTarget = null;
        private List<UIElement> uIElements = new List<UIElement>(); 

        public UIManager(Game game) 
        {
            this.Game = game;
        }

        public void Update(double time)
        {
            foreach (var element in this.uIElements)
                element.Update(time);
        }

        public void Draw(GameTime time)
        {
            var spriteBatch = Game.Services.GetService<SpriteBatch>();


            // Render the sub contents of each UI element

            foreach (var uiElement in this.uIElements)
                uiElement.Draw(spriteBatch);


            // Draw the UI elements to the screen

            spriteBatch.Begin();

            foreach (var uiElement in this.uIElements)
                spriteBatch.Draw(uiElement.RenderTarget, uiElement.Pos, Color.White);

            spriteBatch.End();
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

        public void AddElement(UIElement uiElement)
        {
            this.uIElements.Add(uiElement);
        }


        public void SetCapture(UIElement element)
        {
            this.captured = element;
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
                foreach (var element in this.uIElements)
                {
                    if (element.ProcessInput(inputState))
                        return true;
                }
            }

            return false;
        }
    }
}
