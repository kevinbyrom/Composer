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
        private double duration;

        private IEffect effect;

        private double currTime;
        private VoiceState currState;
        private double stateTime;
        private double currAmp;
        private double peakAmp;


        public Voice(ISignal signal, double freq, double amp, double duration, IEffect effect = null)
        {
            this.signal = signal;
            this.frequency = freq;
            this.amplitude = amp;
            this.duration = duration;
            this.effect = effect;

            this.currTime = 0;
            ChangeState(VoiceState.Attack);
        }


        public void Update(double timeDelta)
        {
            
            if (!this.IsActive)
            {
                this.CurrSample = Sample.Zero;
                return;
            }


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


            // Advance time and check for state machine changes

            this.currTime += timeDelta;
            this.stateTime += timeDelta; 


            // Check for cancellation (time or manual based)

            if (this.duration != -1 && this.currTime >= this.duration)
            {
                Release();
            }

            // Check for state changes

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


            // Get current signal value

            float val = (float)this.signal.GetValue(currTime, this.frequency, this.currAmp);


            // Apply effects

            Sample sample = new Sample(val, val);

            if (this.effect != null)
                sample = this.effect.Apply(sample);

            this.CurrSample = sample;
        }


        void ChangeState(VoiceState newState)
        {
            Debug.WriteLine("New state = " + newState.ToString());
            Debug.WriteLine("Curr Amp = " + this.currAmp.ToString());
            this.currState = newState;
            this.stateTime = 0;
        }


        public void Release()
        {
            this.peakAmp = this.currAmp;
            ChangeState(VoiceState.Release);
        }
    }
}
