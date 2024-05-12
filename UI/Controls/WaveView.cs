using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;
using Microsoft.Xna.Framework;


namespace Composer.UI.Controls
{
    public class WaveView : ViewBase
    {
        public SignalBuffer SignalBuffer { get; set; }

        public WaveView(UIManager ui) : base(ui)
        {

        }

        public WaveView(UIManager ui, IView parent) : base(ui, parent)
        {

        }

        protected override void DrawContent(SpriteBatch spriteBatch) 
        {
            var signals = this.SignalBuffer.GetAll();

            int halfHeight = this.Height / 2;

            spriteBatch.DrawLine(0, halfHeight, this.Width, halfHeight, Color.White);

            for (int x = 0; x < this.Width; x++)
            {
                int ylen = (int)(signals[x].Value * (this.Height / 2));

                spriteBatch.DrawLine(x, halfHeight, x, halfHeight - ylen, Color.White);
            }


            //this.UI.DrawString("Here is a test!", 0, 0, Color.White);
        }

    }

    public static class WaveViewExtensions
    {
        public static WaveView AddWaveView(this UIManager ui)
        {
            var view = new WaveView(ui);

            ui.AddView(view);

            return view;
        }

        public static WaveView SetSignalBuffer(this WaveView view,  SignalBuffer buffer)
        {
            view.SignalBuffer = buffer;
            return view;
        }
    }
}
