using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
