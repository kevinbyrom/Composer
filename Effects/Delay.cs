using System;


namespace Composer.Effects
{
    public class DelayEffect : ISignalTransform
    {
        public bool CanClose { get { return true; }}

        public DelayEffect()
        {
        }

        public Signal Transform(SampleTime time, Signal sample)
        {
            throw new NotImplementedException();
        }

        public void Release()
        {
        }
    }
}