/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Composer.Nodes.Logic
{
    public class AndNode : LogicalNodeBase
    {
        public AndNode() : base() { }
        public AndNode(ISignalNode inputA, ISignalNode inputB) : base(inputA, inputB) { }

        public override void Update(double time)
        {
            Operand1.Update(time);
            Operand2.Update(time);

            this.Signal = Operand1.Signal + Operand2.Signal;
        }
    }
}
*/