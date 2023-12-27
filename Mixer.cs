using System;
using System.Collections.Generic;
using System.Diagnostics;
using Composer.Utilities;


namespace Composer
{
    public class Mixer : ISignalSource
    {
        public List<ISignalSource> Sources { get; set; }

        public Mixer()
        {
            this.Sources = new List<ISignalSource>();
        }

        public Mixer(IEnumerable<ISignalSource> sources) : this()
        {
            this.Sources.AddRange(sources);
        }

        public Signal GetValue(double time)
        {
            
            double val = 0;
            int numSources = 0;

            foreach (var source in this.Sources)
            {
                var s = source.GetValue(time);

                val += s.Value;
                
                if (s.Value != 0)
                    numSources++;
            }

            Signal mixed = new Signal();

            mixed.Value = val / (double)numSources;

            return mixed;
        }
    }
}
