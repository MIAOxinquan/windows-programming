using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Operator_DLL;
using System.Text.RegularExpressions;


namespace Calculator
{
    public partial class Calculater : Form
    {
        private string ope;
        private string expression;
        private string show_exp;
        private int count_left; //未成对左括号数
        //private Stack<string> stack;
        private dynamic ope1;
        private dynamic ope2;
        public Calculater()
        {
            ope = "";
            expression = "";
            show_exp = "";
            count_left = 0;
            //stack = new Stack<string>();

            InitializeComponent();
        }

        public Boolean isFloat(string arg)
        {
            return Regex.IsMatch(arg, @"\d+\.[1-9]+");
        }

        

        /*按键“1”*/
        private void bt_1_Click(object sender, EventArgs e)
        {
            if (!output.Text.Equals("0"))
                ope += "1";
            else
            {
                ope = "1";
                bt_C.Text = "CE";
            }
            output.Text = ope;
        }
        /*按键“2”*/
        private void bt_2_Click(object sender, EventArgs e)
        {
            if (!output.Text.Equals("0"))
                ope += "2";
            else
            {
                ope = "2";
                bt_C.Text = "CE";
            }
            output.Text = ope;
        }
        /*按键“3”*/
        private void bt_3_Click(object sender, EventArgs e)
        {
            if (!output.Text.Equals("0"))
                ope += "3";
            else
            {
                ope = "3";
                bt_C.Text = "CE";
            }
            output.Text = ope; 
        }
        /*按键“4”*/
        private void bt_4_Click(object sender, EventArgs e)
        {
            if (!output.Text.Equals("0"))
                ope += "4";
            else
            {
                ope = "4";
                bt_C.Text = "CE";
            }
            output.Text = ope;
        }
        /*按键“5”*/
        private void bt_5_Click(object sender, EventArgs e)
        {
            if (!output.Text.Equals("0"))
                ope += "5";
            else
            {
                ope = "5";
                bt_C.Text = "CE";
            }
            output.Text = ope;
        }
        /*按键“6”*/
        private void bt_6_Click(object sender, EventArgs e)
        {
            if (!output.Text.Equals("0"))
                ope += "6";
            else
            {
                ope = "6";
                bt_C.Text = "CE";
            }
            output.Text = ope;
        }
        /*按键“7”*/
        private void bt_7_Click(object sender, EventArgs e)
        {
            if (!output.Text.Equals("0"))
                ope += "7";
            else
            {
                ope = "7";
                bt_C.Text = "CE";
            }                
            output.Text = ope;
        }
        /*按键“8”*/
        private void bt_8_Click(object sender, EventArgs e)
        {
            if (!output.Text.Equals("0"))
                ope += "8";
            else
            {
                ope = "8";
                bt_C.Text = "CE";
            }
            output.Text = ope;
        }
        /*按键“9”*/
        private void bt_9_Click(object sender, EventArgs e)
        {
            if (!output.Text.Equals("0"))
                ope += "9";
            else
            {
                ope = "9";
                bt_C.Text = "CE";
            }
            output.Text = ope;
        }
        /*按键“0”*/
        private void bt_0_Click(object sender, EventArgs e)
        {         
            if (output.Text.Equals("0"))            
                ope = "0";                
            else            
                ope += "0"; 
            output.Text = ope;
        }
        /*小数点*/
        private void bt_dot_Click(object sender, EventArgs e)
        {
            ope = output.Text;
            if (!ope.Contains("."))
            {
                ope += ".";
                output.Text = ope;
            }            
        }
        
        /*等号按键*/
        private void bt_Cal_Click(object sender, EventArgs e)
        {
            if (expression == "")
                return;
            if (!ope.Contains("-"))
            {
                expression = expression + ope;
                show_exp = show_exp + ope + "=";
            }
            else
            {
                expression = expression + "(0" + ope + ")";
                show_exp = show_exp + ope + "=";
            }
            exp.Text = show_exp;                        
            output.Text = Calculator.calRPN(Calculator.ConvertRPN(expression)).ToString();
            ope = "";
            expression = "";
            show_exp = "";
        }
        /*清除按键-- C && CE */
        private void bt_C_Click(object sender, EventArgs e)
        {
            if (bt_C.Text.Equals("C") || exp.Text.Contains("="))
            {
                ope = "";
                expression = "";
                show_exp = "";
                exp.Text = "";                
            }
            else if (bt_C.Text.Equals("CE"))
            {
                ope = "";                
            }
            bt_C.Text = "C";
            output.Text = "0";
        }
        /*左括号*/
        private void bt_left_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(expression,@"[0-9]+"))
            {
                expression = expression + ope + "(";
                show_exp = show_exp + ope + "(";                
            }
            else
            {
                expression = expression + "(";
                show_exp = show_exp + "(";
            }
            exp.Text = show_exp;
            count_left++;
            ope = "";
        }
        /*右括号*/
        private void bt_right_Click(object sender, EventArgs e)
        {
            //左括号数量大于零才能输入右括号
            if (count_left > 0)
            {
                expression = expression + ope + ")";
                show_exp = show_exp + ope + ")";
                exp.Text = show_exp;
                count_left--;
                ope = "";
            }
        }
        /*加*/
        private void bt_add_Click(object sender, EventArgs e)
        {
            ope = output.Text;
            if (!ope.Contains("-"))
            {
                expression = expression + ope + "+";
                show_exp = show_exp + ope + "+";
            }
            else
            {
                expression = expression + "(0" + ope + ")" + "+";
                show_exp = show_exp + ope + "+";
            }            
            exp.Text = show_exp;
            ope = "";
        }
        /*减*/
        private void bt_sub_Click(object sender, EventArgs e)
        {
            ope = output.Text;
            if (!ope.Contains("-"))
            {
                expression = expression + ope + "-";
                show_exp = show_exp + ope + "-";
            }
            else
            {
                expression = expression + "(0" + ope + ")" + "-";
                show_exp = show_exp + ope + "-";
            }
            exp.Text = show_exp;
            ope = "";
        }
        /*乘*/
        private void bt_mul_Click(object sender, EventArgs e)
        {
            ope = output.Text;
            if (!ope.Contains("-"))
            {
                expression = expression + ope + "*";
                show_exp = show_exp + ope + "×";
            }
            else
            {
                expression = expression + "(0" + ope + ")" + "*";
                show_exp = show_exp + ope + "×";
            }
            exp.Text = show_exp;
            ope = "";
        }
        /*除*/
        private void bt_div_Click(object sender, EventArgs e)
        {
            ope = output.Text;
            if (!ope.Contains("-"))
            {
                expression = expression + ope + "/";
                show_exp = show_exp + ope + "÷";
            }
            else
            {
                expression = expression + "(0" + ope + ")" + "/";
                show_exp = show_exp + ope + "÷";
            }
            exp.Text = show_exp;
            ope = "";
        }
        /*退格*/
        private void bt_Back_Click(object sender, EventArgs e)
        {
            ope = output.Text;
            if (ope.Contains("-") && ope.Length == 2 || ope.Length == 1)
            {
                ope = "";
                bt_C.Text = "C";
                output.Text = "0";
            }
            else
            {
                ope = ope.Substring(0, ope.Length - 1);
                output.Text = ope;
            }
        }
        /*相反数*/
        private void bt_neg_Click(object sender, EventArgs e)
        {                    
            ope = output.Text;
            if (!ope.Equals("0"))
            {
                if (!ope.Contains("-"))
                {
                    ope = ope.Insert(0, "-");                    
                }
                else
                {
                    ope = ope.Substring(1, ope.Length-1);
                }
                output.Text = ope;
            }
        }

        private void bt_Pow_Click(object sender, EventArgs e)
        {
            ope = output.Text;
            if (!ope.Contains("-"))
            {
                expression = expression + ope + "^";
                show_exp = show_exp + ope + "^";
            }
            else
            {
                expression = expression + "(0" + ope + ")" + "^";
                show_exp = show_exp + ope + "^";
            }
            exp.Text = show_exp;
            ope = "";
        }
        /*阶乘*/
        private void bt_Fac_Click(object sender, EventArgs e)
        {
            ope = output.Text;
            if (ope.Contains("-"))
            {
                ope = "";
                output.Text = "Error";
                return;
            }
            expression = expression + "fac(" + ope + ")";
            show_exp = show_exp + "fac(" + ope + ")";
            exp.Text = show_exp;
            ope = "";
            output.Text = "";
        }
        /*π*/
        private void bt_Pi_Click(object sender, EventArgs e)
        {
            output.Text = Math.PI.ToString();            
        }

        private void bt_E_Click(object sender, EventArgs e)
        {
            output.Text = Math.E.ToString();
        }
        /*平方*/
        private void bt_Sq_Click(object sender, EventArgs e)
        {            
            ope = output.Text;
            if (!ope.Contains("-"))
            {
                expression = expression + "sqr(" + ope + ")";
                show_exp = show_exp + "sqr(" + ope + ")";
            }
            else
            {
                expression = expression + "sqr(0" + ope + ")";
                show_exp = show_exp + "sqr(" + ope + ")";
            }
            exp.Text = show_exp;
            ope = "";
            output.Text = "";
        }
        /*开n次根*/
        private void bt_Ext_Click(object sender, EventArgs e)
        {
            ope = output.Text;
            if (!ope.Contains("-"))
            {
                expression = expression + ope + "EXT";
                show_exp = show_exp + ope + "yroot";
            }
            else
            {
                expression = expression + "(0" + ope + ")" + "EXT";
                show_exp = show_exp + ope + "yroot";
            }
            exp.Text = show_exp;
            ope = "";
        }
        /*开二次根*/
        private void bt_Sqrt_Click(object sender, EventArgs e)
        {
            ope = output.Text;
            if (ope.Contains("-"))
            {
                ope = "";
                output.Text = "Error";
                return;
            }
            expression = expression + "sqrt(" + ope + ")";
            show_exp = show_exp + "sqrt(" + ope + ")";
            exp.Text = show_exp;
            ope = "";
            output.Text = "";
        }
        /*对数*/
        private void bt_Log_Click(object sender, EventArgs e)
        {
            ope = output.Text;
            if (ope.Contains("-"))
            {
                ope = "";
                output.Text = "Error";
                return;
            }
            expression = expression + ope + "LOG";
            show_exp = show_exp + ope + "base log";
            exp.Text = show_exp;
            ope = "";
            output.Text = "0";
        }
        /*自然对数*/
        private void bt_Ln_Click(object sender, EventArgs e)
        {
            ope = output.Text;
            if (ope.Contains("-"))
            {
                ope = "";
                output.Text = "Error";
                return;
            }
            expression = expression + "ln(" + ope + ")";
            show_exp = show_exp + "ln(" + ope + ")";
            exp.Text = show_exp;
            ope = "";
            output.Text = "0";
        }
        /*正弦*/
        private void bu_Sin_Click(object sender, EventArgs e)
        {
            ope = output.Text;
            if (!ope.Contains("-"))
            {
                expression = expression + "sin(" + ope + ")";
                show_exp = show_exp + "sin(" + ope + ")";
            }
            else
            {
                expression = expression + "sin(0" + ope + ")";
                show_exp = show_exp + "sin(" + ope + ")";
            }
            exp.Text = show_exp;
            ope = "";
        }

        private void Calculater_Load(object sender, EventArgs e)
        {

        }
    }
}
