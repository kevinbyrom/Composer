using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Composer
{
    public class SignalQueue
    {
        public struct QueueEntry
        {
            public double DeliverTime;
            public Signal Signal;
        }

        private Queue<QueueEntry> queue;

        public SignalQueue()
        {
            queue = new Queue<QueueEntry>();
        }

        public void Add(Signal signal, double deliverTime)
        { 
            this.queue.Enqueue(new QueueEntry {  DeliverTime = deliverTime, Signal = signal });
        }

        public IEnumerable<Signal> GetNext(double time)
        {
            var signals = new List<Signal>();

            var entry = new QueueEntry();

            while (true)
            {
                if (queue.TryPeek(out entry) && entry.DeliverTime <= time)
                {
                    queue.Dequeue();
                    yield return entry.Signal;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
