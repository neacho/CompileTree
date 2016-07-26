using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompileUtils
{
    public class ExpressionAnalyse
    {
        readonly string symbols = "+-*/()";
        private int cursor = 0;
        public string Expression { get;}
        public CustomNode Node { get; }
        private ExpressionAnalyse() { }

        public static ExpressionAnalyse GetExpressionAnalyseDriver(string expression)
        {
            ExpressionAnalyse analyse= new ExpressionAnalyse();
            analyse.Compile(expression);
            return analyse;
        }

        private void Compile(string expression)
        {
            int index = 0;
            string left = "";
            string right = "";
            string func = "";
            string temp_expression = "";
            bool flag = false;

            //将表达式解析分为两部分 判断表达式中是否包含括号 如果包含则以括号为边界，否则则以最开始的表达式为边界
            if (expression[0] == '(')
            {
                int count = 1;
                #region
                for (int i = 1; i < expression.Length; i++)
                {
                    if (expression[i] == '(')
                    {
                        count++;
                    }

                    if (expression[i] == ')')
                    {
                        count--;
                        if (count == 0)
                        {
                            index = i;
                            break;
                        }
                    }
                }
                #endregion

                if (index != expression.Length - 1)
                {
                    left = expression.Substring(1, index - 1);
                    right = expression.Substring(index + 2, expression.Length - index - 2);
                    func = expression[index + 1].ToString();
                }
                else
                {
                    left = expression.Substring(1, index - 1);
                }
            }
            else
            {
                if (expression[0] == '+' || expression[0] == '-')
                {
                    if (expression[1] == '(')
                    {
                        int count = 1;
                        for (int i = 2; i < expression.Length; i++)
                        {
                            if (expression[i] == '(')
                            {
                                count++;
                            }

                            if (expression[i] == ')')
                            {
                                count--;
                                if (count == 0)
                                {
                                    index = i;
                                    if (index != expression.Length - 1)
                                    {
                                        left = expression.Substring(0, index+1);
                                        right = expression.Substring(index + 2, expression.Length - index - 2);
                                        func = expression[index + 1].ToString();
                                    }
                                    else
                                    {
                                        left = expression.Substring(2, index - 2);
                                        func = expression[0].ToString();
                                    }
                                    flag = true;
                                    break;
                                }
                            }
                        }
                        if (!flag) left = expression;
                    }
                    else
                    {
                        Formal(expression, out func, out left, out right);
                    }
                }
                else
                {
                    Formal(expression, out func, out left, out right);
                }
            }
            cursor++;
            Console.WriteLine("第{0}次  left:{1}",cursor, left);
            Console.WriteLine("第{0}次  right:{1}", cursor, right);
            Console.WriteLine("第{0}次  func:{1}", cursor, func);

            if (Check(left)) Compile(left);
            if (Check(right)) Compile(right);
        }

        void Formal(string expression,out string func,out string left,out string right)
        {
            int index = 0;
            int i=0,j=0;
            bool flag = false;
            func = "";
            left = "";
            right = "";
            for (i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(')
                {
                    index = i;
                    func = expression.Substring(0, index);
                    int count = 1;
                    for (j = index + 1; j < expression.Length; j++)
                    {
                        if (expression[j] == ',') index = j;
                        if (expression[j] == '(')
                        {
                            count++;
                        }

                        if (expression[j] == ')')
                        {
                            count--;
                            if (count == 0)
                            {
                                break;
                            }
                        }
                    }
                    left = expression.Substring(i + 1, index - i - 1);
                    right = expression.Substring(index + 1, j - index - 1);
                    flag = true;
                    break;
                }
                else if (symbols.Contains(expression[i]))
                {
                    index = i;
                    left = expression.Substring(0, index);
                    right = expression.Substring(index + 1, expression.Length - index - 1);
                    func = expression[index].ToString();
                    flag = true;
                    break;
                }
            }
            if (!flag) left = expression;
        }

        bool Check(string expression)
        {
            for(int i = 0; i < symbols.Length; i++)
            {
                if (expression.Contains(symbols[i])){
                    return true;
                }
            }
            return false;
        }

    }
}
