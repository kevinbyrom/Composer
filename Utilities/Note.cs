using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composer.Utilities
{
    public static class NoteUtil
    {
        public static double ToFrequency(int note, double rootHz = 440.0)
        {
            return (double)(440.0 * Math.Pow(2, (note - 9) / 12.0f));
        }
    }
}
