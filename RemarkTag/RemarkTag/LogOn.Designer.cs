namespace RemarkTag
{
    partial class LogOn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogOn));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TitlePanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MinWin = new System.Windows.Forms.Button();
            this.CloseWin = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Shou = new System.Windows.Forms.Button();
            this.hostLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.HostTextBox = new System.Windows.Forms.TextBox();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.TitlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.label1.Location = new System.Drawing.Point(145, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.textBox1.Location = new System.Drawing.Point(149, 156);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(278, 25);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.textBox2.Location = new System.Drawing.Point(149, 237);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(278, 25);
            this.textBox2.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(149, 288);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(278, 32);
            this.button1.TabIndex = 4;
            this.button1.Text = "Signin";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(59, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Login";
            // 
            // TitlePanel
            // 
            this.TitlePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TitlePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.TitlePanel.Controls.Add(this.pictureBox1);
            this.TitlePanel.Controls.Add(this.MinWin);
            this.TitlePanel.Controls.Add(this.CloseWin);
            this.TitlePanel.Controls.Add(this.label3);
            this.TitlePanel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.TitlePanel.Location = new System.Drawing.Point(0, 0);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new System.Drawing.Size(569, 50);
            this.TitlePanel.TabIndex = 20;
            this.TitlePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TitlePanel_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 37);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // MinWin
            // 
            this.MinWin.FlatAppearance.BorderSize = 0;
            this.MinWin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(184)))), ((int)(((byte)(223)))));
            this.MinWin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(184)))), ((int)(((byte)(223)))));
            this.MinWin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinWin.Image = ((System.Drawing.Image)(resources.GetObject("MinWin.Image")));
            this.MinWin.Location = new System.Drawing.Point(455, 10);
            this.MinWin.Name = "MinWin";
            this.MinWin.Size = new System.Drawing.Size(37, 37);
            this.MinWin.TabIndex = 8;
            this.MinWin.UseVisualStyleBackColor = true;
            this.MinWin.Click += new System.EventHandler(this.MinWin_Click);
            // 
            // CloseWin
            // 
            this.CloseWin.FlatAppearance.BorderSize = 0;
            this.CloseWin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(79)))));
            this.CloseWin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(79)))));
            this.CloseWin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseWin.Image = ((System.Drawing.Image)(resources.GetObject("CloseWin.Image")));
            this.CloseWin.Location = new System.Drawing.Point(504, 9);
            this.CloseWin.Name = "CloseWin";
            this.CloseWin.Size = new System.Drawing.Size(37, 37);
            this.CloseWin.TabIndex = 7;
            this.CloseWin.UseVisualStyleBackColor = true;
            this.CloseWin.Click += new System.EventHandler(this.CloseWin_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.label4.Location = new System.Drawing.Point(145, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Password";
            // 
            // Shou
            // 
            this.Shou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Shou.FlatAppearance.BorderSize = 0;
            this.Shou.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Shou.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Shou.Image = ((System.Drawing.Image)(resources.GetObject("Shou.Image")));
            this.Shou.Location = new System.Drawing.Point(266, 333);
            this.Shou.Name = "Shou";
            this.Shou.Size = new System.Drawing.Size(28, 28);
            this.Shou.TabIndex = 7;
            this.Shou.UseVisualStyleBackColor = false;
            this.Shou.Click += new System.EventHandler(this.Shou_Click);
            // 
            // hostLabel
            // 
            this.hostLabel.AutoSize = true;
            this.hostLabel.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.hostLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.hostLabel.Location = new System.Drawing.Point(145, 434);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(40, 20);
            this.hostLabel.TabIndex = 0;
            this.hostLabel.Text = "Host";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.label5.Location = new System.Drawing.Point(145, 517);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Port";
            // 
            // HostTextBox
            // 
            this.HostTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.HostTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HostTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.HostTextBox.Location = new System.Drawing.Point(149, 471);
            this.HostTextBox.Name = "HostTextBox";
            this.HostTextBox.Size = new System.Drawing.Size(278, 25);
            this.HostTextBox.TabIndex = 2;
            // 
            // PortTextBox
            // 
            this.PortTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.PortTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PortTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.PortTextBox.Location = new System.Drawing.Point(149, 555);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(278, 25);
            this.PortTextBox.TabIndex = 3;
            // 
            // LogOn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.ClientSize = new System.Drawing.Size(568, 666);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Shou);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.HostTextBox);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.hostLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TitlePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LogOn";
            this.ShowIcon = false;
            this.TitlePanel.ResumeLayout(false);
            this.TitlePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel TitlePanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Shou;
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox HostTextBox;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Button CloseWin;
        private System.Windows.Forms.Button MinWin;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}