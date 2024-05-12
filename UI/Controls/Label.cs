using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composer.UI.Controls
{
    public class Label : ViewBase
    {
        public string Text { get; set; }

        public Label(UIManager ui) : base(ui)
        {

        }

        protected override void DrawContent(SpriteBatch spriteBatch)
        {
            base.DrawContent(spriteBatch);
        }
    }
}
