using System.Collections.Generic;

namespace Composer
{
    public interface ISampleTransform
    {
        Sample Transform(SampleTime time, Sample sample);
    }
}
