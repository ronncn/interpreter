using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter解释器
{
    public class SScope
    {
        public SScope Parent { get; private set; }

        private Dictionary<String, SObject> variableTable;

        public SScope(SScope parent)
        {
            this.Parent = parent;
            this.variableTable = new Dictionary<string, SObject>();
        }

        public SObject Find(String name)
        {
            SScope current = this;
            while(current != null)
            {
                if (current.variableTable.ContainsKey(name))
                {
                    return current.variableTable[name];
                }
                current = current.Parent;
            }
            throw new Exception(name + " is not defined.");
        }

        //定义
        public SObject Define(String name, SObject value)
        {
            this.variableTable.Add(name, value);
            return value;
        }
        private static Dictionary<String, Func<SExpression[], SScope, SObject>> builtinFunctions =
            new Dictionary<String, Func<SExpression[], SScope, SObject>>();
        public static Dictionary<String, Func<SExpression[], SScope, SObject>> BuiltinFunctions
        {
            get { return builtinFunctions; }
        }
        //内置操作
        public SScope BuildIn(string name, Func<SExpression[], SScope, SObject> builtinFuntion)
        {
            SScope.builtinFunctions.Add(name, builtinFuntion);
            return this;
        }
        public SScope SpawnScopeWith(String[] names, SObject[] values)
        {
            //(names.Length >= values.Length).OrThrows("Too many arguments.");
            SScope scope = new SScope(this);
            for (Int32 i = 0; i < values.Length; i++)
            {
                scope.variableTable.Add(names[i], values[i]);
            }
            return scope;
        }
        public SObject FindInTop(String name)
        {
            if (variableTable.ContainsKey(name))
            {
                return variableTable[name];
            }
            return null;
        }
    }
}
