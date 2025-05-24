/*using Composer.Operators;
using System;

namespace Composer.Effects
{
    public class LowPassFilter : ISignalSource
    {
        public ISignalSource Source { get; set; }
        public Func<double> Min { get; set; }


        public LowPassFilter(ISignalSource source, double min)
        {
            this.Source = source;
            this.Min = () => { return min; };
        }

        public LowPassFilter(ISignalSource source, Func<double> min)
        {
            this.Source = source;
            this.Min = min;
        }


        public Signal GetValue(double time)
        {
            var signal = Source.GetValue(time);

            signal.Value = Math.Max(signal.Value, this.Min());

            return signal;
        }
    }

    public static class LowPassFilterExtensions
    {
        public static ISignalSource LowPass(this ISignalSource source, double min)
        {
            return new LowPassFilter(source, min);
        }

        public static ISignalSource LowPass(this ISignalSource source, Func<double> min)
        {
            return new LowPassFilter(source, min);
        }
    }
}*/
