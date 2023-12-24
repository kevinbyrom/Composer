/*using System;
using System.Diagnostics;
using Composer.Utilities;


namespace Composer.Modifiers
{
    public struct EnvelopeStage : ISampleSource
    {
        public Func<double> StartTime;
        public Func<double> EndTime;
        public Func<double> Level1;
        public Func<double> Level2;
        public Func<double, double, double, double> Lerp;
        
        public Sample GetValue(double time)
        {
            var sample = Sample.Zero;

            var pct = time - StartTime() / EndTime() - StartTime();

            sample += Lerp(pct, Level1(), Level2());

            return sample;
        }

        public bool TimeInBlock(double time) 
        {
            return time >= StartTime() && time < EndTime();
        }

    }   


    public struct EnvelopeSettings
    {
        public EnvelopeStage Attack;
        public EnvelopeStage Decay;
        public EnvelopeStage Sustain;
        public EnvelopeStage Release;
    }


    public class EnvelopeModifier : ISampleSource
    {
        public ISampleSource InputSource;
        public EnvelopeSettings Settings;


        public EnvelopeModifier()
        {
            this.Settings = EnvelopeModifier.BasicSettings();
        }

        public EnvelopeModifier(EnvelopeSettings settings)
        {
            this.Settings = settings;
        }

        public Sample GetValue(double time)
        {
            var input = this.InputSource.GetValue(time);

            var sample = Sample.Zero;

            if (Settings.Attack.TimeInBlock(time))
                sample = Settings.Attack.GetValue(time);

            else if (Settings.Decay.TimeInBlock(time))
                sample = Settings.Decay.GetValue(time);

            else if (Settings.Sustain.TimeInBlock(time))
                sample = Settings.Sustain.GetValue(time);

            else
                sample = Settings.Release.GetValue(time);

            return input * sample;
        }

        public static EnvelopeSettings BasicSettings()
        {
            var settings = new EnvelopeSettings();

            settings.Attack.Target = 1;
            settings.Attack.Time = 0.5;

            settings.Decay.Target = 0.75;
            settings.Decay.Time = 0.25;

            settings.Sustain.Target = 0.75;
            settings.Sustain.Time = -1;

            settings.Release.Target = 0;
            settings.Release.Time = 1;

            return settings;
        }
    }
}
*/