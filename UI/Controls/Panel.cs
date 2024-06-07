using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Composer.UI.Controls
{
    public class Panel : UIElementBase
    {
        public Panel(UIManager ui) : base(ui)
        {

        }

        public Panel(UIManager ui, IUIElement parent) : base(ui, parent)
        {

        }
    }

    public static class PanelExtensions
    {
        public static Panel AddPanel(this UIManager ui)
        {
            var panel = new Panel(ui);

            ui.AddElement(panel);

            return panel;
        }
    }
}
