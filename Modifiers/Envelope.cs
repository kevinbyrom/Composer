using System;
using System.Diagnostics;
using Composer.Effects;
using Composer.Utilities;


namespace Composer.Modifiers
{
    public struct EnvelopeStage
    {
        public static EnvelopeStage Empty = new EnvelopeStage(0, 0);

        public Func<double> Time;
        public Func<double> Level;
        public Func<bool> Sustain;

       // public Func<double, double, double, double> Lerp;

        public EnvelopeStage(double time, double level) 
        {
            this.Time = () => { return time; };
            this.Level = () => { return level; };
            this.Sustain = null;
        }

        public EnvelopeStage(Func<double> time, Func<double> level, Func<bool> sustain = null)
        {
            this.Time = time;
            this.Level = level;
            this.Sustain = sustain;
        }

        public bool TimeInBlock(double time) 
        {
            return (this.Sustain != null && this.Sustain()) || time < this.Time();
        }

    }   


    public class EnvelopeModifier : ISignalSource
    {
        public ISignalSource InputSource { get; private set; }
        public EnvelopeStage[] Stages { get; private set; }

        private double stateTime;
        private int currStage;
        private double startTime;

        public EnvelopeModifier(ISignalSource source, EnvelopeStage[] stages)
        {
            this.InputSource = source;
            this.Stages = stages;
        }

        public void Update(double time)
        {
            if (time - startTime >= this.Stages[currStage].Time())
            {

            }
        }

        public void Reset()
        {

        }


        public Signal GetValue(double time)
        {

            // Determine what stage we are in (assuming that time is relative to the voice start time)

            EnvelopeStage prevStage = EnvelopeStage.Empty;
            EnvelopeStage currStage = EnvelopeStage.Empty;

            foreach (var stage in this.Stages)
            {
                if (!stage.TimeInBlock(time))
                    break;

                prevStage = currStage;
                currStage = stage;
            }


            // Get the value based on where we are in the stage

            var signal = Signal.Zero;
            double pct = (time - prevStage.Time()) / (currStage.Time() - prevStage.Time());

            pct = Math.Max(pct, 1);

            signal += MathUtil.Lerp(prevStage.Level(), currStage.Level(), pct);


            // Mix the input with the current signal

            return this.InputSource.GetValue(time) * signal;
        }
    }

    public static class EnvelopeExtensions
    {
        public static ISignalSource Envelope(this ISignalSource source, EnvelopeStage[] stages)
        {
            return new EnvelopeModifier(source, stages);
        }
    }
}
