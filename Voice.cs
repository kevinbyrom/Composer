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

        public bool IsActive { get; private set; }

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
        private double stateTime;


        public Voice(ISampleSource source, IEnumerable<ISampleTransform> transforms, ISampleTarget target, double duration)
        {
            this.Source = source;
            this.Transforms = transforms;
            this.Target = target;
            this.Duration = duration;
            this.IsActive = true;

            ChangeState(VoiceState.Playing);
        }


        public void Update(SampleTime time)
        {

            if (!this.IsActive)
            {
                this.Target.Write(time, Sample.Zero);
                return;
            }


            // Get the start signal

            var sample = this.Source.GetValue(time.Current);


            // Advance time 

            this.stateTime += time.Elapsed; 


            // Run signal through transforms
            
            bool canClose = true;

            foreach (var transform in this.Transforms)
            {
                sample = transform.Transform(time, sample);

                if (!transform.CanClose)
                canClose = false;
            }

            // Check for state changes

            if (this.currState == VoiceState.Playing && this.Duration != -1 && this.stateTime >= this.Duration)
                Release();


            // Determine if voice is still active

            if (this.currState == VoiceState.Releasing && canClose)
                this.IsActive = false;


            // Output to target

            this.Target.Write(time, sample);
        }


        void ChangeState(VoiceState newState)
        {
            Debug.WriteLine("New voice state = " + newState.ToString());
            this.currState = newState;
            this.stateTime = 0;
        }


        public void Release()
        {
            if (this.currState == VoiceState.Playing)
            {
                ChangeState(VoiceState.Releasing);
                
                foreach (var transform in this.Transforms)
                    transform.Release();
            }    
        }
    }
}
