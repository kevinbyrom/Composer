using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composer.Nodes
{
    public interface ISignalNode
    {
        public Signal Signal { get; }
        public void Update(double time);
    }
}
