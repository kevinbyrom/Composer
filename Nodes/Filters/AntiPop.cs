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
    public class AntiPopNode : SignalNodeBase
    {
        public ISignalNode Input { get; set; }
        public Func<double> MaxVelocity { get; set; }

        private Signal lastSignal = Signal.None;

        public AntiPopNode() : base()
        {
        }

        public AntiPopNode(ISignalNode input) : this()
        {
            this.Input = input;
        }

        public override void Update(double time)
        {
            this.Input.Update(time);


            // Determine the velocity since last update

            var velocity = (this.Input.Signal - this.lastSignal).Value;


            // Ensure it is capped

            if (velocity > 0)
                velocity = Math.Min(this.MaxVelocity(), velocity);
            else
                velocity = Math.Max(-this.MaxVelocity(), velocity);

            var signal = this.Input.Signal + velocity;
            
            this.lastSignal = signal;

            this.Signal = signal;
        }
    }

    public static class AntiPopNodeExtensions
    {
        public static ISignalNode AntiPop(this ISignalNode src, double maxVelocity)
        {
            var node = new AntiPopNode(src);

            node.MaxVelocity = () => { return maxVelocity; };

            return node;
        }
    }
}
