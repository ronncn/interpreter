using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter解释器
{
    /// <summary>
    /// S表达式
    /// </summary>
    public class SExpression
    {
        public String Value { get; private set; }

        public List<SExpression> Children { get; private set; }

        public SExpression Parent { get; private set; }

        //构造函数
        public SExpression(string value, SExpression parent)
        {
            this.Value = value;
            this.Children = new List<SExpression>();
            this.Parent = parent;
        }
        public String Join(String separator, IEnumerable<Object> values)
        {
            return String.Join(separator, values);
        }

        public override string ToString()
        {
            if (this.Value == "(")
            {
                return "(" + Join(" ",  Children) + ")";
            }
            else
            {
                return this.Value;
            }
        }
        public SObject Evaluate(SScope scope)
        {
            if (this.Children.Count == 0)
            {
                Int64 number;
                if (Int64.TryParse(this.Value, out number))
                {
                    return number;
                }
            }
            else
            {
                SExpression first = this.Children[0];
                if (SScope.BuiltinFunctions.ContainsKey(first.Value))
                {
                    var arguments = this.Children.Skip(1).ToArray();
                    return SScope.BuiltinFunctions[first.Value](arguments, scope);
                }
            }
            throw new Exception("THIS IS JUST TEMPORARY!");
        }
    }
}
