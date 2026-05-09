using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monotaur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monotaur.UI;
using Monotaur.Graphics;
using Monotaur.Systems;


namespace Composer.UI.Controls
{
    public class Panel : UIElement
    {
        public Panel(GameEntity parent) : base(parent)
        {

        }
    }

    public static class PanelExtensions
    {
        public static Panel Panel(this UIManager ui)
        {
            var panel = new Panel(null);

            ui.AddElement(panel);

            return panel;
        }
    }
}
