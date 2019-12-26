using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Operator_DLL
{
    public class Operator
    {
        //加法
        public double Add(double a, double b)
        {
            return a + b;
        }
        //减法
        public double Sub(double a, double b)
        {
            return a - b;
        }
        //乘法
        public double Mul(double a, double b)
        {
            return a * b;
        }
        //除法
        public double Div(double a,double b)
        {
            return a / b;
        }
        //幂运算
        public double Pow(double a,double b)
        {
            return Math.Pow(a, b);
        }
        //阶乘
        public double Fac(double a)
        {
            if (a == 0)
                return 1;
            return Fac(a - 1) * a;
        }
        //开平方
        public double Sqrt(double a)
        {
            return Math.Sqrt(a);
        }
        //对数
        public double Log(double a,double b)
        {
            return Math.Log(b, a);
        }
        //自然对数
        public double Ln(double a)
        {
            return Math.Log(a);
        }
        //正弦
        public double Sin(double a)
        {
            return Math.Sin(a * Math.PI / 180);
        }
        //余弦
        public double Cos(double a)
        {
            return Math.Cos(a * Math.PI / 180);
        }
        //正切
        public double Tan(double a)
        {
            return Math.Tan(a * Math.PI / 180);
        }
        //正割
        public double Sec(double a)
        {
            return Math.Asin(a) * 180 / Math.PI;
        }
        //余割
        public double Csc(double a)
        {
            return Math.Acos(a) * 180 / Math.PI;
        }
        //余切
        public double Cot(double a)
        {
            return Math.Atan(a) * 180 / Math.PI;
        }

    }
}
