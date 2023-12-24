using Composer.Operators;
using System;

namespace Composer.Effects
{
    public class CompressEffect : EffectBase
    {
        public Func<double> Min { get; set; }
        public Func<double> Max { get; set; }
        
        public CompressEffect(ISampleSource source, double min, double max) : base(source)
        {
            this.Min = () => { return min; };
            this.Max = () => { return max; };
        }

        public CompressEffect(ISampleSource source, Func<double> min, Func<double> max) : base(source) 
        {
            this.Min = min;
            this.Max = max;
        }

        public override Sample GetValue(double time)
        {
            var sample = Source.GetValue(time);

            sample.Left = Math.Max(sample.Left, this.Min());
            sample.Left = Math.Min(sample.Left, this.Max());
            sample.Right = Math.Max(sample.Right, this.Min());
            sample.Right = Math.Min(sample.Right, this.Max());
            
            return sample;
        }
    }

    public static class CompressExtensions
    {
        public static ISampleSource Compress(this ISampleSource source, double min, double max)
        {
            return new CompressEffect(source, min, max);
        }

        public static ISampleSource Compress(this ISampleSource source, Func<double> min, Func<double> max)
        {
            return new CompressEffect(source, min, max);
        }
    }
}