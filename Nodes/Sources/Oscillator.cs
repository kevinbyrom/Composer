using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Composer.Nodes;
using Composer.Oscillators;

namespace Composer.Nodes.Sources
{
    public class OscillatorNode : SignalNodeBase
    {
        ISignalNode Power { get; set; }

        IOscillator Oscillator { get; set; }

        public Func<double> Frequency { get; set; }

        double startTime = 0.0;

        public OscillatorNode() : base()
        {

        }

        public OscillatorNode(IOscillator osc) :this()
        {
            this.Oscillator = osc;
        }

        public OscillatorNode(IOscillator osc, ISignalNode power) : this(osc)
        {
            this.Power = power;
        }

        public override void Update(double time)
        {
            this.Power.Update(time);

            bool powerOn = this.Power == null || this.Power.Signal.Value > 0;

            if (powerOn && this.Oscillator != null)
            {
                this.Signal = this.Oscillator.GetValue(time - startTime);
            }
            else
            {
                startTime = time;
                this.Signal = Signal.Zero;
            }
        }
    }
}
