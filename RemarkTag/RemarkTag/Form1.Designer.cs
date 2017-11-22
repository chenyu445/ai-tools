
using System.Drawing;
namespace RemarkTag
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SaveJson = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.annoteLine = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.timerInternal = new System.Windows.Forms.Button();
            this.AnnotateTag = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.TimerOC = new System.Windows.Forms.CheckBox();
            this.TypeTab = new System.Windows.Forms.Label();
            this.Annotate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.toolStripComboBox1 = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.Suo = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.Bus = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.Special = new System.Windows.Forms.Button();
            this.Trafficsigns = new System.Windows.Forms.Button();
            this.Cyclist = new System.Windows.Forms.Button();
            this.Bicycle = new System.Windows.Forms.Button();
            this.Trunk = new System.Windows.Forms.Button();
            this.Dontcare = new System.Windows.Forms.Button();
            this.carbtn = new System.Windows.Forms.Button();
            this.Infosigns = new System.Windows.Forms.Button();
            this.Petestrain = new System.Windows.Forms.Button();
            this.Motorcycle = new System.Windows.Forms.Button();
            this.TrunkTrailer = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.pan_picture = new System.Windows.Forms.Panel();
            this.ToolBar = new System.Windows.Forms.Panel();
            this.Last = new System.Windows.Forms.Button();
            this.Next = new System.Windows.Forms.Button();
            this.Bigger = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.BackOne = new System.Windows.Forms.Button();
            this.Around = new System.Windows.Forms.Button();
            this.LeftRight = new System.Windows.Forms.Button();
            this.Updown = new System.Windows.Forms.Button();
            this.Smaller = new System.Windows.Forms.Button();
            this.TitlePanel = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.fen = new System.Windows.Forms.Label();
            this.CrtBucketName = new System.Windows.Forms.Label();
            this.UserName = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.TitleName = new System.Windows.Forms.Label();
            this.CloseWin = new System.Windows.Forms.Button();
            this.MinWin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblProgress = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pan_picture.SuspendLayout();
            this.ToolBar.SuspendLayout();
            this.TitlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox1.Location = new System.Drawing.Point(266, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1254, 700);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseDown);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.picBox_MouseEnter);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseUp);
            this.pictureBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseWheel);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LemonChiffon;
            this.button2.Location = new System.Drawing.Point(44, 675);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(193, 34);
            this.button2.TabIndex = 3;
            this.button2.Text = "Confirm";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.panel1.Controls.Add(this.SaveJson);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.annoteLine);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.timerInternal);
            this.panel1.Controls.Add(this.AnnotateTag);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.TimerOC);
            this.panel1.Controls.Add(this.TypeTab);
            this.panel1.Controls.Add(this.Annotate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.toolStripComboBox1);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1906, 36);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // SaveJson
            // 
            this.SaveJson.Location = new System.Drawing.Point(597, 8);
            this.SaveJson.Name = "SaveJson";
            this.SaveJson.Size = new System.Drawing.Size(75, 23);
            this.SaveJson.TabIndex = 27;
            this.SaveJson.Text = "Back";
            this.SaveJson.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(522, 8);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 26;
            this.button6.Text = "Save";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // annoteLine
            // 
            this.annoteLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(184)))), ((int)(((byte)(223)))));
            this.annoteLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.annoteLine.Location = new System.Drawing.Point(1758, 31);
            this.annoteLine.Name = "annoteLine";
            this.annoteLine.Size = new System.Drawing.Size(100, 4);
            this.annoteLine.TabIndex = 25;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(184)))), ((int)(((byte)(223)))));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Location = new System.Drawing.Point(1639, 31);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(100, 4);
            this.panel4.TabIndex = 25;
            // 
            // timerInternal
            // 
            this.timerInternal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.timerInternal.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.timerInternal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(79)))));
            this.timerInternal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.timerInternal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.timerInternal.Location = new System.Drawing.Point(228, 7);
            this.timerInternal.Name = "timerInternal";
            this.timerInternal.Size = new System.Drawing.Size(67, 25);
            this.timerInternal.TabIndex = 24;
            this.timerInternal.Text = "2";
            this.timerInternal.UseVisualStyleBackColor = false;
            this.timerInternal.Click += new System.EventHandler(this.timerInternal_Click);
            // 
            // AnnotateTag
            // 
            this.AnnotateTag.AutoSize = true;
            this.AnnotateTag.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.AnnotateTag.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.AnnotateTag.Location = new System.Drawing.Point(1756, 9);
            this.AnnotateTag.Name = "AnnotateTag";
            this.AnnotateTag.Size = new System.Drawing.Size(97, 20);
            this.AnnotateTag.TabIndex = 22;
            this.AnnotateTag.Text = "Annotate";
            this.AnnotateTag.Click += new System.EventHandler(this.AnnotateTag_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(1869, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(28, 28);
            this.button4.TabIndex = 0;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.Location = new System.Drawing.Point(369, 7);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(28, 28);
            this.button7.TabIndex = 0;
            this.button7.UseVisualStyleBackColor = false;
            // 
            // TimerOC
            // 
            this.TimerOC.AutoSize = true;
            this.TimerOC.Checked = true;
            this.TimerOC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TimerOC.Font = new System.Drawing.Font("楷体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TimerOC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.TimerOC.Location = new System.Drawing.Point(157, 11);
            this.TimerOC.Name = "TimerOC";
            this.TimerOC.Size = new System.Drawing.Size(69, 19);
            this.TimerOC.TabIndex = 23;
            this.TimerOC.Text = "Timer";
            this.TimerOC.UseVisualStyleBackColor = true;
            this.TimerOC.CheckedChanged += new System.EventHandler(this.TimerOC_CheckedChanged);
            // 
            // TypeTab
            // 
            this.TypeTab.AutoSize = true;
            this.TypeTab.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.TypeTab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
            this.TypeTab.Location = new System.Drawing.Point(1651, 8);
            this.TypeTab.Name = "TypeTab";
            this.TypeTab.Size = new System.Drawing.Size(53, 20);
            this.TypeTab.TabIndex = 21;
            this.TypeTab.Text = "Type";
            this.TypeTab.Click += new System.EventHandler(this.AnnotateTag_Click);
            // 
            // Annotate
            // 
            this.Annotate.Location = new System.Drawing.Point(930, 11);
            this.Annotate.Name = "Annotate";
            this.Annotate.Size = new System.Drawing.Size(41, 23);
            this.Annotate.TabIndex = 21;
            this.Annotate.Text = "Annotate";
            this.Annotate.UseVisualStyleBackColor = true;
            this.Annotate.Visible = false;
            this.Annotate.Click += new System.EventHandler(this.Annotate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.label3.Location = new System.Drawing.Point(1619, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "|";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.label2.Location = new System.Drawing.Point(344, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "|";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.label16.Location = new System.Drawing.Point(122, 7);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(22, 24);
            this.label16.TabIndex = 5;
            this.label16.Text = "|";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.toolStripComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toolStripComboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.toolStripComboBox1.FormattingEnabled = true;
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "Enable Zoom",
            "Disable Zoom"});
            this.toolStripComboBox1.Location = new System.Drawing.Point(977, 10);
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(140, 23);
            this.toolStripComboBox1.TabIndex = 20;
            this.toolStripComboBox1.Text = "Disable Zoom";
            this.toolStripComboBox1.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.label15.Location = new System.Drawing.Point(398, 12);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(19, 19);
            this.label15.TabIndex = 8;
            this.label15.Text = "2";
            // 
            // label5
            // 
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("宋体", 10F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.label5.Location = new System.Drawing.Point(417, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 21);
            this.label5.TabIndex = 6;
            this.label5.Text = "s";
            // 
            // label14
            // 
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Font = new System.Drawing.Font("宋体", 10F);
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.label14.Location = new System.Drawing.Point(295, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(23, 25);
            this.label14.TabIndex = 6;
            this.label14.Text = "s";
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("楷体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.Location = new System.Drawing.Point(2, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(96, 30);
            this.button5.TabIndex = 0;
            this.button5.Text = "GetBucket";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.buttonImportBudget_Click);
            // 
            // Suo
            // 
            this.Suo.FlatAppearance.BorderSize = 0;
            this.Suo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Suo.Image = ((System.Drawing.Image)(resources.GetObject("Suo.Image")));
            this.Suo.Location = new System.Drawing.Point(221, -1);
            this.Suo.Name = "Suo";
            this.Suo.Size = new System.Drawing.Size(20, 39);
            this.Suo.TabIndex = 22;
            this.Suo.UseVisualStyleBackColor = true;
            this.Suo.Click += new System.EventHandler(this.button3_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "A",
            "B",
            "C"});
            this.comboBox1.Location = new System.Drawing.Point(12, 682);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(26, 23);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label13.Location = new System.Drawing.Point(63, 617);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(127, 15);
            this.label13.TabIndex = 8;
            this.label13.Text = "Or input  type:";
            this.label13.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(53, 635);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(193, 25);
            this.textBox1.TabIndex = 9;
            this.textBox1.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.comboBox1);
            this.panel3.Location = new System.Drawing.Point(1631, 86);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(276, 718);
            this.panel3.TabIndex = 18;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.panel5.Controls.Add(this.Bus);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.Special);
            this.panel5.Controls.Add(this.Trafficsigns);
            this.panel5.Controls.Add(this.Cyclist);
            this.panel5.Controls.Add(this.Bicycle);
            this.panel5.Controls.Add(this.Trunk);
            this.panel5.Controls.Add(this.Dontcare);
            this.panel5.Controls.Add(this.carbtn);
            this.panel5.Controls.Add(this.Infosigns);
            this.panel5.Controls.Add(this.Petestrain);
            this.panel5.Controls.Add(this.Motorcycle);
            this.panel5.Controls.Add(this.TrunkTrailer);
            this.panel5.Controls.Add(this.textBox2);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.button1);
            this.panel5.Controls.Add(this.comboBox2);
            this.panel5.Location = new System.Drawing.Point(3, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(270, 718);
            this.panel5.TabIndex = 19;
            // 
            // Bus
            // 
            this.Bus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Bus.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Bus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
            this.Bus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(79)))));
            this.Bus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Bus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.Bus.Location = new System.Drawing.Point(151, 9);
            this.Bus.Name = "Bus";
            this.Bus.Size = new System.Drawing.Size(121, 30);
            this.Bus.TabIndex = 10;
            this.Bus.Text = "Bus";
            this.Bus.UseVisualStyleBackColor = false;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Location = new System.Drawing.Point(2, 210);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(267, 4);
            this.panel6.TabIndex = 25;
            // 
            // Special
            // 
            this.Special.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Special.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Special.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
            this.Special.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(79)))));
            this.Special.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Special.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.Special.Location = new System.Drawing.Point(6, 174);
            this.Special.Name = "Special";
            this.Special.Size = new System.Drawing.Size(121, 30);
            this.Special.TabIndex = 10;
            this.Special.Text = "Special";
            this.Special.UseVisualStyleBackColor = false;
            // 
            // Trafficsigns
            // 
            this.Trafficsigns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Trafficsigns.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Trafficsigns.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
            this.Trafficsigns.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(79)))));
            this.Trafficsigns.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Trafficsigns.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.Trafficsigns.Location = new System.Drawing.Point(6, 141);
            this.Trafficsigns.Name = "Trafficsigns";
            this.Trafficsigns.Size = new System.Drawing.Size(121, 30);
            this.Trafficsigns.TabIndex = 10;
            this.Trafficsigns.Text = "Traffic signs";
            this.Trafficsigns.UseVisualStyleBackColor = false;
            // 
            // Cyclist
            // 
            this.Cyclist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Cyclist.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Cyclist.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
            this.Cyclist.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(79)))));
            this.Cyclist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cyclist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.Cyclist.Location = new System.Drawing.Point(6, 108);
            this.Cyclist.Name = "Cyclist";
            this.Cyclist.Size = new System.Drawing.Size(121, 30);
            this.Cyclist.TabIndex = 10;
            this.Cyclist.Text = "Cyclist";
            this.Cyclist.UseVisualStyleBackColor = false;
            // 
            // Bicycle
            // 
            this.Bicycle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Bicycle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Bicycle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
            this.Bicycle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(79)))));
            this.Bicycle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Bicycle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.Bicycle.Location = new System.Drawing.Point(6, 75);
            this.Bicycle.Name = "Bicycle";
            this.Bicycle.Size = new System.Drawing.Size(121, 30);
            this.Bicycle.TabIndex = 10;
            this.Bicycle.Text = "Bicycle";
            this.Bicycle.UseVisualStyleBackColor = false;
            // 
            // Trunk
            // 
            this.Trunk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Trunk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Trunk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
            this.Trunk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(79)))));
            this.Trunk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Trunk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.Trunk.Location = new System.Drawing.Point(6, 42);
            this.Trunk.Name = "Trunk";
            this.Trunk.Size = new System.Drawing.Size(121, 30);
            this.Trunk.TabIndex = 10;
            this.Trunk.Text = "Trunk";
            this.Trunk.UseVisualStyleBackColor = false;
            // 
            // Dontcare
            // 
            this.Dontcare.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Dontcare.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Dontcare.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
            this.Dontcare.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(79)))));
            this.Dontcare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Dontcare.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.Dontcare.Location = new System.Drawing.Point(151, 174);
            this.Dontcare.Name = "Dontcare";
            this.Dontcare.Size = new System.Drawing.Size(121, 30);
            this.Dontcare.TabIndex = 10;
            this.Dontcare.Text = "Don\'t care";
            this.Dontcare.UseVisualStyleBackColor = false;
            // 
            // carbtn
            // 
            this.carbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.carbtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.carbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
            this.carbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(79)))));
            this.carbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.carbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.carbtn.Location = new System.Drawing.Point(6, 9);
            this.carbtn.Name = "carbtn";
            this.carbtn.Size = new System.Drawing.Size(121, 30);
            this.carbtn.TabIndex = 10;
            this.carbtn.Text = "Car";
            this.carbtn.UseVisualStyleBackColor = false;
            this.carbtn.Click += new System.EventHandler(this.AnnotateTypeClick);
            // 
            // Infosigns
            // 
            this.Infosigns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Infosigns.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Infosigns.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
            this.Infosigns.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(79)))));
            this.Infosigns.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Infosigns.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.Infosigns.Location = new System.Drawing.Point(151, 141);
            this.Infosigns.Name = "Infosigns";
            this.Infosigns.Size = new System.Drawing.Size(121, 30);
            this.Infosigns.TabIndex = 10;
            this.Infosigns.Text = "Info signs";
            this.Infosigns.UseVisualStyleBackColor = false;
            // 
            // Petestrain
            // 
            this.Petestrain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Petestrain.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Petestrain.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
            this.Petestrain.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(79)))));
            this.Petestrain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Petestrain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.Petestrain.Location = new System.Drawing.Point(151, 108);
            this.Petestrain.Name = "Petestrain";
            this.Petestrain.Size = new System.Drawing.Size(121, 30);
            this.Petestrain.TabIndex = 10;
            this.Petestrain.Text = "Petestrain";
            this.Petestrain.UseVisualStyleBackColor = false;
            // 
            // Motorcycle
            // 
            this.Motorcycle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Motorcycle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Motorcycle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
            this.Motorcycle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(79)))));
            this.Motorcycle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Motorcycle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.Motorcycle.Location = new System.Drawing.Point(151, 75);
            this.Motorcycle.Name = "Motorcycle";
            this.Motorcycle.Size = new System.Drawing.Size(121, 30);
            this.Motorcycle.TabIndex = 10;
            this.Motorcycle.Text = "Motorcycle";
            this.Motorcycle.UseVisualStyleBackColor = false;
            // 
            // TrunkTrailer
            // 
            this.TrunkTrailer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.TrunkTrailer.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.TrunkTrailer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
            this.TrunkTrailer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(79)))));
            this.TrunkTrailer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TrunkTrailer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.TrunkTrailer.Location = new System.Drawing.Point(151, 42);
            this.TrunkTrailer.Name = "TrunkTrailer";
            this.TrunkTrailer.Size = new System.Drawing.Size(121, 30);
            this.TrunkTrailer.TabIndex = 10;
            this.TrunkTrailer.Text = "Trunk Trailer";
            this.TrunkTrailer.UseVisualStyleBackColor = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(53, 635);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(193, 25);
            this.textBox2.TabIndex = 9;
            this.textBox2.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label4.Location = new System.Drawing.Point(63, 617);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Or input  type:";
            this.label4.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LemonChiffon;
            this.button1.Location = new System.Drawing.Point(44, 675);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(193, 34);
            this.button1.TabIndex = 3;
            this.button1.Text = "Confirm";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "A",
            "B",
            "C"});
            this.comboBox2.Location = new System.Drawing.Point(12, 682);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(26, 23);
            this.comboBox2.TabIndex = 7;
            this.comboBox2.Visible = false;
            // 
            // pan_picture
            // 
            this.pan_picture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pan_picture.Controls.Add(this.Suo);
            this.pan_picture.Controls.Add(this.pictureBox1);
            this.pan_picture.Location = new System.Drawing.Point(-4, 85);
            this.pan_picture.Name = "pan_picture";
            this.pan_picture.Size = new System.Drawing.Size(1590, 711);
            this.pan_picture.TabIndex = 19;
            // 
            // ToolBar
            // 
            this.ToolBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ToolBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.ToolBar.Controls.Add(this.Last);
            this.ToolBar.Controls.Add(this.Next);
            this.ToolBar.Controls.Add(this.Bigger);
            this.ToolBar.Controls.Add(this.button3);
            this.ToolBar.Controls.Add(this.BackOne);
            this.ToolBar.Controls.Add(this.Around);
            this.ToolBar.Controls.Add(this.LeftRight);
            this.ToolBar.Controls.Add(this.Updown);
            this.ToolBar.Controls.Add(this.Smaller);
            this.ToolBar.Location = new System.Drawing.Point(1588, 85);
            this.ToolBar.Name = "ToolBar";
            this.ToolBar.Size = new System.Drawing.Size(41, 719);
            this.ToolBar.TabIndex = 23;
            this.ToolBar.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // Last
            // 
            this.Last.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Last.FlatAppearance.BorderSize = 0;
            this.Last.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Last.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Last.Image = ((System.Drawing.Image)(resources.GetObject("Last.Image")));
            this.Last.Location = new System.Drawing.Point(7, 273);
            this.Last.Name = "Last";
            this.Last.Size = new System.Drawing.Size(28, 28);
            this.Last.TabIndex = 0;
            this.Last.UseVisualStyleBackColor = false;
            this.Last.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // Next
            // 
            this.Next.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Next.FlatAppearance.BorderSize = 0;
            this.Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Next.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Next.Image = ((System.Drawing.Image)(resources.GetObject("Next.Image")));
            this.Next.Location = new System.Drawing.Point(7, 308);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(28, 28);
            this.Next.TabIndex = 0;
            this.Next.UseVisualStyleBackColor = false;
            this.Next.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // Bigger
            // 
            this.Bigger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Bigger.FlatAppearance.BorderSize = 0;
            this.Bigger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Bigger.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Bigger.Image = ((System.Drawing.Image)(resources.GetObject("Bigger.Image")));
            this.Bigger.Location = new System.Drawing.Point(5, 10);
            this.Bigger.Name = "Bigger";
            this.Bigger.Size = new System.Drawing.Size(28, 28);
            this.Bigger.TabIndex = 0;
            this.Bigger.UseVisualStyleBackColor = false;
            this.Bigger.Click += new System.EventHandler(this.Bigger_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(5, 238);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(28, 28);
            this.button3.TabIndex = 0;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // BackOne
            // 
            this.BackOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.BackOne.FlatAppearance.BorderSize = 0;
            this.BackOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackOne.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.BackOne.Image = ((System.Drawing.Image)(resources.GetObject("BackOne.Image")));
            this.BackOne.Location = new System.Drawing.Point(6, 201);
            this.BackOne.Name = "BackOne";
            this.BackOne.Size = new System.Drawing.Size(28, 28);
            this.BackOne.TabIndex = 0;
            this.BackOne.UseVisualStyleBackColor = false;
            this.BackOne.Click += new System.EventHandler(this.BackOne_Click);
            // 
            // Around
            // 
            this.Around.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Around.FlatAppearance.BorderSize = 0;
            this.Around.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Around.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Around.Image = ((System.Drawing.Image)(resources.GetObject("Around.Image")));
            this.Around.Location = new System.Drawing.Point(7, 159);
            this.Around.Name = "Around";
            this.Around.Size = new System.Drawing.Size(28, 28);
            this.Around.TabIndex = 0;
            this.Around.UseVisualStyleBackColor = false;
            this.Around.Click += new System.EventHandler(this.Around_Click);
            // 
            // LeftRight
            // 
            this.LeftRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.LeftRight.FlatAppearance.BorderSize = 0;
            this.LeftRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LeftRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.LeftRight.Image = ((System.Drawing.Image)(resources.GetObject("LeftRight.Image")));
            this.LeftRight.Location = new System.Drawing.Point(6, 120);
            this.LeftRight.Name = "LeftRight";
            this.LeftRight.Size = new System.Drawing.Size(28, 28);
            this.LeftRight.TabIndex = 0;
            this.LeftRight.UseVisualStyleBackColor = false;
            this.LeftRight.Click += new System.EventHandler(this.LeftRight_Click);
            // 
            // Updown
            // 
            this.Updown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Updown.FlatAppearance.BorderSize = 0;
            this.Updown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Updown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Updown.Image = ((System.Drawing.Image)(resources.GetObject("Updown.Image")));
            this.Updown.Location = new System.Drawing.Point(7, 83);
            this.Updown.Name = "Updown";
            this.Updown.Size = new System.Drawing.Size(28, 28);
            this.Updown.TabIndex = 0;
            this.Updown.UseVisualStyleBackColor = false;
            this.Updown.Click += new System.EventHandler(this.Updown_Click);
            // 
            // Smaller
            // 
            this.Smaller.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Smaller.FlatAppearance.BorderSize = 0;
            this.Smaller.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Smaller.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Smaller.Image = ((System.Drawing.Image)(resources.GetObject("Smaller.Image")));
            this.Smaller.Location = new System.Drawing.Point(5, 47);
            this.Smaller.Name = "Smaller";
            this.Smaller.Size = new System.Drawing.Size(28, 28);
            this.Smaller.TabIndex = 0;
            this.Smaller.UseVisualStyleBackColor = false;
            this.Smaller.Click += new System.EventHandler(this.Smaller_Click);
            // 
            // TitlePanel
            // 
            this.TitlePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TitlePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.TitlePanel.Controls.Add(this.pictureBox2);
            this.TitlePanel.Controls.Add(this.lblProgress);
            this.TitlePanel.Controls.Add(this.fen);
            this.TitlePanel.Controls.Add(this.CrtBucketName);
            this.TitlePanel.Controls.Add(this.UserName);
            this.TitlePanel.Controls.Add(this.pictureBox4);
            this.TitlePanel.Controls.Add(this.TitleName);
            this.TitlePanel.Controls.Add(this.CloseWin);
            this.TitlePanel.Controls.Add(this.MinWin);
            this.TitlePanel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.TitlePanel.Location = new System.Drawing.Point(0, 0);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new System.Drawing.Size(1906, 50);
            this.TitlePanel.TabIndex = 20;
            this.TitlePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TitlePanel_Paint);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(14, 11);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(37, 37);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // fen
            // 
            this.fen.AutoSize = true;
            this.fen.Font = new System.Drawing.Font("楷体", 15F);
            this.fen.Location = new System.Drawing.Point(401, 15);
            this.fen.Name = "fen";
            this.fen.Size = new System.Drawing.Size(25, 25);
            this.fen.TabIndex = 5;
            this.fen.Text = "|";
            // 
            // CrtBucketName
            // 
            this.CrtBucketName.AutoSize = true;
            this.CrtBucketName.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CrtBucketName.Location = new System.Drawing.Point(447, 19);
            this.CrtBucketName.Name = "CrtBucketName";
            this.CrtBucketName.Size = new System.Drawing.Size(39, 19);
            this.CrtBucketName.TabIndex = 5;
            this.CrtBucketName.Text = "car";
            // 
            // UserName
            // 
            this.UserName.AutoSize = true;
            this.UserName.Font = new System.Drawing.Font("隶书", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.UserName.Location = new System.Drawing.Point(295, 18);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(49, 19);
            this.UserName.TabIndex = 4;
            this.UserName.Text = "wang";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(262, 11);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(32, 25);
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // TitleName
            // 
            this.TitleName.AutoSize = true;
            this.TitleName.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TitleName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.TitleName.Location = new System.Drawing.Point(57, 15);
            this.TitleName.Name = "TitleName";
            this.TitleName.Size = new System.Drawing.Size(142, 26);
            this.TitleName.TabIndex = 2;
            this.TitleName.Text = "AnnotateTool";
            // 
            // CloseWin
            // 
            this.CloseWin.FlatAppearance.BorderSize = 0;
            this.CloseWin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(79)))));
            this.CloseWin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(79)))));
            this.CloseWin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseWin.Image = ((System.Drawing.Image)(resources.GetObject("CloseWin.Image")));
            this.CloseWin.Location = new System.Drawing.Point(1833, 10);
            this.CloseWin.Name = "CloseWin";
            this.CloseWin.Size = new System.Drawing.Size(37, 37);
            this.CloseWin.TabIndex = 1;
            this.CloseWin.UseVisualStyleBackColor = true;
            this.CloseWin.Click += new System.EventHandler(this.CloseWin_Click);
            // 
            // MinWin
            // 
            this.MinWin.FlatAppearance.BorderSize = 0;
            this.MinWin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(184)))), ((int)(((byte)(223)))));
            this.MinWin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(184)))), ((int)(((byte)(223)))));
            this.MinWin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinWin.Image = ((System.Drawing.Image)(resources.GetObject("MinWin.Image")));
            this.MinWin.Location = new System.Drawing.Point(1791, 9);
            this.MinWin.Name = "MinWin";
            this.MinWin.Size = new System.Drawing.Size(37, 37);
            this.MinWin.TabIndex = 0;
            this.MinWin.UseVisualStyleBackColor = true;
            this.MinWin.Click += new System.EventHandler(this.MinWin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 14F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.label1.Location = new System.Drawing.Point(40, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Visible = false;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.AutoSize = true;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(2, 85);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(211, 365);
            this.panel2.TabIndex = 6;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProgress.Location = new System.Drawing.Point(563, 19);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(59, 19);
            this.lblProgress.TabIndex = 5;
            this.lblProgress.Text = "total";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(1906, 800);
            this.ControlBox = false;
            this.Controls.Add(this.ToolBar);
            this.Controls.Add(this.TitlePanel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pan_picture);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pan_picture.ResumeLayout(false);
            this.ToolBar.ResumeLayout(false);
            this.TitlePanel.ResumeLayout(false);
            this.TitlePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

      

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel pan_picture;
        private System.Windows.Forms.ComboBox toolStripComboBox1;
        private System.Windows.Forms.Button Annotate;
        private System.Windows.Forms.Button Suo;
        private System.Windows.Forms.Panel TitlePanel;
        private System.Windows.Forms.Button CloseWin;
        private System.Windows.Forms.Button MinWin;
        private System.Windows.Forms.Label TitleName;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label UserName;
        private System.Windows.Forms.Label CrtBucketName;
        private System.Windows.Forms.Label fen;
        private System.Windows.Forms.CheckBox TimerOC;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel ToolBar;
        private System.Windows.Forms.Label TypeTab;
        private System.Windows.Forms.Label AnnotateTag;
        private System.Windows.Forms.Button Last;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.Button Smaller;
        private System.Windows.Forms.Button Bigger;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button BackOne;
        private System.Windows.Forms.Button Around;
        private System.Windows.Forms.Button LeftRight;
        private System.Windows.Forms.Button Updown;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Button timerInternal;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel annoteLine;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button TrunkTrailer;
        private System.Windows.Forms.Button Trunk;
        private System.Windows.Forms.Button carbtn;
        private System.Windows.Forms.Button Bus;
        private System.Windows.Forms.Button Bicycle;
        private System.Windows.Forms.Button Motorcycle;
        private System.Windows.Forms.Button Cyclist;
        private System.Windows.Forms.Button Petestrain;
        private System.Windows.Forms.Button Special;
        private System.Windows.Forms.Button Trafficsigns;
        private System.Windows.Forms.Button Dontcare;
        private System.Windows.Forms.Button Infosigns;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button SaveJson;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label lblProgress;
    }
}

