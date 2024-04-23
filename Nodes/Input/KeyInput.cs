using Composer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Composer.Nodes.Input
{
    public class KeyNode : SignalNodeBase
    {
        public Keys Key { get; set; }

        public KeyNode() : base()
        {
        }

        public KeyNode(Keys key) : this()
        {
            this.Key = key;
        }

        public override void Update(double time)
        {
            bool isKeyDown = Keyboard.GetState().IsKeyDown(this.Key);

            this.Signal = isKeyDown ? Signal.Max : Signal.Zero;
        }
    }
}
