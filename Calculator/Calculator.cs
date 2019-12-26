using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Windows.Forms;

namespace Calculator
{
    class Calculator
    {
        /*是否为纯数字。正则表达式实现*/
        public static bool isNumber(string tmp)
        {
            return Regex.IsMatch(tmp, @"[0-9]+[.]{0,1}[0-9]*");
        }

        /*是否为需拆分的运算符+-*^/() */
        public static bool isOp(string tmp)
        {
            bool bRet = false;
            switch (tmp)
            {
                case "+":
                case "-":
                case "*":
                case "/":
                case "^":
                case "(":
                case ")":
                case "EXT":
                case "LOG":
                    bRet = true;
                    break;
                default:
                    bRet = false;
                    break;
            }
            return bRet;
        }

        /*是否为一元函数名*/
        public static bool isFunc(string tmp)
        {
            bool isfunc = false;
            switch (tmp)
            {
                case "SIN":
                case "COS":
                case "TAN":
                case "SEC":
                case "CSC":
                case "COT":
                case "SQRT":
                case "FAC":
                case "SQR":
                case "LN":
                    isfunc = true;
                    break;
                default:
                    isfunc = false;
                    break;
            }
            return isfunc;
        }

        /*比较运算符及函数优先级。函数视作运算符进行操作。
    返回值：1 表示 大于，-1 表示 小于，0 表示 相等    */
        public static int compOper(string op1, string op2)
        {
            int i = 0;
            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("+", 1);
            dic.Add("-", 1);
            dic.Add("*", 2);
            dic.Add("/", 2);
            dic.Add("^", 3);
            dic.Add("SIN", 4);
            dic.Add("COS", 4);
            dic.Add("TAN", 4);
            dic.Add("SEC", 4);
            dic.Add("CSC", 4);
            dic.Add("COT", 4);
            dic.Add("SQRT", 4);
            dic.Add("FAC", 4);
            dic.Add("SQR", 4);
            dic.Add("EXT", 4);
            dic.Add("LOG", 4);
            dic.Add("LN", 4);
            dic.Add("(", 100);
            dic.Add(")", 100);
            if (dic[op1] > dic[op2])
                i = 1;
            else if (dic[op1] < dic[op2])
                i = -1;
            else
                i = 0;
            return i;
        }

        /*按照=+-*^/()分隔出元素*/
        public static string splitFunc(string tmp)
        {
            string splitStr = tmp;
            splitStr = splitStr.Replace("=", "\n=\n");
            splitStr = splitStr.Replace("+", "\n+\n");
            splitStr = splitStr.Replace("-", "\n-\n");
            splitStr = splitStr.Replace("*", "\n*\n");
            splitStr = splitStr.Replace("/", "\n/\n");
            splitStr = splitStr.Replace("^", "\n^\n");
            splitStr = splitStr.Replace("(", "\n(\n");
            splitStr = splitStr.Replace(")", "\n)\n");
            splitStr = splitStr.Replace("EXT", "\nEXT\n");
            splitStr = splitStr.Replace("LOG", "\nLOG\n");

            return splitStr;
        }

        /*中缀表达式转后缀表达式*/
        public static string ConvertRPN(string str)
        {
            string RPNstr = "";
            string[] arr = splitFunc(str.ToUpper()).Split('\n');  //分隔后的中缀表达式
            Stack<string> stack = new Stack<string>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (string.IsNullOrEmpty(arr[i]))
                    continue;
                else if (isNumber(arr[i]))
                    RPNstr += arr[i] + ",";     //数字直接写入后缀表达式
                else if (isFunc(arr[i]))
                    stack.Push(arr[i]);
                else if (isOp(arr[i]))  //处理当前是运算符的情况
                {
                    if (stack.Count != 0 && stack.Peek() == "(" && arr[i] != ")")  //栈不为空，最上层为"("，则运算符直接入栈
                        stack.Push(arr[i]);
                    else if (stack.Count != 0 && stack.Peek() == ")")   //栈不为空，最上层为")",出栈直到"("
                    {
                        stack.Pop();
                        while (stack.Peek() != "(")
                        {
                            RPNstr += stack.Pop() + ",";
                        }
                        stack.Pop();                        
                        if (stack.Count() != 0 && isFunc(stack.Peek()))
                            RPNstr += stack.Pop() + ",";
                        stack.Push(arr[i]);
                    }
                    else if (stack.Count != 0 && compOper(arr[i], stack.Peek()) == -1)  //栈不为空，且不是括号，当前运算符优先级比栈顶运算符低
                    {
                        while (stack.Count != 0 && stack.Peek() != "(" && compOper(arr[i], stack.Peek()) == -1)
                            RPNstr += stack.Pop() + ",";
                        if (stack.Count() != 0 && stack.Peek() == "(")
                            stack.Pop();
                        if (stack.Count() != 0)
                            RPNstr += stack.Pop() + ",";
                        stack.Push(arr[i]);
                    }
                    else if (stack.Count != 0 && stack.Peek() != "(" && compOper(arr[i], stack.Peek()) == 0)   //栈不为空，运算符优先级一样
                    {
                        RPNstr += stack.Pop() + ",";
                        stack.Push(arr[i]);
                    }
                    else if (stack.Count != 0 && compOper(arr[i], stack.Peek()) == 1)
                        stack.Push(arr[i]);
                    else
                        stack.Push(arr[i]);
                }
            }
            while (stack.Count > 0)
            {
                if (stack.Peek() == "(" || stack.Peek() == ")")
                    stack.Pop();
                else
                    RPNstr += stack.Pop() + ",";
            }                
            return RPNstr;
        }

        public static dynamic calVal(string num2,string num1,string op)
        {            
            Assembly assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "Operator_DLL.dll");
            Type type = assembly.GetType("Operator_DLL.Operator");            
            switch (op)
            {
                case "+":
                    MethodInfo methed = type.GetMethod("Add");
                    object instance = Activator.CreateInstance(type);
                    var result = methed.Invoke(instance, new object[] { double.Parse(num1), double.Parse(num2) });
                    return result;
                case "-":
                    methed = type.GetMethod("Sub");
                    instance = Activator.CreateInstance(type);
                    result = methed.Invoke(instance, new object[] { double.Parse(num1), double.Parse(num2) });
                    return result;
                case "*":
                    methed = type.GetMethod("Mul");
                    instance = Activator.CreateInstance(type);
                    result = methed.Invoke(instance, new object[] { double.Parse(num1), double.Parse(num2) });
                    return result;
                case "/":
                    if (double.Parse(num2) != 0)
                    {
                        methed = type.GetMethod("Div");
                        instance = Activator.CreateInstance(type);
                        result = methed.Invoke(instance, new object[] { double.Parse(num1), double.Parse(num2) });
                        return result;
                    }
                    else
                    {
                        //MessageBox.Show("Error!");
                        return "Error";
                    }
                case "^":
                    methed = type.GetMethod("Pow");
                    instance = Activator.CreateInstance(type);
                    result = methed.Invoke(instance, new object[] { double.Parse(num1), double.Parse(num2) });
                    return result;
                case "EXT":
                    if (double.Parse(num1) < 0 && double.Parse(num2) % 2 == 0)
                        return "Error";
                    methed = type.GetMethod("Pow");
                    instance = Activator.CreateInstance(type);
                    result = methed.Invoke(instance, new object[] { double.Parse(num1), 1.0d/double.Parse(num2) });
                    return result;
                case "LOG":
                    if (double.Parse(num1) > 0 && double.Parse(num1) != 1 && double.Parse(num2) > 0)
                    {
                        methed = type.GetMethod("Log");
                        instance = Activator.CreateInstance(type);
                        result = methed.Invoke(instance, new object[] { double.Parse(num1), double.Parse(num2) });
                        return result;
                    }
                    else
                        return "Error";
            }
            return 0;
        }

        public static dynamic calVal(string op,string num)
        {
            Assembly assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "Operator_DLL.dll");
            Type type = assembly.GetType("Operator_DLL.Operator");
            switch (op)
            {
                case "FAC":
                    MethodInfo methed = type.GetMethod("Fac");
                    object instance = Activator.CreateInstance(type);
                    var result = methed.Invoke(instance, new object[] { double.Parse(num)});
                    return result;
                case "SQR":
                    methed = type.GetMethod("Pow");
                    instance = Activator.CreateInstance(type);
                    result = methed.Invoke(instance, new object[] { double.Parse(num),2.0d });
                    return result;
                case "SQRT":
                    methed = type.GetMethod("Sqrt");
                    instance = Activator.CreateInstance(type);
                    result = methed.Invoke(instance, new object[] { double.Parse(num)});
                    return result;
                case "LN":
                    methed = type.GetMethod("Ln");
                    instance = Activator.CreateInstance(type);
                    result = methed.Invoke(instance, new object[] { double.Parse(num) });
                    return result;
                case "SIN":
                    methed = type.GetMethod("Sin");
                    instance = Activator.CreateInstance(type);
                    result = methed.Invoke(instance, new object[] { double.Parse(num) });
                    return result;
                case "COS":
                    methed = type.GetMethod("Cos");
                    instance = Activator.CreateInstance(type);
                    result = methed.Invoke(instance, new object[] { double.Parse(num) });
                    return result;
                case "TAN":
                    methed = type.GetMethod("Tan");
                    instance = Activator.CreateInstance(type);
                    result = methed.Invoke(instance, new object[] { double.Parse(num) });
                    return result;
                case "SEC":
                    methed = type.GetMethod("Sec");
                    instance = Activator.CreateInstance(type);
                    result = methed.Invoke(instance, new object[] { double.Parse(num) });
                    return result;
                case "CSC":
                    methed = type.GetMethod("Csc");
                    instance = Activator.CreateInstance(type);
                    result = methed.Invoke(instance, new object[] { double.Parse(num) });
                    return result;
                case "COT":
                    methed = type.GetMethod("Cot");
                    instance = Activator.CreateInstance(type);
                    result = methed.Invoke(instance, new object[] { double.Parse(num) });
                    return result;
            }
            return 0;
        }

        /*计算后缀表达式*/
        public static dynamic calRPN(string tmp)
        {
            //double dRet = 0.0d;
            string[] strArr = tmp.Split(',');
            Stack<string> strStk = new Stack<string>();
            for (int i = 0; i < strArr.Length - 1; i++)
            {
                if (isNumber(strArr[i]))                //纯数字入栈
                    strStk.Push(strArr[i]);
                else if (isOp(strArr[i]))               //二元运算符，pop两个元素，计算值后压入栈
                    strStk.Push(calVal(strStk.Pop(), strStk.Pop(), strArr[i]).ToString());
                else if (isFunc(strArr[i]))         //一元函数名，pop一个元素，计算后压入栈
                    strStk.Push(calVal(strArr[i], strStk.Pop()).ToString());
            }
            //dRet = double.Parse(strStk.Pop());          //取最后栈中元素作为结果值
            return strStk.Pop();
        }
    }
}
