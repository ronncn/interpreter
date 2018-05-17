using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter解释器
{
    public class SNumber : SObject
    {
        private readonly Int64 value;
        public SNumber(Int64 val)
        {
            this.value = val;
        }

        public override string ToString()
        {
            return this.value.ToString();
        }

        public static implicit operator Int64(SNumber number)
        {
            return number.value;
        }

        public static implicit operator SNumber(Int64 value)
        {
            return new SNumber(value);
        }
    }
}
