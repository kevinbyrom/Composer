using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composer.Nodes
{
    public abstract class SignalNodeBase : ISignalNode
    {
        public Signal Signal { get; protected set; }

        public abstract void Update(double time);

        public Signal GetSignal(double time)
        {
            return this.Signal;
        }
    }
}
