using System;
using System.Collections.Generic;


namespace Composer
{
    public struct Signal
    {
        static public Signal Zero => new Signal(0.0);
        static public Signal Max => new Signal(1.0);
        static public Signal Min => new Signal(-1.0);

        public double Value { get; set; }
        public double BoundedValue 
        {
            get
            {
                return this.Value;
            }
            set
            {
                double val = value;
                val = Math.Min(1, val);
                this.Value = val;
            }
        }

        public Signal(double val)
        {
            this.Value = val;
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
            return new Signal() { BoundedValue = a.Value + b.Value };
        }

        public static Signal operator +(Signal a, double b)
        {
            return new Signal() { BoundedValue = a.Value + b };
        }
        
        public static Signal operator -(Signal a, Signal b)
        {
            return new Signal() { BoundedValue = a.Value - b.Value };
        }

        public static Signal operator -(Signal a, double b)
        {
            return new Signal() { BoundedValue = a.Value - b };
        }

        public static Signal operator *(Signal a, Signal b)
        {
            return new Signal() { BoundedValue = a.Value * b.Value };
        }

        public static Signal operator *(Signal a, double b)
        {
            return new Signal() { BoundedValue = a.Value * b };
        }

        public static Signal operator /(Signal a, Signal b)
        {
            return new Signal() { BoundedValue = a.Value / b.Value };
        }

        public static Signal operator /(Signal a, double b)
        {
            return new Signal() { BoundedValue = a.Value / b };
        }

        #endregion

    }

    public interface ISignalSource 
    {
        Signal GetValue(double time);
    }

    public interface ISignalTarget
    {
        void Write(SampleTime time, Signal signal);
        void Write(SampleTime time, IEnumerable<Signal> signals);
        void Flush();
    }

    public interface ISignalTransform
    {
        bool CanClose { get; }
        Signal Transform(SampleTime time, Signal signal);
        void Release();
    }
}
