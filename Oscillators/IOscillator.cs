using System;


namespace Composer.Oscillators
{
    public interface IOscillator : ISampleSource
    { 
        Func<double> Frequency { get; set; }
    }
}
