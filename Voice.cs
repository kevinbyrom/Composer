using System;
using System.Collections.Generic;
using System.Diagnostics;
using Composer.Oscillators;
using Composer.Utilities;


namespace Composer
{
    public enum VoiceState
    {
        Stopped,
        Playing,
        Releasing
    }

    public class Voice : ISampleSource
    {        
        //public IOscillator Oscillator { get; set; }
        //public ISampleSource Effect { get; set; }

        public bool IsActive { get; private set; }

        public ISampleSource Source { get; set; }
        
        public VoiceState CurrState { get; private set; }

        private double stateTime;


        public Voice(ISampleSource source)
        {
            this.Source = source;
            //Oscillator = oscillator;
            ChangeState(VoiceState.Stopped);
        }


        /*public void Update(SampleTime time)
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
        }*/


        public void On()
        {
            ChangeState(VoiceState.Playing);
        }


        public void Off()
        {
            ChangeState(VoiceState.Stopped);
        }


        public Sample GetValue(double time)
        {
            Sample sample = Sample.Zero;


            // Get the oscillator value

            if (CurrState == VoiceState.Playing)
            {
                sample = Source.GetValue(time - stateTime);
            }


            // Apply the envelope
            // Apply the effects

            return sample;
        }

        void ChangeState(VoiceState newState)
        {
            this.CurrState = newState;
            this.stateTime = 0;
        }


        /*public void Release()
        {
            if (this.currState == VoiceState.Playing)
            {
                ChangeState(VoiceState.Releasing);
                
                foreach (var transform in this.Transforms)
                    transform.Release();
            }    
        }*/
    }
}
