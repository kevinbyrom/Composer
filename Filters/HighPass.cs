using Composer.Operators;
using System;

namespace Composer.Effects
{
    public class HighPassFilter : ISampleSource
    {
        public ISampleSource Source { get; set; }
        public Func<double> Max { get; set; }


        public HighPassFilter(ISampleSource source, double max)
        {
            this.Source = source;
            this.Max = () => { return max; };
        }

        public HighPassFilter(ISampleSource source, Func<double> max)
        {
            this.Source = source;
            this.Max = max;
        }


        public Sample GetValue(double time)
        {
            var sample = Source.GetValue(time);

            sample.Left = Math.Min(sample.Left, this.Max());
            sample.Right = Math.Min(sample.Right, this.Max());

            return sample;
        }
    }

    public static class HighPassFilterExtensions
    {
        public static ISampleSource HighPass(this ISampleSource source, double max)
        {
            return new HighPassFilter(source, max);
        }

        public static ISampleSource HighPass(this ISampleSource source, Func<double> max)
        {
            return new HighPassFilter(source, max);
        }
    }
}