using System;

namespace Composer.Effects
{
    public class LowPassFilter : ISampleTransform
    {
        public bool CanClose { get { return true; }}
        public Func<double> Min { get; set; }

        public LowPassFilter(double min)
        {
            this.Min = () => { return min; };
        }

        public LowPassFilter(Func<double> min)
        {
            this.Min = min;
        }

        public Sample Transform(SampleTime time, Sample sample)
        {
            sample.Left = Math.Max(sample.Left, this.Min());
            sample.Right = Math.Max(sample.Right, this.Min());

            return sample;
        }

        public void Release()
        {
        }
    }
}