using System;
using System.Collections.Generic;
using System.Diagnostics;
using Composer.Utilities;


namespace Composer
{
    public enum VoiceState
    {
        Playing,
        Releasing,
        Released
    }

    public class Voice 
    {
        private const double AttackTime = 0.1;
        private const double DecayTime = 0.1;
        private const double ReleaseTime = 1;

        public bool IsActive
        {
            get
            {
                return this.currState != VoiceState.Released;
            }
        }

        public ISampleSource Source { get; private set; }

        public IEnumerable<ISampleTransform> Transforms { get; set; }

        public ISampleTarget Target { get; private set; }
       
        public double Duration { get; private set; }

        public Sample CurrSample
        {
            get
            {
                return this.currSample;
            }
        }
        private Sample currSample;

        private VoiceState currState;
        

        public Voice(ISampleSource source, IEnumerable<ISampleTransform> transforms, ISampleTarget target, double duration)
        {
            this.Source = source;
            this.Transforms = transforms;
            this.Target = target;
            this.Duration = duration;

            ChangeState(VoiceState.Playing);
        }


        public void Update(SampleTime time)
        {

            if (!this.IsActive)
            {
                //this.currSample = Sample.Zero;
                this.Target.Write(time, Sample.Zero);
                return;
            }


            // Get the start signal

            var sample = this.Source.GetValue(time.Current);


            // Run signal through transforms
            
            foreach (var transform in this.Transforms)
                sample = transform.Transform(time, sample);


            // Output to target

            this.Target.Write(time, sample);


            

    
            // Get current signal value

            //Sample sample = this.signalFunc(time);

            //Sample val = (float)this.signalFunc(currTime) * (float)this.currAmp;

            //this.CurrSample = sample;

            //this.Target.Write(sample);*/

        }


        void ChangeState(VoiceState newState)
        {
            Debug.WriteLine("New state = " + newState.ToString());
            this.currState = newState;
        }


        public void Release()
        {
            this.peakAmp = this.currAmp;
            ChangeState(VoiceState.Release);
            Debug.WriteLine("Released voice");
        }
    }
}
