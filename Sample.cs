using System;
using System.Collections.Generic;


namespace Composer
{
    public struct Sample
    {
        static public Sample Zero => new Sample(0, 0);
        public double Left;
        public double Right;

        public double BoundedLeft 
        {
            get
            {
                return this.Left;
            }
            set
            {
                double val = value;
                val = Math.Min(1, val);
                val = Math.Max(-1, val);
                this.Left = val;
            }
        }
        public double BoundedRight
        {
            get
            {
                return this.Right;
            }
            set
            {
                double val = value;
                val = Math.Min(1, val);
                val = Math.Max(-1, val);
                this.Right = val;
            }
        }

        public Sample(double val) : this(val, val)
        {
        }

        public Sample(double left, double right)
        {
            this.Left = left;
            this.Right = right;
        }

        public override bool Equals(object obj)
        {
            if (obj is Sample target)
                return this.Left == target.Left && this.Right == target.Right;

            return false;
        }

        #region Operator Overloads

        public static Sample operator+ (Sample a, Sample b)
        {
            return new Sample() { BoundedLeft = a.Left + b.Left, BoundedRight = a.Right + b.Right };
        }

        public static Sample operator +(Sample a, double b)
        {
            return new Sample() { BoundedLeft = a.Left + b, BoundedRight = a.Right + b };
        }
        
        public static Sample operator -(Sample a, Sample b)
        {
            return new Sample() { BoundedLeft = a.Left - b.Left, BoundedRight = a.Right - b.Right };
        }

        public static Sample operator -(Sample a, double b)
        {
            return new Sample() { BoundedLeft = a.Left - b, BoundedRight = a.Right - b };
        }

        public static Sample operator *(Sample a, Sample b)
        {
            return new Sample() { BoundedLeft = a.Left * b.Left, BoundedRight = a.Right * b.Right };
        }

        public static Sample operator *(Sample a, double b)
        {
            return new Sample() { BoundedLeft = a.Left * b, BoundedRight = a.Right * b };
        }

        public static Sample operator /(Sample a, Sample b)
        {
            return new Sample() { BoundedLeft = a.Left / b.Left, BoundedRight = a.Right / b.Right };
        }

        public static Sample operator /(Sample a, double b)
        {
            return new Sample() { BoundedLeft = a.Left / b, BoundedRight = a.Right / b };
        }

        #endregion

    }

    public interface ISampleSource 
    {
        Sample CurrSample { get; }
    }

    public interface ISampleTarget
    {
        void Write(Sample sample);
        void Write(IEnumerable<Sample> samples);
    }

    public interface ISampleTransform
    {
        Sample Transform(SampleTime time, Sample sample);
    }
}
