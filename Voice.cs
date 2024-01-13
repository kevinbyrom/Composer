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

    public class Voice
    {        
        //public IOscillator Oscillator { get; set; }
        //public ISignalSource Effect { get; set; }

        //public bool IsActive { get; private set; }

        public ISignalSource Source { get; set; }
        
        public VoiceState CurrState { get; private set; }

        public Signal CurrSignal { get; private set; }

        private double currTime;
        private double stateTime;


        public Voice(ISignalSource source)
        {
            this.Source = source;
            //Oscillator = oscillator;
            ChangeState(VoiceState.Stopped);
        }


        public void On()
        {
            if (this.CurrState != VoiceState.Playing)
                ChangeState(VoiceState.Playing);
        }


        public void Off()
        {
            if (this.CurrState != VoiceState.Stopped)
                ChangeState(VoiceState.Stopped);
        }


        public void Update(double time)
        {
            Signal signal = Signal.Zero;

            this.currTime = time;

            if (CurrState == VoiceState.Playing)
                signal = this.Source.GetValue(time - stateTime);

            this.CurrSignal = signal;
        }


        void ChangeState(VoiceState newState)
        {
            this.CurrState = newState;
            this.stateTime = this.currTime;
        }
    }
}
