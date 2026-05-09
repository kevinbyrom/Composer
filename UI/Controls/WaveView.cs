using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Monotaur;
using Monotaur.Graphics;
using Monotaur.Systems;
using Monotaur.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Composer.UI.Controls
{
    public class WaveView : UIElement
    {
        public SignalBuffer SignalBuffer { get; set; }

        public WaveView(GameEntity parent) : base(parent)
        {

        }

        protected override void OnDrawContent(IRenderer renderer)
        {
            var signals = this.SignalBuffer.GetAll();

            int halfHeight = this.Height / 2;

            renderer.DrawLine(0, halfHeight, this.Width, halfHeight, Color.White);

            for (int x = 0; x < this.Width; x++)
            {
                int ylen = (int)(signals[x].Value * (this.Height / 2));

                renderer.DrawLine(x, halfHeight, x, halfHeight - ylen, Color.White);
            }

            renderer.DrawString(String.Format("{0:0.00}", signals.Last().Value), 10, 10, Color.White);
        }

    }

    public static class WaveViewExtensions
    {
        public static WaveView WaveView(this UIManager ui)
        {
            var view = new WaveView(null);

            ui.AddElement(view);

            return view;
        }

        public static WaveView SetSignalBuffer(this WaveView view,  SignalBuffer buffer)
        {
            view.SignalBuffer = buffer;
            return view;
        }
    }
}
