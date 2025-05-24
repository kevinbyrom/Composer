using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;


namespace Composer.UI.Controls
{
    public class DebugPanel : UIElementBase
    {
        private bool hovering = false;
        private bool moving = false;
        private Point movingOffset;


        public DebugPanel(UIManager ui) : base(ui)
        {
        }

        protected override void OnDrawContent(SpriteBatch spriteBatch) 
        {
            if (this.hovering)
                this.UI.Clear(Color.White);
            
            var text = String.Format("{0} - {1}", (int)this.ScreenPos.X, (int)this.ScreenPos.Y);
            
            this.UI.DrawStringCentered(text, this.Width / 2, this.Height / 2, Color.Black);
        }

        public override void OnMouseEnter(MouseState state)
        {
            this.hovering = true;
        }

        public override void OnMouseMove(MouseState state)
        {
            if (!this.moving)
            {
                if (state.LeftButton == ButtonState.Pressed)
                {
                    this.UI.SetMouseCapture(this);
                    this.movingOffset = state.Position - this.ScreenPos;
                    this.moving = true;
                }
            }
            else
            {
                if (state.LeftButton != ButtonState.Pressed)
                {
                    this.UI.ReleaseMouseCapture();
                    this.moving = false;
                }
            }

            if (this.moving)
                this.ScreenPos = state.Position - movingOffset;
        }

        public override void OnMouseExit(MouseState state)
        {
            this.hovering = false;
        }
    }

    public static class DebugPanelExtensions
    {
        public static DebugPanel DebugPanel(this UIManager ui)
        {
            var button = new DebugPanel(ui);

            ui.AddElement(button);

            return button;
        }
    }
}
