using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composer.Effects
{
    public abstract class EffectBase : ISampleSource
    {
        public ISampleSource Source { get; set; }

        public EffectBase(ISampleSource source) 
        { 
            this.Source = source; 
        }

        public abstract Sample GetValue(double time);
    }
}
