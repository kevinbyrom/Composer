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
    public class ReverbNode : SignalNodeBase
    {
        const int NumDelayNodes = 2;

        public ISignalNode Input { get; set; }
        public Func<double> Time { get; set; }
        public Func<double> Dampen { get; set; }

        private DelayNode[] delayNodes = new DelayNode[NumDelayNodes];

        public ReverbNode() : base()
        {
            this.Dampen = () => { return 0.5; };

            for (int i = 0; i < this.delayNodes.Length; i++)
            {
                this.delayNodes[i] = new DelayNode();
                this.delayNodes[i].Input = this.Input;
                this.delayNodes[i].Time = () => { return Time() + 0.25 + (i * 0.25);  };
                this.delayNodes[i].Dampen = this.Dampen;
            }
        }

        public ReverbNode(ISignalNode input) : this()
        {
            this.Input = input;
        }

        public override void Update(double time)
        {
            this.Input.Update(time);

            Signal signal = this.Input.Signal;

            double val = 0;

            for (int i = 0; i < this.delayNodes.Length; i++)
            {
                this.delayNodes[i].Update(time);

                val += this.delayNodes[i].Signal.Value;
            }

            this.Signal = new Signal(val / NumDelayNodes);
        }
    }

    public static class ReverbNodeExtensions
    {
        public static ISignalNode Reverb(this ISignalNode src, double time, double dampen = 0.5)
        {
            var node = new DelayNode(src);

            node.Time = () => time; ;
            node.Dampen = () => dampen;
            return node;
        }
    }
}
