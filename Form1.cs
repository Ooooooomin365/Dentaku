using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Dentaku
{

    public partial class Form1 : Form
    {

        bool _Check = false;

        double[] _result ={ 0, 0 };  //1度目と2度目に入力した値を配列管理する。
        public Form1()
        {
            InitializeComponent();
        }

        //数字入力部分
        private void BtnNumber_Click(object sender, EventArgs e)
        {
            //四則演算をしているとき
            if (c != _Calc._null && !_Check)
            {
                TextDisplay.Text = "";  // +や-等を押してから2度目の値を入力する際にディスプレイを初期化する。
                _Check = true;　//1度だけ実行するため

            }

            try
            {   //オーバーフローチェック
                checked
                {
                    string DisplayText = TextDisplay.Text + ((Button)sender).Text;

                    double dDisplayText = Convert.ToDouble(DisplayText);

                    string DisplayText2 = dDisplayText.ToString(); //2度手間に見えるが、文字列を数値に変えた際に
                                                                   //最初の値が0だとその値は消える。
                    TextDisplay.Text = DisplayText2;
                }
            }
            catch (OverflowException)
            {
                TextDisplay.Text = "オーバーフロー";
            }
        }

        // クリアボタンを押すと初期設定に戻る。配列内の数値も初期化する。
        private void Btnclear_Click(object sender, EventArgs e)
        {
            TextDisplay.Text = "0";
            _result[0] = 0;
            _result[1] = 0;
        }

        //ドットをクリックしたとき
        private void BtnDot_Click(object sender, EventArgs e)
        {
            if (TextDisplay.Text == "" || TextDisplay.Text.IndexOf(".") >= 0)
                return;
            else
                TextDisplay.Text += ((Button)sender).Text;
        }

        enum _Calc{
            _null,
            _Sum,
            _Sub,
            _Mul,
            _Div,
            _Faiz
        }

        _Calc c = _Calc._null;

        private void BtnCalc_Click(object sender, EventArgs e)
        {

            if (((Button)sender).Text == "+")
            {
                c = _Calc._Sum;
            }
            else if (((Button)sender).Text == "-")
            {
                c = _Calc._Sub;
            }
            else if (((Button)sender).Text == "×")
            {
                c = _Calc._Mul;
            }
            else if (((Button)sender).Text == "÷")
            {
                c = _Calc._Div;
            }

            _result[0] = double.Parse(TextDisplay.Text);
            _Check = false;
        }

        //イコールボタンを押したとき
        private void BtnEqu_Click(object sender, EventArgs e)
        {
            _result[1] = Convert.ToDouble(TextDisplay.Text);  //2番目の値

            //四則演算をせずに"555"表示で"="をクリックするとファイズマークになる（おまけ機能）。
            if(TextDisplay.Text == "555")
                c = _Calc._Faiz;　

            try
            {
                checked
                {
                    switch (c)
                    {
                        case _Calc._Sum:
                            TextDisplay.Text = (_result[0] + _result[1]).ToString();
                            break;

                        case _Calc._Sub:
                            TextDisplay.Text = (_result[0] - _result[1]).ToString();
                            break;

                        case _Calc._Mul:
                            TextDisplay.Text = (_result[0] * _result[1]).ToString();
                            break;

                        case _Calc._Div:
                            TextDisplay.Text = (_result[0] / _result[1]).ToString();
                            break;

                        case _Calc._Faiz:
                            TextDisplay.Text = "Φ";
                            break;
                    }
                }
            }
            catch(OverflowException)
            {
                TextDisplay.Text = "オーバーフロー";
            }

        }

        // Googleホームへ
        private void BtnToG_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.google.com/");
        }
    }
}
