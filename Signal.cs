using System;
using System.Collections.Generic;


namespace Composer
{
    public struct Signal
    {
        static public Signal None => new Signal(0.0);
        //static public Signal Zero => new Signal(0.0, true);
        static public Signal Max => new Signal(1.0, true);
        static public Signal Min => new Signal(-1.0, true);

        private double val = 0.0;

        public bool IsActive { get; set; } = false;
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

        public Signal()
        {
            this.val = Signal.None.val;
            this.IsActive = false;
        }
        
        public Signal(Signal s)
        {
            this.val = s.val;
            this.IsActive = s.IsActive;
        }

        public Signal(double v, bool isActive = false)
        {
            this.val = v;
            this.IsActive = isActive;
        }

        public override bool Equals(object obj)
        {
            if (obj is Signal target)
                return this.Value == target.Value;

            return false;
        }

        public override string ToString()
        {
            return String.Format($"{this.val: 0.00} - {this.IsActive}");
        }


        #region Operator Overloads

        public static Signal operator+ (Signal a, Signal b)
        {
            return new Signal() { Value = a.Value + b.Value, IsActive = a.IsActive || b.IsActive };
        }

        public static Signal operator +(Signal a, double b)
        {
            return new Signal() { Value = a.Value + b, IsActive = a.IsActive };
        }
        
        public static Signal operator -(Signal a, Signal b)
        {
            return new Signal() { Value = a.Value - b.Value, IsActive = a.IsActive || b.IsActive };
        }

        public static Signal operator -(Signal a, double b)
        {
            return new Signal() { Value = a.Value - b, IsActive = a.IsActive };
        }

        public static Signal operator *(Signal a, Signal b)
        {
            return new Signal() { Value = a.Value * b.Value , IsActive = a.IsActive || b.IsActive };
        }

        public static Signal operator *(Signal a, double b)
        {
            return new Signal() { Value = a.Value * b, IsActive = a.IsActive };
        }

        public static Signal operator /(Signal a, Signal b)
        {
            return new Signal() { Value = a.Value / b.Value, IsActive = a.IsActive || b.IsActive };
        }

        public static Signal operator /(Signal a, double b)
        {
            return new Signal() { Value = a.Value / b, IsActive = a.IsActive };
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
