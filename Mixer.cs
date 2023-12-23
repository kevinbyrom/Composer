using System;
using System.Collections.Generic;
using System.Diagnostics;
using Composer.Utilities;


namespace Composer
{
    public class Mixer : ISampleSource
    {
        public List<ISampleSource> Sources { get; set; }

        public Mixer()
        {
            this.Sources = new List<ISampleSource>();
        }

        public Mixer(IEnumerable<ISampleSource> sources) : this()
        {
            this.Sources.AddRange(sources);
        }

        public Sample GetValue(double time)
        {
            
            double left = 0;
            double right = 0;
            int numSamples = 0;

            foreach (var source in this.Sources)
            {
                var s = source.GetValue(time);

                left += s.Left;
                right += s.Right;

                if (s.Left != 0 && s.Right != 0)
                    numSamples++;
            }

            Sample mixed = new Sample();

            mixed.Left = left / (float)numSamples;
            mixed.Right = right / (float)numSamples;


            return mixed;
        }
    }
}
