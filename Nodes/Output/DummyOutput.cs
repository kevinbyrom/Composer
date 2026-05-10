using System;
using System.Collections.Generic;

namespace Composer.Output
{
    public class DummyOutput : ISignalTarget
    {
        public void Write(double time, Signal signal)
        {
            // Do nothing
        }

        public void Write(double time, IEnumerable<Signal> signals)
        {
            // Do nothing
        }

        public void Flush()
        {
            // Do nothing
        }
    }
}