using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Composer.Operators
{
    public class AddOperator : ISampleSource
    {
        public ISampleSource Operand1 { get; set; }
        public ISampleSource Operand2 { get; set; }

        public AddOperator() 
        { 
        }

        public AddOperator(ISampleSource oper1, ISampleSource oper2)
        {
            this.Operand1 = oper1;
            this.Operand2 = oper2;
        }

        public Sample GetValue(double time) 
        {
            return Operand1.GetValue(time) + Operand2.GetValue(time);
        }   
    }

    public static class AddOperatorExtensions
    {
        public static ISampleSource Add(this ISampleSource source, ISampleSource target)
        {
            return new AddOperator(source, target);
        }
    }
}
