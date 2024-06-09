using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace Composer.UI.Controls
{
    public class Slider : UIElementBase
    {
        double Min { get; set; } = 0.0;
        double Max { get; set; } = 1.0;
        double Val { get; set; } = 0.0;

        public Slider(UIManager ui) : base(ui)
        {
            this.Color = Color.Black;
        }

        protected override void OnDrawContent(SpriteBatch spriteBatch)
        {            
            var text = String.Format("{0:0.00}", this.Val);

            this.UI.DrawStringCentered(text, this.Width / 2, this.Height / 2, Color.White);
        }

        public override void OnMouseMove(MouseState state)
        {
            if (state.LeftButton == ButtonState.Pressed)
            {
                var pct = (double)(state.X - this.ScreenPos.X) / this.Size.X;

                this.Val = pct * Max; 
            }
        }

    }

    public static class SliderExtensions
    {
        public static Slider AddSlider(this UIManager ui)
        {
            var slider = new Slider(ui);

            ui.AddElement(slider);

            return slider;
        }
    }
}
