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
       
        private Func<double, double> freqFunc;

        private Func<double, double> ampFunc;

        private double duration;

        private Func<Sample, Sample> effectFunc;

        private double currTime;
        private VoiceState currState;
        private double stateTime;
        private double currAmp;
        private double peakAmp;

        

        public Voice(Func<double, double> freqFunc, Func<double, double> ampFunc, double duration, Func<Sample, Sample> effectFunc = null)
        {
            this.freqFunc = freqFunc;
            this.ampFunc = ampFunc;
            this.duration = duration;
            this.effectFunc = effectFunc;

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


            // Advance time and check for state machine changes

            this.currTime += timeDelta;
            this.stateTime += timeDelta; 


            // Adjust amplitude based on envelope

            switch (this.currState)
            {
                case VoiceState.Attack:
                    this.currAmp = MathUtil.Lerp(0, 1, (float)(Math.Min(this.stateTime, AttackTime) / AttackTime));
                    break;

                case VoiceState.Decay:
                    this.currAmp = MathUtil.Lerp(1, this.ampFunc(this.currTime), (float)(Math.Min(this.stateTime, DecayTime) / DecayTime));
                    break;

                case VoiceState.Release:
                    this.currAmp = MathUtil.Lerp(this.peakAmp, 0, (float)(Math.Min(this.stateTime, ReleaseTime) / ReleaseTime));
                    break;
            }


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

            float val = (float)this.freqFunc(currTime) * (float)this.currAmp;


            // Apply effects

            Sample sample = new Sample(val, val);

            if (this.effectFunc != null)
                sample = this.effectFunc(sample);

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
