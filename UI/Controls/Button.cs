using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composer.UI.Controls
{
    public class Button : UIElementBase
    {
        public string Text { get; set; }

        public Button(UIManager ui) : base(ui)
        {
        }
    }

    public static class ButtonExtensions
    {
        public static Button AddButton(this UIManager ui)
        {
            var button = new Button(ui);

            ui.AddElement(button);

            return button;
        }

        public static Button SetText(this Button button, string text)
        {
            button.Text = text;
            return button;
        }
    }
}
