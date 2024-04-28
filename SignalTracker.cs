using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composer
{
    
    public class SignalTracker
    {
        private Signal[] signals;
        int size;
        int pos;

        public SignalTracker(int size) 
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
    

    /*
    public class SignalTracker
    {
        private Signal[] signals;
        int size;
        int pos;

        public SignalTracker(int size) 
        {
            this.signals = new Signal[size];
            this.size = size;
            this.pos = 0;
        }

        public void Add(Signal signal) 
        {
            for (int i = 0; i < size - 1; i++)
            {
                this.signals[i] = this.signals[i + 1];
            }

            this.signals[size - 1] = signal;
        }

        public Signal[] GetAll() 
        {
            return this.signals;
        }
    }
    */
}
