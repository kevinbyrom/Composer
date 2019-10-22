using System;
using System.Diagnostics;
using Composer.Signals;
using Composer.Utilities;


namespace Composer
{
    public enum VoiceState
    {
        Attack,
        Decay,
        Sustain,
        Release,
        Finished
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
                return this.currState != VoiceState.Finished;
            }
        }

        public Sample CurrSample { get; private set; }
       
        private ISignal signal;
        private double frequency;
        private double amplitude;
        private IDuration duration;

        private int currTime;
        private VoiceState currState;
        private double stateTime;
        private double currAmp;
        private double peakAmp;


        public Voice(ISignal signal, double freq, double amp, IDuration duration)
        {
            this.signal = signal;
            this.frequency = freq;
            this.amplitude = amp;
            this.duration = duration;

            this.currTime = 0;
            ChangeState(VoiceState.Attack);
        }


        public void Update()
        {
            if (!this.IsActive)
            {
                this.CurrSample = Sample.Zero;
                return;
            }

            // Check for cancellation (time or manual based)

            if (this.duration.IsDone && this.currState != VoiceState.Release)
            {
                ChangeState(VoiceState.Release);
                this.peakAmp = this.currAmp;
            }

            // Progress the envelope

            // Adjust amplitude based on envelope

            switch (this.currState)
            {
                case VoiceState.Attack:
                    this.currAmp = MathUtil.Lerp(0, 1, (float)(Math.Min(this.stateTime, AttackTime) / AttackTime));
                    break;

                case VoiceState.Decay:
                    this.currAmp = MathUtil.Lerp(1, this.amplitude, (float)(Math.Min(this.stateTime, DecayTime) / DecayTime));
                    break;

                case VoiceState.Release:
                    this.currAmp = MathUtil.Lerp(this.peakAmp, 0, (float)(Math.Min(this.stateTime, ReleaseTime) / ReleaseTime));
                    break;
            }


            // Get current signal value

            float val = (float)this.signal.GetValue(this.currTime, this.frequency, this.currAmp);


            // Advance time and check for state machine changes

            this.currTime++;
            this.stateTime++; 

            switch (this.currState)
            {
                case VoiceState.Attack:
                    if (this.stateTime >= AttackTime)
                        ChangeState(VoiceState.Decay);
                    break;

                case VoiceState.Decay:
                    if (this.stateTime >= DecayTime)
                        ChangeState(VoiceState.Sustain);
                    break;

                case VoiceState.Release:
                    if (this.stateTime >= ReleaseTime)
                        ChangeState(VoiceState.Finished);
                    break;
            }

            this.CurrSample = new Sample(val, val);
        }


        void ChangeState(VoiceState newState)
        {
            Debug.WriteLine("New state = " + newState.ToString());
            Debug.WriteLine("Curr Amp = " + this.currAmp.ToString());
            this.currState = newState;
            this.stateTime = 0;
        }
    }
}
