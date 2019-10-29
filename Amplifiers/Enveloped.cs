using System;

namespace Composer.Amplifiers
{
    public class EnvelopedAmplifier : ISampleTransform
    {
        public double TargetAmp { get; private set; }
        
        public EnvelopedAmplifier(double targetAmp)
        {
            this.TargetAmp = targetAmp;
        }

        public Sample Transform(SampleTime time, Sample sample)
        {
            return sample * this.TargetAmp;
        }
    }
}