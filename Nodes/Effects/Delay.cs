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
    public class DelayNode : SignalNodeBase
    {
        public ISignalNode Input { get; set; }
        public Func<double> Time { get; set; }
        public Func<double> Dampen { get; set; }

        private SignalQueue delayBuffer;

        public DelayNode() : base()
        {
            this.delayBuffer = new SignalQueue();
            this.Dampen = () => { return 0.5; };
        }

        public DelayNode(ISignalNode input) : this()
        {
            this.Input = input;
        }

        public override void Update(double time)
        {
            this.Input.Update(time);

            Signal signal = this.Input.Signal;
            
            foreach (var delayed in this.delayBuffer.GetNext(time))
            {
                signal += delayed;
            }

            this.delayBuffer.Add(signal * Dampen(), time + Time());

            this.Signal = signal;
        }
    }

    public static class DelayNodeExtensions
    {
        public static ISignalNode Delay(this ISignalNode src, double time, double dampen = 0.5)
        {
            var node = new DelayNode(src);

            node.Time = () => time; ;
            node.Dampen = () => dampen;
            return node;
        }
    }
}
