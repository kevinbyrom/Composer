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
        IOscillator Oscillator { get; set; }

        public OscillatorNode() : base()
        {

        }

        public OscillatorNode(IOscillator osc) :this()
        {
            this.Oscillator = osc;
        }

        public override void Update(double time)
        {
            if (this.Oscillator != null)
                this.Signal = this.Oscillator.GetValue(time);
        }
    }
}
