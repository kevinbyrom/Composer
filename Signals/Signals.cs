using System;
using System.Linq;


namespace Composer.Signals
{
    public interface ISignal
    {
        double GetValue(double time, double freq, double amp);
    }


    public class EmptyWaveSignal : ISignal
    {
        public double GetValue(double time, double amp, double freq)
        {
            return 0;
        }
    }


    public class SineWaveSignal : ISignal
    {
        public double GetValue(double time, double freq, double amp)
        {
            return Math.Sin(freq * time * 2 * Math.PI) * amp;
        }
    }

    public class TriangleWaveSignal : ISignal
    {
        public double GetValue(double time, double freq, double amp)
        {
            return Math.Abs(2 * (time * freq - Math.Floor(time * freq + 0.5))) * amp * 2 - amp;
        }
    }

    public class SquareWaveSignal : ISignal
    {
        public double GetValue(double time, double freq, double amp)
        {
            return Math.Sin(freq * time * 2 * Math.PI) >= 0 ? amp : -amp;
        }
    }

    public class SawtoothWaveSignal : ISignal
    {
        public double GetValue(double time, double freq, double amp)
        {
            return 2 * (time * freq - Math.Floor(time * freq + 0.5)) * amp;
        }
    }

    public class PulseWaveSignal : ISignal
    {
        public double DutyCycle;

        public double GetValue(double time, double freq, double amp)
        {
            double period = 1.0 / freq;
            double timeModulusPeriod = time - Math.Floor(time / period) * period;
            double phase = timeModulusPeriod / period;

            if (phase <= DutyCycle)
                return amp;
            else
                return -amp;
        }
    }

    public class NoiseWaveSignal : ISignal
    {
        public Random Rng = new Random();

        public double GetValue(double time, double freq, double amp)
        {
            return (Rng.NextDouble() - Rng.NextDouble()) * amp;
        }
    }


    public class CompositeWaveSignal : ISignal
    {
        public ISignal[] Signals;

        public double GetValue(double time, double freq, double amp)
        {
            return Signals.Select(s => s.GetValue(time, amp, freq)).Sum();
        }
    }
}
