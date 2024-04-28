using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Composer.Nodes;

namespace Composer.Nodes.Sources
{
    public class PredicatedConstantNode : SignalNodeBase
    {
        Signal ConstValue { get; set; }
        Func<bool> Predicate { get; set; }

        public PredicatedConstantNode(Signal val, Func<bool> predicate)
        {
            this.ConstValue = val;
            Predicate = predicate;
        }

        public override void Update(double time)
        {
            if (this.Predicate())
                this.Signal = ConstValue;
            else
                this.Signal = Signal.None;
        }
    }
}
