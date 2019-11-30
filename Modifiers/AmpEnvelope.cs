using System;
using Composer.Utilities;


namespace Composer.Modifiers
{
    public enum EnvelopeState
    {
        Attack,
        Decay,
        Sustain,
        Release
    }


    public struct EnvelopeSetting
    {
        public double Time;
        public double Target;
    }   


    public struct EnvelopeConfig
    {
        public EnvelopeSetting Attack;

        public EnvelopeSetting Decay;
        public EnvelopeSetting Sustain;
        public EnvelopeSetting Release;
    }


    public class AmpEnvelopeModifier : ISampleTransform
    {
        public EnvelopeConfig Envelope;
       
        private EnvelopeState currState;
        private double stateTime;
        private double targetTime;


        public AmpEnvelopeModifier(EnvelopeConfig envelope)
        {
            this.Envelope = envelope;
            this.stateTime = 0;
        }

        public Sample Transform(SampleTime time, Sample sample)
        {
            double currAmp = 0;


            // Advance time and check for state machine changes

            this.stateTime += time.Elapsed; 


            // Get amplitude based on envelope

            switch (this.currState)
            {
                case EnvelopeState.Attack:
                    currAmp = MathUtil.Lerp(0, this.Envelope.Attack.Target, TimePercent(this.stateTime, this.Envelope.Attack.Time));
                    break;

                case EnvelopeState.Decay:
                    currAmp = MathUtil.Lerp(this.Envelope.Attack.Target, this.Envelope.Decay.Target, TimePercent(this.stateTime, this.Envelope.Decay.Time));
                    break;

                case EnvelopeState.Release:
                    currAmp = MathUtil.Lerp(this.peakAmp, 0, TimePercent(this.stateTime, this.Envelope.Release.Time));
                    break;
            }


            // Check for state changes

            if (this.targetTime != -1 && this.stateTime >= this.targetTime)
            {
                NextState();
            }
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
        }

        private float TimePercent(double currTime, double targetTime)
        {
            return (float)(Math.Min(currTime, targetTime) / targetTime);
        }

        private void NextState()
        {
            switch (this.currState)
            {
                case EnvelopeState.Attack:
                    this.targetTime = this.Envelope.Attack.Time;
                    this.currState = EnvelopeState.Decay;
                    break;

            }

            this.stateTime = 0;
        }
    }
}