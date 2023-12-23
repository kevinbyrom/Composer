/*using System;

namespace Composer.Effects
{
    public class HighPassFilter : ISampleSource
    {
        public bool CanClose { get { return true; }}
        public Func<double> Max { get; set; }

        public HighPassFilter(double max)
        {
            this.Max = () => { return max; };
        }

        public HighPassFilter(Func<double> max)
        {
            this.Max = max;
        }

        public Sample Transform(SampleTime time, Sample sample)
        {
            sample.Left = Math.Min(sample.Left, this.Max());
            sample.Right = Math.Min(sample.Right, this.Max());

            return sample;
        }

        public Sample GetValue(SampleTime time)
        {
            sample.Left = Math.Min(sample.Left, this.Max());
            sample.Right = Math.Min(sample.Right, this.Max());

            return sample;
        }

        public void Release()
        {
        }
    }
}*/