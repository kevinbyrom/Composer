﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Composer.Utilities;


namespace Composer
{
    public static class SignalMixer 
    {
        public static Signal Mix(IEnumerable<Signal> signals)
        {
            double val = 0;
            int numActive = 0;

            foreach (var s in signals)
            {
                if (s.Value > val)
                    val = s.Value;
                else if (s.Value < val)
                    val = s.Value;

                //val += s.Value;

                if (s.Value != 0)
                    numActive++;
            }

            Signal mixed = new Signal(val);

            //mixed.Value = val / (double)numActive;

            return mixed;
        }
    }
}
