using Composer.Operators;
using System;

namespace Composer.Effects
{
    public class LowPassFilter : ISampleSource
    {
        public ISampleSource Source { get; set; }
        public Func<double> Min { get; set; }


        public LowPassFilter(ISampleSource source, double min)
        {
            this.Source = source;
            this.Min = () => { return min; };
        }

        public LowPassFilter(ISampleSource source, Func<double> min)
        {
            this.Source = source;
            this.Min = min;
        }


        public Sample GetValue(double time)
        {
            var sample = Source.GetValue(time);

            sample.Left = Math.Max(sample.Left, this.Min());
            sample.Right = Math.Max(sample.Right, this.Min());

            return sample;
        }
    }

    public static class LowPassFilterExtensions
    {
        public static ISampleSource LowPass(this ISampleSource source, double min)
        {
            return new LowPassFilter(source, min);
        }

        public static ISampleSource LowPass(this ISampleSource source, Func<double> min)
        {
            return new LowPassFilter(source, min);
        }
    }
}