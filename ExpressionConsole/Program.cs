/*
 * 如何解析表达式为单个表达式单元 
 * 1.将表达式解析分为两部分  判断表达式中是否包含括号 如果包含则以括号为边界，否则则以最开始的表达式为边界
 * 2.两部分的表达式再次进行同样操作，直到最后解析完成
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompileUtils;

namespace ExpressionConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            #region//表达式 简单测试
            string expression1 = "a+b+c+d";
            string expression2 = "(a+(c+d))+e";
            string expression3 = "a+(-b)";
            string expression4 = "-a+b";
            string expression5 = "-(a+b)+c";
            string expression6 = "((-b)+a)/d";
            string expression7 = "aaa+ddd";
            string expression8 = "-(-a)+b";
            string expression9 = "Add(-a,b)";
            string expression10 = "Add(Double(a,b),b)";
            string expression11 = "a";
            string expression12 = "-a";
            Console.WriteLine("----expression1:{0}", expression1);
            SplitExpression(expression1);
            Console.WriteLine("----expression2:{0}", expression2);
            SplitExpression(expression2);
            Console.WriteLine("----expression3:{0}", expression3);
            SplitExpression(expression3);
            Console.WriteLine("----expression4:{0}", expression4);
            SplitExpression(expression4);
            Console.WriteLine("----expression5:{0}", expression5);
            SplitExpression(expression5);
            Console.WriteLine("----expression6:{0}", expression6);
            SplitExpression(expression6);
            Console.WriteLine("----expression7:{0}", expression7);
            SplitExpression(expression7);
            Console.WriteLine("----expression8:{0}", expression8);
            SplitExpression(expression8);
            Console.WriteLine("----expression9:{0}", expression9);
            SplitExpression(expression9);
            Console.WriteLine("----expression10:{0}", expression10);
            SplitExpression(expression10);
            Console.WriteLine("----expression11:{0}", expression11);
            SplitExpression(expression11);
            Console.WriteLine("----expression12:{0}", expression12);
            SplitExpression(expression12);
            #endregion

            #region//表达式测试
            string[] testcol = { expression1, expression2, expression3, expression4, expression5, expression6, expression7,
                                 expression8,expression9,expression10,expression11,expression12};
            bool flag = true;
            for(int i = 0; i < testcol.Length; i++)
            {
                Console.WriteLine("^^^^^^^^^^{0}的测试结果：", testcol[i]);
                ExpressionAnalyse analyse = ExpressionAnalyse.GetExpressionAnalyseDriver(testcol[i]);
            }
            
            #endregion

            Console.Read();
        }

        class Node
        {
            public object Parameter { get; set; }

            public object Value { get; set; }

            public Node[] Nodes { get; set; }
        }

        static void Expresion(string expression)
        {
            string leftExpresion = "";
            string rigthExpression = "";
            string FuncName = "";


        }

        static void SplitExpression(string expression)
        {
            string sysm = "+-*/";
            int index = 0;
            string left = "";
            string rigth = "";
            string func = "";
            bool flag = false;
            //将表达式解析分为两部分 判断表达式中是否包含括号 如果包含则以括号为边界，否则则以最开始的表达式为边界
            if (expression[0] == '(')
            {
                int count = 1;
                for(int i = 1; i < expression.Length; i++)
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
                left = expression.Substring(1, index - 1);
                rigth = expression.Substring(index + 2, expression.Length - index - 2);
                func = expression[index + 1].ToString();
            }
            else
            {
                if (expression[0] == '+' || expression[0] == '-')
                {
                    if(expression[1]=='(')
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
                                    left = expression.Substring(0, index + 1);
                                    rigth = expression.Substring(index + 2, expression.Length - index - 2);
                                    func = expression[index + 1].ToString();
                                    flag = true;
                                    break;
                                }
                            }
                        }
                        if (!flag) left = expression;
                    }
                    else
                    {
                        for (int i = 1; i < expression.Length; i++)
                        {
                            if (sysm.Contains(expression[i]))
                            {
                                index = i;
                                left = expression.Substring(0, index);
                                rigth = expression.Substring(index + 1, expression.Length - index - 1);
                                func = expression[index].ToString();
                                flag = true;
                                break;
                            }
                        }
                        if (!flag) left = expression;
                    }
                }
                else
                {
                    for (int i = 1; i < expression.Length; i++)
                    {
                        if (expression[i] == '(')
                        {
                            index = i;
                            int end = 0;
                            func = expression.Substring(0, index);
                            int count = 1;
                            for(int j = index+1; j < expression.Length; j++)
                            {
                                if (expression[j]== ',') index = j;
                                if (expression[j] == '(')
                                {
                                    count++;
                                }

                                if (expression[j] == ')')
                                {
                                    count--;
                                    if (count == 0)
                                    {
                                        end = j;
                                        break;
                                    }
                                }
                            }
                            left = expression.Substring(i + 1, index - i - 1);
                            rigth = expression.Substring(index + 1, end - index - 1);
                            flag = true;
                            break;
                        }
                        if (sysm.Contains(expression[i]))
                        {
                            index = i;
                            left = expression.Substring(0, index);
                            rigth = expression.Substring(index + 1, expression.Length - index - 1);
                            func = expression[index].ToString();
                            flag = true;
                            break;
                        }
                    }
                    if (!flag) left = expression;
                    
                }
            }
            Console.WriteLine("left:{0}",left);
            Console.WriteLine("right:{0}", rigth);
            Console.WriteLine("func:{0}", func);
        }
    }
}
