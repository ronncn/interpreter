using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter解释器
{
    public class SObject
    {
        public static implicit operator SObject(Int64 value)
        {
            return (SNumber)value;
        }

        public static implicit operator SObject(Boolean value)
        {
            return (SBool)value;
        }

    }
}
