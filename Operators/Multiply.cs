using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Composer.Operators
{
    public class MultiplyOperator : ISignalSource
    {
        public ISignalSource Operand1 { get; set; }
        public ISignalSource Operand2 { get; set; }

        public MultiplyOperator()
        {
        }

        public MultiplyOperator(ISignalSource oper1, ISignalSource oper2)
        {
            this.Operand1 = oper1;
            this.Operand2 = oper2;
        }

        public Signal GetValue(double time)
        {
            return Operand1.GetValue(time) * Operand2.GetValue(time);
        }
    }

    public static class MultiplyOperatorExtensions
    {
        public static ISignalSource Multiply(this ISignalSource source, ISignalSource target)
        {
            return new MultiplyOperator(source, target);
        }
    }
}
