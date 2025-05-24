using System;
using System.Diagnostics;
using Composer.Nodes;
using Composer.Oscillators;
using Composer.Utilities;


namespace Composer.Nodes.Modifiers
{
    public class EnvelopeSetting
    {
        public Func<double> Duration;
        public Func<double> Level;
        public Func<bool> Latch;

        public double TotalDuration()
        {
            if (Latch != null && Latch())
            {
                return double.MaxValue;
            }
            else
            {
                return Duration();
            }
        }
    }   


    public struct EnvelopeSettings
    {
        public EnvelopeSetting Attack;
        public EnvelopeSetting Decay;
        public EnvelopeSetting Sustain;
        public EnvelopeSetting Release;

        public EnvelopeSettings()
        {
            Attack = new EnvelopeSetting();
            Decay = new EnvelopeSetting();
            Sustain = new EnvelopeSetting();
            Release = new EnvelopeSetting();
        }
    }

    public enum EnvelopeState
    {
        None,
        Attacking,
        Decaying,
        Sustaining,
        Releasing
    }

    public class EnvelopeNode : SignalNodeBase
    {
        public ISignalNode Input { get; set; }
        public EnvelopeSettings Settings;

        EnvelopeSetting currSetting = null;
        EnvelopeState currState = EnvelopeState.None;
        double stateStartTime = 0;
        double currLevel = 0;
        double startLevel;
        double endLevel;
        Signal lastInput = Signal.None;

        public EnvelopeNode() : base()
        {
            this.Settings = new EnvelopeSettings();
            this.currSetting = null;
            this.stateStartTime = 0;
            this.currLevel = 0;
            this.startLevel = 0;
            this.endLevel = 0;
        }

        public EnvelopeNode(ISignalNode input) : this()
        {
            this.Input = input;
        }

        public override void Update(double time)
        {
            this.Input.Update(time);


            // Check for new signal activations

            if (!lastInput.IsActive && this.Input.Signal.IsActive)
            {
                this.currLevel = 0;
                SetState(EnvelopeState.Attacking, this.Settings.Attack, time);
            }
            
            lastInput = this.Input.Signal;


            // Do something only if we are not in NONE state

            if (this.currState != EnvelopeState.None)
            {
                // Determine value based on where we are, time-wise

                var totalDuration = currSetting.TotalDuration();
                var elapsed = time - stateStartTime;
                var pct = elapsed / (totalDuration != 0 ? totalDuration : 1);

                this.currLevel = MathUtil.Lerp(this.startLevel, this.endLevel, pct);

                // Check for state changes

                if (elapsed >= totalDuration)
                    NextState(time);
            }

            // Mix the input with the current value

            var signal = Signal.Max * this.currLevel;
            signal.IsActive = this.Input.Signal.IsActive || this.currState != EnvelopeState.None;
            this.Signal = signal;
        }

        private void NextState(double time)
        {
            switch(currState)
            {
                case EnvelopeState.Attacking:
                    SetState(EnvelopeState.Decaying, this.Settings.Decay, time);
                    break;

                case EnvelopeState.Decaying:
                    SetState(EnvelopeState.Sustaining, this.Settings.Sustain, time);
                    break;

                case EnvelopeState.Sustaining:
                    SetState(EnvelopeState.Releasing, this.Settings.Release, time);
                    break;

                case EnvelopeState.Releasing:
                    SetState(EnvelopeState.None, null, time);
                    break;

                default:
                    break;
            }
        }

        private void SetState(EnvelopeState state, EnvelopeSetting setting, double time)
        {
            this.currState = state;
            this.currSetting = setting;
            this.stateStartTime = time;

            if (setting != null)
            {
                this.startLevel = this.currLevel;
                this.endLevel = setting.Level();
            }
        }
    }
}
