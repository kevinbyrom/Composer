using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Composer.UI
{
   /* internal class UIElement 
    {
        public UIManager UI { get; private set; }

        public Vector2 ScreenPos;

        public Vector2 Size;

        protected List<UIElement> subElements = new List<UIElement>();

        protected RenderTarget2D renderTarget;

        protected Texture2D texture;

        public Color Color { get; set; }

        public UIElement(UIManager ui, int x, int y, int width, int height) 
        {
            this.UI = ui;
            this.ScreenPos.X = x;
            this.ScreenPos.Y = y;
            this.Size.X = width;
            this.Size.Y = height;
            this.Color = Color.Transparent;
            this.renderTarget = new RenderTarget2D(this.UI.Game.GraphicsDevice, texture.Width, texture.Height);
        }

        public UIElement(UIManager ui, int x, int y, int width, int height, Color color) : this(UIManager ui, x, y, width, height)
        {
            this.Color = color;
        }

        public void AddSubElement(UIElement element)
        {
            subElements.Add(element);
        }

        public virtual void Draw(SpriteBatch spriteBatch, double time)
        {
            this.UI.PushRenderTarget(this.renderTarget);
            this.UI.Clear(this.Color);


            // Draw to the render target
            spriteBatch.Begin();
            spriteBatch.Draw(texture, this.ScreenPos, Color.White);
            foreach (var subElement in subElements)
            {
                subElement.Draw(spriteBatch, time);
            }
            spriteBatch.End();

            this.UI.Game.GraphicsDevice.SetRenderTarget(null);

            // Draw the render target to the screen
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(renderTarget, this.ScreenPos, Color.White);
            spriteBatch.End();

        }
    }*/
}
