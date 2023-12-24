using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Composer.Operators
{
    public class SubtractOperator : ISampleSource
    {
        public ISampleSource Operand1 { get; set; }
        public ISampleSource Operand2 { get; set; }

        public SubtractOperator()
        {
        }

        public SubtractOperator(ISampleSource oper1, ISampleSource oper2)
        {
            this.Operand1 = oper1;
            this.Operand2 = oper2;
        }

        public Sample GetValue(double time)
        {
            return Operand1.GetValue(time) - Operand2.GetValue(time);
        }
    }

    public static class SubtractOperatorExtensions
    {
        public static ISampleSource Subtract(this ISampleSource source, ISampleSource target)
        {
            return new SubtractOperator(source, target);
        }
    }
}
