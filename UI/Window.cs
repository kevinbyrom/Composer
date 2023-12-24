using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Composer.UI
{
    internal class Window : DrawableGameComponent
    {
        private RenderTarget2D renderTarget;

        public int Width {  get; private set; }
        public int Height { get; private set; }

        public Color Background { get; set; }

        public Window(Game game, int width, int height) : this(game, width, height, Color.Transparent)
        {

        }

        public Window(Game game, int width, int height, Color background) : base(game)
        {
            renderTarget = new RenderTarget2D(game.GraphicsDevice, width, height);
            Width = width; 
            Height = height;
            Background = background;
        }

        public override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.SetRenderTarget(this.renderTarget);
            this.GraphicsDevice.Clear(this.Background);
            
            // Draw the content
            
            // Draw the sub windows

        }
    }
}
