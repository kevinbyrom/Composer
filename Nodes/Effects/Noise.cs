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
    public class NoiseNode : SignalNodeBase
    {
        public ISignalNode Input { get; set; }
        public Func<double> Randomness { get; set; }

        private Random rnd = new Random();

        public NoiseNode() : base()
        {
        }

        public NoiseNode(ISignalNode input) : this()
        {
            this.Input = input;
        }

        public override void Update(double time)
        {
            this.Input.Update(time);

            Signal signal = this.Input.Signal;

            double fuzz = this.Randomness() * rnd.NextDouble() * rnd.Next(-1, 1);

            signal += fuzz;

            this.Signal = signal;
        }
    }

    public static class NoiseNodeExtensions
    {
        public static ISignalNode Noise(this ISignalNode src, double randomness)
        {
            var node = new NoiseNode(src);

            node.Randomness = () => { return randomness; };

            return node;
        }
    }
}
