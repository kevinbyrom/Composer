using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composer.Effects
{
    public abstract class EffectBase : ISignalSource
    {
        public ISignalSource Source { get; set; }

        public EffectBase(ISignalSource source) 
        { 
            this.Source = source; 
        }

        public abstract Signal GetValue(double time);
    }
}
