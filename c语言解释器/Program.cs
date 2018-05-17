using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c语言解释器
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("C语言解释器：");
            KeepConsole();
        }

        /// <summary>
        /// 保持控制台显示
        /// </summary>
        static void KeepConsole()
        {
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(">>> ");
                    String code;
                    if(!String.IsNullOrWhiteSpace(code = Console.ReadLine()))
                    {
                        SendMessage(Operation(code).ToString(), MessageType.success);
                    }
                }
                catch (Exception ex)
                {
                    //出现错误显示红色
                    SendMessage(ex.Message, MessageType.error);
                }
            }
        }
        public enum MessageType { error, warning, success};
        static void SendMessage(string msg , MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case MessageType.warning:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case MessageType.success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
            Console.WriteLine(">>> " + msg);
        }

        static String[] Tokenize(String text)
        {
            String[] tokens = text.Replace("+", " + ")
                                  .Replace("-", " - ")
                                  .Replace("*", " * ")
                                  .Replace("/", " / ")
                                  .Split(" \r\n\t".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            return tokens;
        }

        static String PrettyPrint(String[] strArray)
        {
            return "[" + String.Join(", ", strArray.Select(s => "'" + s +"'")) + "]";
        }

        static int Operation(String code)
        {
            //var fh = new List<string> { "+", "-", "*", "/" };
            var kh = new List<string> { "(", ")" };
            List<String> outputs = new List<string>();
            Stack<string> express = new Stack<string>();
            foreach (var lex in Tokenize(code))
            {
                // 判断是操作数，放入输出中
                if (!Priority.Contains(lex))
                {
                    outputs.Add(lex);
                }
                else
                {
                    if(express.Count == 0)
                    {
                        express.Push(lex);
                    }
                    else
                    {
                        //判断lex的符号和栈顶元素的优先级
                        if(Priority.GetPriority(lex) > Priority.GetPriority(express.Peek()))
                        {
                            outputs.Add(express.Pop());
                        }
                        express.Push(lex);
                    }
                }
            }
            while(express.Count != 0)
            {
                outputs.Add(express.Pop());
            }
            Stack<int> sumStack = new Stack<int>();
            foreach(string lex in outputs)
            {
                //判断是否是符号
                if (Priority.Contains(lex))
                {
                    int b = sumStack.Pop();
                    int a = sumStack.Pop();

                    sumStack.Push(cal(a, b, lex));
                }
                else
                {
                    sumStack.Push(int.Parse(lex));
                }
            }
            return sumStack.Peek();
        }
        private static int cal(int a, int b, string flag)
        {
            int result = 0;

            switch (flag)
            {
                case "+":
                    {
                        result = a + b;
                        break;
                    }
                case "-":
                    {
                        result = a - b;
                        break;
                    }
                case "*":
                    {
                        result = a * b;
                        break;
                    }
                case "/":
                    {
                        result = a / b;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return result;
        }
    }
}
