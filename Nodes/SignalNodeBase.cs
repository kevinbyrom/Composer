using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composer.Nodes
{
    public abstract class SignalNodeBase : ISignalNode
    {
        public Signal Signal { get; protected set; }
        public double ActiveStartTime { get; }

        private double activeStartTime = 0;
        private double lastUpdateTime = 0;

        public virtual void Update(double time)
        {
            
            // Check if updates have already happened

            if (lastUpdateTime == time)
                return;

            lastUpdateTime = time;


            // Track when the signal started to be active

            if (!this.Signal.IsActive)
                this.activeStartTime = time; 
        }

        public Signal GetSignal(double time)
        {
            return this.Signal;
        }
    }
}
