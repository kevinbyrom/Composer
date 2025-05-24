using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Composer.Nodes;
using Composer.Nodes.Operators;
using Composer.Nodes.Output;

namespace Composer.Nodes.Effects
{
    public class CompressNode : SignalNodeBase
    {
        public ISignalNode Input { get; set; }
        public Func<double> Min { get; set; }
        public Func<double> Max { get; set; }
        public CompressNode() :base()
        {
        }

        public CompressNode(ISignalNode input) : this()
        {
            this.Input = input;
        }

        public override void Update(double time)
        {
            this.Input.Update(time);

            Signal signal = this.Input.Signal;

            if (this.Min != null)
                signal.Value = Math.Max(this.Min(), signal.Value);

            if (this.Max != null)
                signal.Value = Math.Min(this.Max(), signal.Value);

            this.Signal = signal;
        }
    }

    public static class CompressNodeExtensions
    {
        public static ISignalNode Compress(this ISignalNode src, double min, double max)
        {
            var node = new CompressNode(src);

            node.Min = () => { return min; };
            node.Max = () => { return max; };

            return node;
        }
    }
}
