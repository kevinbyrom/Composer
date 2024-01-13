using System;
using System.Collections.Generic;


namespace Composer
{
    public struct Signal
    {
        static public Signal Zero => new Signal(0.0);
        static public Signal Max => new Signal(1.0);
        static public Signal Min => new Signal(-1.0);

        private double val;

        public double Value
        {
            get
            {
                return val;
            }
            set
            {
                double v = value;
                v = Math.Min(1.0, v);
                v = Math.Max(-1.0, v);
                val = v;
            }
        }

        public Signal(double v)
        {
            this.val = v;
        }

        public override bool Equals(object obj)
        {
            if (obj is Signal target)
                return this.Value == target.Value;

            return false;
        }

        public override string ToString()
        {
            return String.Format($"{0.00}");
        }


        #region Operator Overloads

        public static Signal operator+ (Signal a, Signal b)
        {
            return new Signal() { Value = a.Value + b.Value };
        }

        public static Signal operator +(Signal a, double b)
        {
            return new Signal() { Value = a.Value + b };
        }
        
        public static Signal operator -(Signal a, Signal b)
        {
            return new Signal() { Value = a.Value - b.Value };
        }

        public static Signal operator -(Signal a, double b)
        {
            return new Signal() { Value = a.Value - b };
        }

        public static Signal operator *(Signal a, Signal b)
        {
            return new Signal() { Value = a.Value * b.Value };
        }

        public static Signal operator *(Signal a, double b)
        {
            return new Signal() { Value = a.Value * b };
        }

        public static Signal operator /(Signal a, Signal b)
        {
            return new Signal() { Value = a.Value / b.Value };
        }

        public static Signal operator /(Signal a, double b)
        {
            return new Signal() { Value = a.Value / b };
        }

        #endregion

    }

    public interface ISignalSource 
    {
        Signal GetValue(double time);
    }

    public interface ISignalTarget
    {
        void Write(double time, Signal signal);
        void Write(double time, IEnumerable<Signal> signals);
        void Flush();
    }

    public interface ISignalTransform
    {
        bool CanClose { get; }
        Signal Transform(double time, Signal signal);
        void Release();
    }
}
