using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter解释器
{
    public class SBool : SObject
    {
        public static readonly SBool False = new SBool();
        public static readonly SBool True = new SBool();

        public override string ToString()
        {
            return ((Boolean)this).ToString();
        }

        public static implicit operator Boolean(SBool value)
        {
            return value == SBool.True;
        }

        public static implicit operator SBool(Boolean value)
        {
            return value ? True : False;
        }
    }
}
