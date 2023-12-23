using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composer.Effects
{
    public abstract class EffectBase : ISampleSource
    {
        public ISampleSource InputSource;

        public EffectBase() { }

        public abstract Sample GetValue(double time);
    }
}
