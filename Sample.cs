using System;

namespace Composer
{
    public struct Sample
    {
        static public Sample Zero => new Sample(0f, 0f);
        public float Left;
        public float Right;

        public float BoundedLeft 
        {
            get
            {
                return this.Left;
            }
            set
            {
                float val = value;
                val = Math.Min(1, val);
                val = Math.Max(-1, val);
                this.Left = val;
            }
        }
        public float BoundedRight
        {
            get
            {
                return this.Right;
            }
            set
            {
                float val = value;
                val = Math.Min(1, val);
                val = Math.Max(-1, val);
                this.Right = val;
            }
        }

        public Sample(float left, float right)
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

        public static Sample operator +(Sample a, float b)
        {
            return new Sample() { BoundedLeft = a.Left + b, BoundedRight = a.Right + b };
        }
        
        public static Sample operator -(Sample a, Sample b)
        {
            return new Sample() { BoundedLeft = a.Left - b.Left, BoundedRight = a.Right - b.Right };
        }

        public static Sample operator -(Sample a, float b)
        {
            return new Sample() { BoundedLeft = a.Left - b, BoundedRight = a.Right - b };
        }

        public static Sample operator *(Sample a, Sample b)
        {
            return new Sample() { BoundedLeft = a.Left * b.Left, BoundedRight = a.Right * b.Right };
        }

        public static Sample operator *(Sample a, float b)
        {
            return new Sample() { BoundedLeft = a.Left * b, BoundedRight = a.Right * b };
        }

        public static Sample operator /(Sample a, Sample b)
        {
            return new Sample() { BoundedLeft = a.Left / b.Left, BoundedRight = a.Right / b.Right };
        }

        public static Sample operator /(Sample a, float b)
        {
            return new Sample() { BoundedLeft = a.Left / b, BoundedRight = a.Right / b };
        }

        #endregion

    }
}
