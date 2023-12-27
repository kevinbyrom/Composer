using System;


namespace Composer.Oscillators
{
    public interface IOscillator : ISignalSource
    { 
        Func<double> Frequency { get; set; }
    }
}
