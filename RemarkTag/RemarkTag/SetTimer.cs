using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemarkTag
{
    public partial class SetTimer : Form
    {
        private Form1 mainFrm;
        public SetTimer(Form1 mainForm)
        {
            InitializeComponent();
            this.mainFrm = mainForm;
        }
         //public frmControl(SetTimer mainForm)
         //{
         //     this.mainForm=mainForm;
         //}
        private void TitlePanel_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel1.ClientRectangle,
                     Color.White, 0, ButtonBorderStyle.Solid, //左边
                     Color.White, 0, ButtonBorderStyle.Solid, //上边
                     Color.White, 0, ButtonBorderStyle.Solid, //右边
                     Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42))))), 1, ButtonBorderStyle.Solid);//底边
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Internal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键  
            {
                int len = Internal.Text.Length;
                if (len < 1 && e.KeyChar == '0')
                {
                    e.Handled = true;
                }
                else if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字  
                {
                    e.Handled = true;
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inter = this.Internal.Text.Trim();
            if (inter == "")
            {
                MessageBox.Show("Please input the Timer Internal!");
                return;
            }
            else
            {
                BaseBll bll = new BaseBll();
                bll.TimerSet = Convert.ToInt32(this.Internal.Text);

                Form1 form = new Form1();
                
                form.TimerInterval = bll.TimerSet;
                 // .timerInternal.Text = bll.TimerSet.ToString();
                if (mainFrm != null)
                {
                    mainFrm.timerInternal.Text = this.Internal.Text;
                }

                this.Close();
            }

        }
    }
}
