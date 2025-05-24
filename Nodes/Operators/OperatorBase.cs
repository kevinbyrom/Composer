using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Composer.Nodes;

namespace Composer.Nodes.Operators
{
    public abstract class OperatorNodeBase : SignalNodeBase
    {
        public ISignalNode Operand1 { get; set; }
        public ISignalNode Operand2 { get; set; }

        public OperatorNodeBase()
        {

        }

        public OperatorNodeBase(ISignalNode oper1, ISignalNode oper2)
        {
            Operand1 = oper1;
            Operand2 = oper2;
        }
    }
}
