using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HesapMakinesi
{
    public partial class Form1 : Form
    {
        bool isFirst = false, isEqLast = false;
        char islem = ' ';
        double s1 = 0, s2 = 0;
        char[] islemler = new char[] { '+', '-', '*', '/', '=' };
        public Form1()
        {
            InitializeComponent();
        }

        private void btnNumPad_Click(object sender, EventArgs e)
        {
            //Button tiklanan = sender as Button;
            Button tiklanan = (Button)sender;

            WriteToDisplay(tiklanan.Text);
        }

        private void btnIslem_Click(object sender, EventArgs e)
        {
            Button tiklanan = sender as Button;

            if (islem != ' ') Hesapla(tiklanan.Text);

            IslemSec(tiklanan.Text);
        }

        private void IslemSec(string text)
        {
            isEqLast = false;

            if (islem == ' ') s1 = Convert.ToDouble(txtDisplay.Text);

            switch (text)
            {
                case "+": islem = '+'; break;
                case "-": islem = '-'; break;
                case "x":
                case "*": islem = '*'; break;
                case "/":
                case "÷": islem = '/'; break;
                default: isEqLast = true; break;
            }
            isFirst = true;
        }

        private void Hesapla(string text)
        {
            if (!isEqLast) s2 = Convert.ToDouble(txtDisplay.Text);

            if (!isFirst || text == "=")
            {
                switch (islem)
                {
                    case '+': s1 += s2; break;
                    case '-': s1 -= s2; break;
                    case '*': s1 *= s2; break;
                    case '/': s1 /= s2; break;
                }
            }
            txtDisplay.Text = s1.ToString();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DeleteToDisplay();

            if (txtDisplay.Text[(txtDisplay.Text.Length - 1)] == ',')
            {
                DeleteToDisplay();
            }
        }

        private void DeleteToDisplay()
        {
            if (txtDisplay.Text.Length > 1)
            {
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
            }
            else
            {
                txtDisplay.Text = "0";
            }
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            isEqLast = isFirst = false;
            islem = ' ';
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
        }

        private void Form1_KlavyedenBasilan(object sender, KeyPressEventArgs e)
        {
            if (txtDisplay.Text == "0")
            {
                txtDisplay.Text = "";
            }

            if (isFirst && islem != ' ')
            {
                txtDisplay.Text = "0";
            }

            if (e.KeyChar != ',' && !islemler.Contains(e.KeyChar) && !char.IsNumber(e.KeyChar) && e.KeyChar != (int)Keys.Back)
            {
                e.Handled = true;
            }
            else if (islemler.Contains(e.KeyChar))
            {
                IslemSec(e.KeyChar.ToString());
            }
            else if (e.KeyChar == (int)Keys.Back)
            {
                DeleteToDisplay();
            }
            else
            {
                WriteToDisplay(e.KeyChar.ToString());
            }
        }

        private void txtDisplay_TextChanged(object sender, EventArgs e)
        {
            if (txtDisplay.Text == ",")
            {
                txtDisplay.Text = "0,";
            }
        }

        private void WriteToDisplay(string basilacak)
        {
            //giriş öncesi reset
            if (txtDisplay.Text == "0" && basilacak != ",")
            {
                txtDisplay.Text = "";
            }
            //2.sayı için hazırlık

            if (isFirst && islem != ' ')
            {
                txtDisplay.Text = "";
            }

            //istenmeyen durumlar
            if (basilacak == "," && txtDisplay.Text.Contains(','))
            {
                return;
            }
            txtDisplay.Text += basilacak;

            isFirst = false;
        }
    }
}
