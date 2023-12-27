using System;
using System.Collections.Generic;
using System.Linq;


namespace Composer.Output
{
    /*public struct MixerInput
    {
        public ISignalSource Source;
        public float LeftBalance;
        public float RightBalance;
    }*/


    public class MixedOutput : ISignalTarget
    {
        private ISignalTarget target;

        public MixedOutput(ISignalTarget target)
        {
            this.target = target;
        }

        public void Write(SampleTime time, Signal sample)
        {
            this.target.Write(time, sample);
        }

        public void Write(SampleTime time, IEnumerable<Signal> signals)
        {
            if (signals.Count() == 0)
            {
                this.target.Write(time, Signal.Zero);
                return;
            }


            // Combine the signals and take an average
            // This code will likely need to be changed as 
            // I am hearing an audible shift when dead voices drop off

            double val = 0.0;
            int numSignals = 0;

            foreach (var s in signals)
            {
                val += s.Value;
                
                if (s.Value != 0)
                    numSignals++;
            }

            Signal mixed = new Signal();

            mixed.Value = val / (double)numSignals;
            
            this.target.Write(time, mixed);
        }

        public void Flush()
        {

        }
    }
}