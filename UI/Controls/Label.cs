using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Composer.UI.Controls
{
    public class LabelView : UIElementBase
    {
        public string Text { get; set; }

        public LabelView(UIManager ui) : base(ui)
        {
        }

        protected override void OnDrawContent(SpriteBatch spriteBatch)
        {
            //this.UI.DrawStringCentered(Text, this.Width / 2, this.Height / 2, Color.Transparent);

            this.UI.DrawStringCentered(String.Format("{} - {}", (int)this.ScreenPos.X, (int)this.ScreenPos.Y), this.Width / 2, this.Height / 2, Color.Transparent);
        }
    }

    public static class LabelViewExtensions
    {
        public static LabelView Label(this UIManager ui)
        {
            var label = new LabelView(ui);

            ui.AddElement(label);

            return label;
        }

        public static LabelView Text(this LabelView label, string text)
        {
            label.Text = text;
            return label;
        }
    }
}
