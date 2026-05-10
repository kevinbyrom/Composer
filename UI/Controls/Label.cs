using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monotaur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monotaur.UI;
using Monotaur.Systems;
using Monotaur.Graphics;


namespace Composer.UI.Controls
{
    public class LabelView : UIElement
    {
        public string Text { get; set; }


        public LabelView(IUIManager ui, GameEntity parent) : base(ui, parent)
        {
        }

        public LabelView(GameEntity parent) : base(parent)
        {
        }


        protected override void OnDrawContent(IRenderer renderer)
        {
            //this.UI.DrawStringCentered(Text, this.Width / 2, this.Height / 2, Color.Transparent);

            renderer.DrawStringCentered(String.Format("{} - {}", (int)this.ScreenPos.X, (int)this.ScreenPos.Y), this.Width / 2, this.Height / 2, Color.Transparent);
        }
    }

    public static class LabelViewExtensions
    {
        public static LabelView Label(this IUIManager ui)
        {
            var label = new LabelView(ui, null);

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
