/*using System;
using System.Diagnostics;
using Composer.Utilities;


namespace Composer.Modifiers
{
    public struct EnvelopeStage
    {
        public Func<double> StartTime;
        public Func<double> EndTime;
        public Func<double> StartLevel;
        public Func<double> EndLevel;
        public Func<double, double, double, double> Lerp;

        public bool TimeInBlock(double time) 
        {
            return EndTime() == -1 || time >= StartTime() && time < EndTime();
        }

    }   


    public struct EnvelopeSettings
    {
        public EnvelopeStage Attack;
        public EnvelopeStage Decay;
        public EnvelopeStage Sustain;
        public EnvelopeStage Release;
    }


    public class EnvelopeModifier : ISignalSource
    {
        public ISignalSource InputSource;
        public EnvelopeSettings Settings;

        public EnvelopeModifier(EnvelopeSettings settings)
        {
            this.Settings = settings;
        }

        public Signal GetValue(double time)
        {
     
            // Determine what stage we are in (assuming that time is relative to the voice start time)

            EnvelopeStage stage;

            if (Settings.Attack.TimeInBlock(time))
                stage = Settings.Attack;
            else if (Settings.Decay.TimeInBlock(time))
                stage = Settings.Decay;
            else if (Settings.Sustain.TimeInBlock(time))  // SustainTime should be set to -1 if pedal held
                stage = Settings.Sustain;
            else
                stage = Settings.Release;


            // Get the value based on where we are in the stage

            var signal = Signal.Zero;

            var pct = time - stage.StartTime() / stage.EndTime() - stage.StartTime();

            signal += MathUtil.Lerp(stage.StartLevel(), stage.EndLevel(), pct);


            // Mix the input with the current signal

            return this.InputSource.GetValue(time) * signal;
        }
    }
}
*/