using System;
using System.Linq;


namespace Composer
{
    public interface ISignalSource
    {
        double GetValue(double time, double freq);
    }
}
