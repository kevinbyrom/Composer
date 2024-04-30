using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composer
{
    public static class Notes
    {
        public enum Key : int
        { 
            C = 0,
            CS = 1,
            D = 2,
            DS = 3,
            E = 4,
            F = 5,
            FS = 6,
            G = 7,
            GS = 8,
            A = 9,
            AS = 10,
            B = 11
        }

        public static readonly double[] RootNotes = {
            16.35, // C
            17.32, // C#
            18.35, // D
            19.45, // D#
            20.60, // E
            21.83, // F
            23.12, // F#
            24.50, // G
            25.96, // G#
            27.50, // A
            29.14, // A#
            30.87  // B
        };

        public static double HZ(Key key, int octave = 4)
        {
            return HZ((int)key, octave);    
        }

        public static double HZ(int key, int octave = 4) 
        {
            return RootNotes[key] * Math.Pow(2, octave);
        }
    }
}
