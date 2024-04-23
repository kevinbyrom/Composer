using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Composer.Nodes;
using Composer.Nodes.Operators;

namespace Composer.Nodes.Output
{
    public class MixerNode : SignalNodeBase
    {
        ISignalNode[] Sources { get; set; }

        public MixerNode() : base()
        {

        }

        public MixerNode(ISignalNode[] sources) : this()
        {
            this.Sources = sources;
        }

        public override void Update(double time)
        {
            if (this.Sources != null)
            {
                double val = 0;
                int numActive = 0;

                foreach (var s in this.Sources)
                {
                    s.Update(time);

                    val += s.Signal.Value;

                    if (s.Signal.Value != 0)
                        numActive++;
                }

                Signal mixed = new Signal();

                mixed.Value = val / (double)numActive;

                this.Signal = mixed;
            }
        }
    }
    public static class MixerNodeExtensions
    {
        public static ISignalNode Mix(this ISignalNode src, ISignalNode target)
        {
            var node = new MixerNode(new ISignalNode[] { src, target });

            return node;
        }
    }
}
