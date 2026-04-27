using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Composer.Nodes;
using Composer.Waves;

namespace Composer.Nodes.Sources
{
    public class OscillatorNode : SignalNodeBase
    {
        ISignalNode Power { get; set; }

        IWave Wave { get; set; }

        public Func<double> Frequency { get; set; }
        public Func<double> Amp { get; set; }

        double startTime = 0.0;

        public OscillatorNode() : base()
        {
            this.Amp = () => 1.0;
        }

        public OscillatorNode(IWave wave) :this()
        {
            this.Wave = wave;
        }

        public OscillatorNode(IWave wave, ISignalNode power) : this(wave)
        {
            this.Power = power;
        }

        public override void Update(double time)
        {
            this.Power.Update(time);

            bool powerOn = this.Power == null || this.Power.Signal.Value > 0;

            if (powerOn && this.Wave != null)
            {
                this.Signal = this.Wave.GetValue(time - startTime) * Amp();
            }
            else
            {
                startTime = time;
                this.Signal = Signal.None;
            }
        }
    }
}
