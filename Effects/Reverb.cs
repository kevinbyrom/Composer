using System;


namespace Composer.Effects
{
    public class ReverbEffect : ISampleTransform
    {
        public bool CanClose { get { return true; }}
        public ReverbEffect()
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