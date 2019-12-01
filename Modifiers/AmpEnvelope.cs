using System;
using System.Diagnostics;
using Composer.Utilities;


namespace Composer.Modifiers
{
    public enum EnvelopeState
    {
        Attack,
        Decay,
        Sustain,
        Release,
        Released
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
        public bool CanClose
        {
            get
            {
                return this.currState == EnvelopeState.Released;
            }
        }

        public EnvelopeConfig Envelope;
        private EnvelopeState currState;
        private double stateTime;
        private double targetTime;

        private double peakAmp;

        public AmpEnvelopeModifier()
        {
            this.Envelope = AmpEnvelopeModifier.BasicConfig();
            ChangeState(EnvelopeState.Attack);
            this.peakAmp = 0;
        }

        public AmpEnvelopeModifier(EnvelopeConfig envelope)
        {
            this.Envelope = envelope;
            ChangeState(EnvelopeState.Attack);
            this.peakAmp = 0;
        }

        public Sample Transform(SampleTime time, Sample sample)
        {
            double currAmp = 0;


            // Advance time 

            this.stateTime += time.Elapsed; 


            // Get amplitude based on envelope

            switch (this.currState)
            {
                case EnvelopeState.Attack:
                    currAmp = CalculateAmp(0, this.Envelope.Attack.Target, this.Envelope.Attack.Time);
                    break;

                case EnvelopeState.Decay:
                    currAmp = CalculateAmp(this.Envelope.Attack.Target, this.Envelope.Decay.Target, this.Envelope.Decay.Time);
                    break;

                case EnvelopeState.Release:
                    currAmp = CalculateAmp(this.peakAmp, 0, this.Envelope.Release.Time);
                    break;
            }

            if (currAmp > this.peakAmp)
            {
                this.peakAmp = currAmp;
            }

            Debug.WriteLine($"CurrAmp = {currAmp}");

            // Check for state changes

            if (this.targetTime != -1 && this.stateTime >= this.targetTime)
            {
                NextState();
            }

            return sample * currAmp;
        }

        public void Release()
        {
            if (this.currState == EnvelopeState.Attack || this.currState == EnvelopeState.Decay)
            {
                ChangeState(EnvelopeState.Release);
                this.targetTime = this.Envelope.Release.Time;
            }
        }

        private double CalculateAmp(double minVal, double maxVal, double targetTime)
        {
            var timePct = TimePercent(this.stateTime, targetTime);

            Debug.WriteLine($"TimePct = {timePct}, StateTime = {this.stateTime}");

            return MathUtil.Lerp(minVal, maxVal, timePct); 
        }

        private float TimePercent(double currTime, double targetTime)
        {
            return (float)(Math.Min(currTime, targetTime) / targetTime);
        }


        private void ChangeState(EnvelopeState newState)
        {
            Debug.WriteLine("New envelope state = " + newState.ToString());
            this.currState = newState;
            this.stateTime = 0;
        }


        private void NextState()
        {
            
            // Set state to next and set new target time for the state

            switch (this.currState)
            {
                case EnvelopeState.Attack:
                    ChangeState(EnvelopeState.Decay);
                    this.targetTime = this.Envelope.Attack.Time;
                    break;

                case EnvelopeState.Decay:
                    ChangeState(EnvelopeState.Sustain);
                    this.targetTime = this.Envelope.Sustain.Time;
                    break;

                case EnvelopeState.Sustain:
                    ChangeState(EnvelopeState.Release);
                    this.targetTime = this.Envelope.Release.Time;
                    break;

                case EnvelopeState.Release:
                    ChangeState(EnvelopeState.Released);
                    this.targetTime = -1;
                    break;

            }
        }

        public static EnvelopeConfig BasicConfig()
        {
            var config = new EnvelopeConfig();

            config.Attack.Target = 1;
            config.Attack.Time = 0.5;

            config.Decay.Target = 0.75;
            config.Decay.Time = 0.25;
            ;
            config.Sustain.Target = 0.75;
            config.Sustain.Time = -1;

            config.Release.Target = 0;
            config.Release.Time = 1;

            return config;
        }
    }
}