using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter解释器
{
    public class SList : SObject, IEnumerable<SObject>
    {
        private readonly IEnumerable<SObject> values;

        public SList(IEnumerable<SObject> values)
        {
            this.values = values;
        }
        public static String Join(String separator, IEnumerable<Object> values)
        {
            return String.Join(separator, values);
        }

        public override string ToString()
        {
            return "(list " + Join(" ", this.values) + ")";
        }

        public IEnumerator<SObject> GetEnumerator()
        {
            return this.values.GetEnumerator();
        }
         IEnumerator IEnumerable.GetEnumerator()
        {
            return this.values.GetEnumerator();
        }
    }
}
