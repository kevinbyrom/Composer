using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Composer.Nodes;

namespace Composer.Nodes.Logic
{
    public abstract class LogicalNodeBase : SignalNodeBase
    {
        public ISignalNode InputA { get; set; }
        public ISignalNode InputB { get; set; }

        public LogicalNodeBase()
        {

        }

        public LogicalNodeBase(ISignalNode inputA, ISignalNode inputB)
        {
            InputA = inputA;
            InputB = inputB;
        }
    }
}
