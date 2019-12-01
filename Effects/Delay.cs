using System;


namespace Composer.Effects
{
    public class DelayEffect : ISampleTransform
    {
        public bool CanClose { get { return true; }}

        public DelayEffect()
        {
        }

        public Sample Transform(SampleTime time, Sample sample)
        {
            throw new NotImplementedException();
        }

        public void Release()
        {
        }
    }
}