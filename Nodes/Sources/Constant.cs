using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Composer.Nodes;

namespace Composer.Nodes.Sources
{
    public class ConstantNode : SignalNodeBase
    {
        Signal ConstValue { get; set; }

        public ConstantNode(Signal val)
        {
            this.Signal = val;
        }
       
        public override void Update(double time)
        {
            this.Signal = ConstValue;
        }
    }
}
