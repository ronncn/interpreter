using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter解释器
{
    public class SFunction : SObject
    {
        public SExpression Body { get; private set; }

        public String[] Parameters { get; private set; }

        public SScope Scope { get; private set; }

        public Boolean IsPartial
        {
            get
            {
                return this.ComputeFilledParameters().Length > 1 && this.ComputeFilledParameters().Length < this.Parameters.Length ? true : false;
            }
        }

        public SFunction(SExpression sExpression, String[] parameters, SScope scope)
        {
            this.Body = sExpression;
            this.Parameters = parameters;
            this.Scope = scope;
        }

        public SObject Evaluate()
        {
            String[] filledParameters = this.ComputeFilledParameters();
            if(filledParameters.Length < Parameters.Length)
            {
                return this;
            }
            else
            {
                return this.Body.Evaluate(this.Scope);
            }
        }

        //扩展String.Join的方法
        private String Join(String separator, IEnumerable<Object> values)
        {
            return String.Join(separator, values);
        }

        public override string ToString()
        {
            return String.Format("(func({0}){1})",
                Join(" ",this.Parameters.Select(p =>
                {
                    SObject value = null;
                    if ((value = this.Scope.Find(p)) != null)
                    {
                        return p + ":" + value;
                    }
                    return p;
                })), this.Body);
        }

        private String[] ComputeFilledParameters()
        {
            return this.Parameters.Where(p => Scope.Find(p) != null).ToArray();
        }
    }
}
