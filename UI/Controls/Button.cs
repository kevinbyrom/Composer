using Monotaur;
using Monotaur.Systems;
using Monotaur.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Composer.UI.Controls
{
    public class Button : UIElement
    {
        public string Text { get; set; }

        public Button(GameEntity parent) : base(parent)
        {
        }
    }

    public static class ButtonExtensions
    {
        public static Button Button(this UIManager ui)
        {
            var button = new Button(null);

            ui.AddElement(button);

            return button;
        }

        public static Button Text(this Button button, string text)
        {
            button.Text = text;
            return button;
        }
    }
}
