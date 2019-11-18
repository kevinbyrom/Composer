using System;

namespace Composer.Modifiers
{
    public enum EvelopeState
    {
        Attack,
        Decay,
        Sustain,
        Release
    }

    public class EnvelopeModifier : ISampleTransform
    {
        

        public EnvelopeModifier()
        {
        }

        public Sample Transform(SampleTime time, Sample sample)
        {
            throw new NotImplementedException();
        }
    }
}