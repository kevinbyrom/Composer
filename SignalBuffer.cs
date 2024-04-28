using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composer
{
    
    public class SignalBuffer
    {
        private Signal[] signals;
        int size;
        int pos;

        public SignalBuffer(int size) 
        {
            this.signals = new Signal[size];
            this.size = size;
            this.pos = 0;
        }

        public void Add(Signal signal) 
        {
            int addPos = (pos + size - 1) % size;

            signals[addPos] = signal;

            pos += 1;
        }

        public Signal[] GetAll() 
        { 
            var vals = new Signal[size];

            for (int i = 0; i < size; i++)
            {
                int currPos = (this.pos + i) % this.size;

                vals[i] = this.signals[currPos];
            }

            return signals;
        }
    }
}
