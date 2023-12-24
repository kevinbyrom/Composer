using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Composer.Operators
{
    public class DivideOperator : ISampleSource
    {
        public ISampleSource Operand1 { get; set; }
        public ISampleSource Operand2 { get; set; }

        public DivideOperator()
        {
        }

        public DivideOperator(ISampleSource oper1, ISampleSource oper2)
        {
            this.Operand1 = oper1;
            this.Operand2 = oper2;
        }

        public Sample GetValue(double time)
        {
            return Operand1.GetValue(time) / Operand2.GetValue(time);
        }
    }

    public static class DivideOperatorExtensions
    {
        public static ISampleSource Divide(this ISampleSource source, ISampleSource target)
        {
            return new DivideOperator(source, target);
        }
    }
}
