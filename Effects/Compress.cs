using Composer.Operators;
using System;

namespace Composer.Effects
{
    public class CompressEffect : EffectBase
    {
        public Func<double> Min { get; set; }
        public Func<double> Max { get; set; }
        
        public CompressEffect(ISignalSource source, double min, double max) : base(source)
        {
            this.Min = () => { return min; };
            this.Max = () => { return max; };
        }

        public CompressEffect(ISignalSource source, Func<double> min, Func<double> max) : base(source) 
        {
            this.Min = min;
            this.Max = max;
        }

        public override Signal GetValue(double time)
        {
            var signal = Source.GetValue(time);

            signal.Value = Math.Max(signal.Value, this.Min());
            signal.Value = Math.Min(signal.Value, this.Max());
            
            return signal;
        }
    }

    public static class CompressExtensions
    {
        public static ISignalSource Compress(this ISignalSource source, double min, double max)
        {
            return new CompressEffect(source, min, max);
        }

        public static ISignalSource Compress(this ISignalSource source, Func<double> min, Func<double> max)
        {
            return new CompressEffect(source, min, max);
        }
    }
}