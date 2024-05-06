using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Composer.UI
{
    public class UIElement 
    {
        public UIManager UI { get; private set; }
        public UIElement Parent { get; private set; }

        public Vector2 Pos;

        public Vector2 Size;

        protected List<UIElement> subElements = new List<UIElement>();

        public RenderTarget2D RenderTarget { get; private set; }

        public Color Color { get; set; }

        public UIElement(UIManager ui, UIElement parent, int x, int y, int width, int height) 
        {
            this.UI = ui;
            this.Parent = parent;
            this.Pos.X = x;
            this.Pos.Y = y;
            this.Size.X = width;
            this.Size.Y = height;
            this.Color = Color.Transparent;
            this.RenderTarget = new RenderTarget2D(this.UI.Game.GraphicsDevice, width, height);
        }

        public UIElement(UIManager ui, UIElement parent, int x, int y, int width, int height, Color color) : this(ui, parent, x, y, width, height)
        {
            this.Color = color;
        }

        public void AddSubElement(UIElement element)
        {
            subElements.Add(element);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
            // Ensure the sub elements update their render target
            
            foreach (var subElement in subElements)
                subElement.Draw(spriteBatch);


            // Draw this element's content to the render target

            this.UI.PushRenderTarget(this.RenderTarget);
            this.UI.Clear(this.Color);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            DrawContent(spriteBatch);


            // Then draw the sub elements to the render target
           
            foreach (var subElement in subElements)
                spriteBatch.Draw(subElement.RenderTarget, subElement.Pos, Color.White);

            spriteBatch.End();


            this.UI.PopRenderTarget();
  
        }

        protected virtual void DrawContent(SpriteBatch spriteBatch) { }
      
    }
}
