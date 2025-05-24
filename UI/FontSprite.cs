using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Composer.UI
{
    public class FontSprite
    {
        public Texture2D Texture { get; set; }
        public int GlyphWidth {  get; private set; }
        public int GlyphHeight { get; private set; }
        
        public int GlyphWidthScaled
        {
            get
            {
                return (int)(this.GlyphWidth * this.Scale);
            }
        }

        public int GlphyHeightScaled
        {
            get
            {
                return (int)(this.GlyphHeight * this.Scale);
            }
        }

        public float Scale { get; private set; }

        private Rectangle[] glyphRects;


        public FontSprite(Texture2D texture, int glyphWidth, int glyphHeight, int numX, int numY) 
        {
            this.Scale = 1;
            //this.Scale = 2;
            this.Texture = texture;
            this.GlyphWidth = glyphWidth;
            this.GlyphHeight = glyphHeight;

            this.glyphRects = new Rectangle[numX * numY];

            // Parse the glyph rects

            for (int y = 0; y < numY; y++)
            {
                for (int x = 0; x < numX; x++)
                {
                    var rect = new Rectangle(x * this.GlyphWidth + (x + 1), y * this.GlyphHeight + (y + 1), this.GlyphWidth, this.GlyphHeight);

                    this.glyphRects[(y * numX) + x] = rect;
                }
            }
        }


        public void DrawString(SpriteBatch spriteBatch, string text, int x, int y, Color color)
        {
            foreach (char c in text)
            {

                // Get rect of the character

                int pos = 0;

                if (c >= 32 || c < 126)
                    pos = c - 32;

                // Draw the character

                var destRect = new Rectangle(x, y, this.GlyphWidthScaled, this.GlphyHeightScaled);

                spriteBatch.Draw(this.Texture, destRect, this.glyphRects[pos], color);

                x += (int)(this.GlyphWidth * this.Scale);
            }
        }


        public Vector2 MeasureString(string text)
        {
            return new Vector2(text.Length * this.GlyphWidthScaled, this.GlphyHeightScaled); 
        }
    }
}
