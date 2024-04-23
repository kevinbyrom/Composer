/*using Composer.Operators;
using System;

namespace Composer.Nodes.Filters
{
    public class HighPassFilter : ISignalSource
    {
        public ISignalSource Source { get; set; }
        public Func<double> Max { get; set; }


        public HighPassFilter(ISignalSource source, double max)
        {
            Source = source;
            Max = () => { return max; };
        }

        public HighPassFilter(ISignalSource source, Func<double> max)
        {
            Source = source;
            Max = max;
        }


        public Signal GetValue(double time)
        {
            var signal = Source.GetValue(time);

            signal.Value = Math.Min(signal.Value, Max());

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
}*/