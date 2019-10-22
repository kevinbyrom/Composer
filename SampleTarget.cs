using System.Collections.Generic;

namespace Composer
{
    public interface ISampleTarget
    {
        void Write(Sample sample);
        void Write(IEnumerable<Sample> samples);
    }
}
