using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Composer.Nodes.Operators
{
    public class DivideNode : OperatorNodeBase
    {
        public DivideNode() : base() { }
        public DivideNode(ISignalNode oper1, ISignalNode oper2) : base(oper1, oper2) { }

        public override void Update(double time)
        {
            Operand1.Update(time);
            Operand2.Update(time);

            this.Signal = Operand1.Signal / Operand2.Signal;
        }
    }
}
