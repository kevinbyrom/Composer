using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Composer.UI.Controls
{
    public class LabelView : ViewBase
    {
        public string Text { get; set; }

        public LabelView(UIManager ui) : base(ui)
        {
        }

        protected override void DrawContent(SpriteBatch spriteBatch)
        {
            //this.UI.DrawStringCentered(Text, this.Width / 2, this.Height / 2, Color.Transparent);

            this.UI.DrawStringCentered(String.Format("{} - {}", (int)this.ScreenPos.X, (int)this.ScreenPos.Y), this.Width / 2, this.Height / 2, Color.Transparent);
        }
    }

    public static class LabelViewExtensions
    {
        public static LabelView AddLabel(this UIManager ui)
        {
            var view = new LabelView(ui);

            ui.AddView(view);

            return view;
        }

        public static LabelView SetText(this LabelView view, string text)
        {
            view.Text = text;
            return view;
        }
    }
}
