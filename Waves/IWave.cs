using System;


namespace Composer.Waves
{
    public interface IWave : ISignalSource
    { 
        Func<double> Frequency { get; set; }
    }
}
