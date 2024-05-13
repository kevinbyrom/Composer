using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Composer.UI.Controls
{
    public class Panel : ViewBase
    {
        public Panel(UIManager ui) : base(ui)
        {

        }

        public Panel(UIManager ui, IView parent) : base(ui, parent)
        {

        }

        protected override void DrawContent(SpriteBatch spriteBatch)
        {
            var text = String.Format("{0} - {1}", (int)this.ScreenPos.X, (int)this.ScreenPos.Y);

            this.UI.DrawStringCentered(text, this.Width / 2, this.Height / 2, Color.Black);
            //this.UI.DrawString(text, 0, 0, Color.White);
        }
    }

    public static class PanelExtensions
    {
        public static Panel AddPanel(this UIManager ui)
        {
            var panel = new Panel(ui);

            ui.AddView(panel);

            return panel;
        }
    }
}
