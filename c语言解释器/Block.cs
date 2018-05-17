using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c语言解释器
{
    /// <summary>
    /// 代码块
    /// </summary>
    public class Block
    {
        public String Value { get; set; }

        public Block Parent { get; set; }
        
        //public List<Block> Children { get; private set; }
        public Block LeftChild { get; set; }
        public Block RightChild { get; set; }


        public Block(String value,Block parent)
        {
            this.Value = value;
            this.Parent = parent;
        }

        public override string ToString()
        {
            return "'" + Value + "'";
        }


    }
}
