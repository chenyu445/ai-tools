using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RemarkTag.Qiniu;
using System.IO;
namespace RemarkTag
{
    public partial class LogOn :Form
    {
        string conString = string.Format(@"Data Source={0}; Pooling=false; FailIfMissing=false;", System.Windows.Forms.Application.StartupPath + @"\test.db");
       
        public LogOn()
        {
            InitializeComponent();
            this.textBox1.Text = "xiaowang";
            this.textBox2.Text = "wang2pass";
            this.HostTextBox.Text = "http://";

            Mylocal ml = new Mylocal();
            bool isGetInfo = ml.ExistWebFile();
            if (isGetInfo)//如果已经得到了配置信息
            {
                //把窗口变小
                this.Size = new System.Drawing.Size(480, 346);
            }
            else { 
                //窗口变大，即不变
            }
           
           // this.contextMenuStrip1.Renderer = new QQToolStripRenderer();
        }


        //登录按钮
        private void button1_Click(object sender, EventArgs e)
        {
            //bool rResult = ExistFileIdInFile("1", "11");
            //第一步，查找有没有host和Port的值
            Mylocal ml = new Mylocal();
            bool isGetInfo = ml.ExistWebFile();
            if (!isGetInfo)
            {
                string host = this.HostTextBox.Text;
                string port = this.PortTextBox.Text;
                if (host.Trim() == "http://" || host.Trim().Length < 8)
                {
                    MessageBox.Show("Please input host configuration");
                    return;
                }
                if (port.Trim() == "")
                {
                    MessageBox.Show("Please input port info！");
                    return;
                }
              
                //bool isGetInfo = ml.ExistWebFile();
               
                 //没有webinfo时，写入webinfo的信息
                    ml.WriteWebInfo(host, port);
                
            }

            //第二步，登录
            string UserName = this.textBox1.Text;
            string Password = this.textBox2.Text;
            if (UserName.Trim() == "") {
                MessageBox.Show("Please input user name");
                return;
            }
            if (Password.Trim() == "")
            {
                MessageBox.Show("Please input password！");
                return;
            }


            Data d = new Data();
            string reStr = d.GetLogStatus(UserName, Password);//"success";// 
            if (reStr == "success")
            {
                Form1 F1 = new Form1();
                this.Hide();
                // if(F1.IsAccessible())
                Form1.IsLogOn = true;
                F1.Show();
            }
            else {
                MessageBox.Show("You have no logon rights！");
                return;
            }

        }

        //关闭窗口
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Shou_Click(object sender, EventArgs e)
        {
            int height = this.Size.Height;
            if (height < 400)
            {
                this.Size = new System.Drawing.Size(480, 496);
            }
            else {
                this.Size = new System.Drawing.Size(480, 346);
            }
        }

        private void TitlePanel_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, TitlePanel.ClientRectangle,
                     Color.White, 0, ButtonBorderStyle.Solid, //左边
                     Color.White, 0, ButtonBorderStyle.Solid, //上边
                     Color.White, 0, ButtonBorderStyle.Solid, //右边
                     Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42))))), 1, ButtonBorderStyle.Solid);//底边
        }

        private void CloseWin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MinWin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

    }
}
