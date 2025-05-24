using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;

namespace Composer.UI.Controls
{
    public class Slider : UIElementBase
    {
        double Min { get; set; } = 0.0;
        double Max { get; set; } = 1.0;
        double Val { get; set; } = 0.0;
        double Percent
        {
            get
            {
                return (Max - Min) == 0.0 ? 0.0 : Val / (Max - Min);
            }
        }
        
        public Slider(UIManager ui) : base(ui)
        {
            this.Color = Color.Black;
        }

        protected override void OnDrawContent(SpriteBatch spriteBatch)
        {            
            var text = String.Format("{0:0.00}", this.Val);

            this.UI.DrawFilledRectangle(0, 0, (int)(this.Width * this.Percent), this.Height, Color.Aqua);

            this.UI.DrawStringCentered(text, this.Width / 2, this.Height / 2, Color.White);
        }

        public override void OnMouseMove(MouseState state)
        {
            if (state.LeftButton == ButtonState.Pressed)
            {
                this.UI.SetMouseCapture(this);

                var pct = (double)(state.X - this.ScreenPos.X) / this.Size.X;

                pct = Math.Min(pct, 1.0);
                pct = Math.Max(pct, 0.0);

                this.Val = pct * Max; 
            }
            else
            {
                this.UI.ReleaseMouseCapture(this);
            }
        }

    }

    public static class SliderExtensions
    {
        public static Slider Slider(this UIManager ui)
        {
            var slider = new Slider(ui);

            ui.AddElement(slider);

            return slider;
        }
    }
}
