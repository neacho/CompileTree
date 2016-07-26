using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompileUtils
{
    /// <summary>
    /// 参数值（或函数名称）、返回值
    /// </summary>
    public class CustomNode: ICalculators
    {
        public object Parameter { get; set; }

        public object Value { get; set; }

        public CustomNode LeftNode { get; set; }

        public CustomNode RightNode { get; set; }

        public void CalCulator()
        {
            
        }
    }
}
