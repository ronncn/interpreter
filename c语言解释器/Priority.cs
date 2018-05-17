using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c语言解释器
{
    /// <summary>
    /// 符号优先级
    /// </summary>
    public static class Priority
    {
        private static Dictionary<string, int> priority = new Dictionary<string, int>
        {
            { "/",3},
            { "*",3},
            { "+",4},
            { "-",4},
        };

        public static int GetPriority(string fh)
        {
            //判断传入的字符串是否是优先级表中的符号
            if (priority.ContainsKey(fh))
            {
                return priority[fh];
            }
            return 999;
        }

        public static bool Contains(string fh)
        {
            return priority.ContainsKey(fh);
        }

    }
}
