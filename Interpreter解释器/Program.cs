using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter解释器
{
    static class Program
    {
        static void Main(string[] cmdArgs)
        {
            Console.WriteLine("解释器：");
            //Output(Input());
            SScope sScope = new SScope(parent: null)
                .BuildIn("+", (args, scope) =>
                {
                    var numbers = args.Select(obj => obj.Evaluate(scope)).Cast<SNumber>();
                    return numbers.Sum(n => n);
                })
                .BuildIn("-", (args, scope) =>
                {
                    var numbers = args.Select(obj => obj.Evaluate(scope)).Cast<SNumber>().ToArray();
                    Int64 firstValue = numbers[0];
                    if (numbers.Length == 1)
                    {
                        return -firstValue;
                    }
                    return firstValue - numbers.Skip(1).Sum(s => s);
                });
            KeepInterpretingInConsole( sScope ,(code,scope) => ParseAsIScheme(code).Evaluate(scope));
        }

        public static void KeepInterpretingInConsole(this SScope scope, Func<String, SScope, SObject> evaluate)
        {
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(">> ");
                    String code;
                    if (!String.IsNullOrWhiteSpace(code = Console.ReadLine()))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(">> " + evaluate(code, scope));
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(">> " + ex.Message);
                }
            }
        }   
        public static IEnumerable<T> Evaluate<T>(this IEnumerable<SExpression> expressions, SScope scope)
where T : SObject
        {
            return expressions.Evaluate(scope).Cast<T>();
        }
        public static IEnumerable<SObject> Evaluate(this IEnumerable<SExpression> expressions, SScope scope)
        {
            return expressions.Select(exp => exp.Evaluate(scope));
        }

        //输出
        public static void Output(string text)
        {
            if(text == "quit")
            {
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine(">>> " + text);
                Output(PrettyPrint(Tokenize(Input())));
            }
        }
        //输入
        public static string Input()
        {
            Console.Write(">>> ");
            return Console.ReadLine();
        }


        //加法
        public static int Add(int a, int b)
        {
            return a + b;
        }

        //阶乘
        public static int Factorial(int n)
        {
            if(n == 1)
            {
                return 1;
            }
            else
            {
                return n * Factorial(n - 1);
            }
        }

        //平方
        public static int Square(int x)
        {
            return x * x;
        }

        public static int SumSquare(int a, int b)
        {
            return Square(a) + Square(b);
        }

        //生成Token
        public static string[] Tokenize(string str)
        {
            String[] Tokens = str.Replace("(", " ( ").Replace(")", " ) ").Split(" \t\r\n".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            return Tokens;
        }

        //扩展String.Join的方法
        public static String Join(String separator, IEnumerable<Object> values)
        {
            return String.Join(separator, values);
        }

        //打印字符串数组
        public static string PrettyPrint(String[] lexes)
        {
            return "[" + Join(", ",lexes.Select(s => "'" + s + "'")) + "]";
        }

        //抽象语法树的构建过程
        public static SExpression ParseAsIScheme(this String code)
        {
            SExpression program = new SExpression(value: "", parent: null);
            SExpression current = program;
            foreach (var lex in Tokenize(code))
            {
                if (lex == "(")
                {
                    SExpression newNode = new SExpression(value: "(", parent: current);
                    current.Children.Add(newNode);
                    current = newNode;
                }
                else if (lex == ")")
                {
                    current = current.Parent;
                }
                else
                {
                    current.Children.Add(new SExpression(value: lex, parent: current));
                }
            }
            return program.Children[0];
        }

        public static String[] ForeachSEx(SExpression sExpression)
        {
            String[] tokens = null;
            return tokens;
        }
    }
}
