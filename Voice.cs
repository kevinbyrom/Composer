using System;
using System.Diagnostics;
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
       
        private Func<SampleTime, Sample> signalFunc;

        private Func<SampleTime, Sample, Sample> filterFunc;

        private Func<SampleTime, Sample> ampFunc;

        private Func<SampleTime, Sample, Sample> effectFunc;

        private double duration;
        
        private VoiceState currState;
        private double stateTime;
        private double currAmp;
        private double peakAmp;

        
        
        public Voice(Func<SampleTime, Sample> signalFunc, Func<SampleTime, Sample> ampFunc, Func<SampleTime, Sample, Sample> effectFunc, double duration)
        {
            this.signalFunc = signalFunc;
            this.ampFunc = ampFunc;
            this.duration = duration;
            this.effectFunc = effectFunc;

            ChangeState(VoiceState.Attack);
        }


        public void WriteNext(SampleTime time, ISampleTarget target)
        {
            
            if (!this.IsActive)
            {
                this.CurrSample = Sample.Zero;
                return;
            }


            // Advance time and check for state machine changes

            //this.currTime = time.Current;
            this.stateTime += time.Elapsed; 


            // Adjust amplitude based on envelope

            /*switch (this.currState)
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
            }*/


            // Check for cancellation (time or manual based)

            //if (this.duration != -1 && this.time.Current >= this.duration)
            //{
              //  Release();
            //}

            // Check for state changes

            /*switch (this.currState)
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

    */
            // Get current signal value

            Sample sample = this.signalFunc(time);

            //Sample val = (float)this.signalFunc(currTime) * (float)this.currAmp;

            //this.CurrSample = sample;

            target.Write(sample);
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
