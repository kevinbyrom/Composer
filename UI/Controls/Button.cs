using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composer.UI.Controls
{
    public class Button : ViewBase
    {
        public string Text { get; set; }

        public Button(UIManager ui) : base(ui)
        {
        }

        public Button(UIManager ui, IView parent) : base(ui, parent)
        {
        }
    }

    public static class ButtonExtensions
    {
        public static Button AddButton(this UIManager ui)
        {
            var view = new Button(ui);

            ui.AddView(view);

            return view;
        }

        public static Button SetText(this Button view, string text)
        {
            view.Text = text;
            return view;
        }
    }
}
