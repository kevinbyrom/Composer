using Composer.Operators;
using System;

namespace Composer.Effects
{
    public class HighPassFilter : ISignalSource
    {
        public ISignalSource Source { get; set; }
        public Func<double> Max { get; set; }


        public HighPassFilter(ISignalSource source, double max)
        {
            this.Source = source;
            this.Max = () => { return max; };
        }

        public HighPassFilter(ISignalSource source, Func<double> max)
        {
            this.Source = source;
            this.Max = max;
        }


        public Signal GetValue(double time)
        {
            var signal = Source.GetValue(time);

            signal.Value = Math.Min(signal.Value, this.Max());
            
            return signal;
        }
    }

    public static class HighPassFilterExtensions
    {
        public static ISignalSource HighPass(this ISignalSource source, double max)
        {
            return new HighPassFilter(source, max);
        }

        public static ISignalSource HighPass(this ISignalSource source, Func<double> max)
        {
            return new HighPassFilter(source, max);
        }
    }
}