using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Qiniu.Util;
using RemarkTag.Qiniu;
using NLog;
using System.Threading;
namespace RemarkTag
{
    public partial class Form1 : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static public bool IsLogOn = false;
        static public string selectStr = "";
       
        static List<bucketObj> bucketList = null;
        static public string CrtBucketDomain = "";
        static public int CrtBucketId = 0;
        static public List<int> CrtPicPos = new List<int>();//对数组bucketList大小一致
     
      
         public int TimerInterval = 1;//定时器时间间隔

         int crtTypeBtn = 0;

         private bool isSelected = false;
         private static int zoomtime = 0;        //缩放次数，负为缩小正为放大
         private static Image img_ori;           //初始Img
         private Point mouseDownPoint = new Point();
         private static bool IsDrawRect = false;

         protected bool isDown;//是否按下鼠标左键
         private bool isDrag;//是否拖拽
         Point mousePoint = new Point(0, 0);
         Point startPoint = new Point(0, 0);
         Point endPoint = new Point(0, 0);
      
         private Rectangle rc = new Rectangle();
         private DireTypes dirType = DireTypes.Null;
         public List<Rectangle> TotalRects = new List<Rectangle>();

         public static string CrtAnnotateName = "";
        public Form1()
        {
            InitializeComponent();

            
            this.WindowState = FormWindowState.Maximized;

            BaseBll bll = new BaseBll();
            this.timerInternal.Text = bll.TimerSet.ToString();
           // TimerInterval = bll.TimerSet;
            this.Annotate.Visible = false;
           // ListBucketReturn Buckets = new ListBucketReturn();
            this.CloseWin.Left = 1480;
            this.MinWin.Left = 1450;
            this.button4.Left = 1460;
           
            this.UserName.Text = BaseBll.CrtUser;
            this.panel2.Visible = false;
            this.Suo.Visible = false;
            this.panel5.Visible = false;
            this.annoteLine.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ShowIcon = false;
            if (this.TimerOC.Checked)
            {
                this.timerInternal.Visible = true;
            }
            else {
                this.timerInternal.Visible = false;
            }
            this.KeyPreview = true;
        }

        /// <summary>
        /// 获取焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBox_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Focus();
            if (toolStripComboBox1.Text.Trim() == "Enable Zoom")
            {
                OperateClass.resetPic(pictureBox1);
            }
        }

        /// <summary>
        /// 滚动缩放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBox_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int numberOfTextLinesToMove = 0;
            numberOfTextLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / 120;

            //if (toolStripComboBox1.Text.Trim() != "Enable Zoom") numberOfTextLinesToMove = 0;
            if (numberOfTextLinesToMove > 0)
            {
                pictureBox1.Dock = DockStyle.None;
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                for (int i = 0; i < numberOfTextLinesToMove; i++)
                {
                    zoomtime++;

                    OperateClass.maxMin(pictureBox1, img_ori, zoomtime,numberOfTextLinesToMove,i);//---放大逻辑
            

                }
            }
            else if (numberOfTextLinesToMove < 0)
            {
                int MinW = 800;
                if(OperateClass.IsMin(pictureBox1,MinW)){
                    for (int i = 0; i > numberOfTextLinesToMove; i--)
                    {
                        zoomtime--;
                        OperateClass.maxMin(pictureBox1, img_ori, zoomtime,numberOfTextLinesToMove,i);//---缩小逻辑
                    
                      
                        Application.DoEvents();
                    }
                }
            }

        }


        #region  拖动
        private void picBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                if (IsDrawRect == false)//拖拽
                {
                   
                    isSelected = true;
                }
                else
                {//绘制矩形
                    TotalRects.Clear();
                    //startPoint = new Point(Cursor.Position.X, Cursor.Position.Y);
                    int x = e.Location.X;
                    int y = e.Location.Y;
                    startPoint = new Point(x, y);
                    Invalidate();
                    isSelected = true;
                    IsDrawRect = true;
                }
            }
        }
        private void picBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelected && IsMouseInPanel())
            {
                if(IsDrawRect == false)
                {
                    #region

                    double ScaleWidth = -0.9 * pictureBox1.Width;
                    int LastRight = (int)ScaleWidth;//pictureBox2.Location.X + pictureBox2.Width;

                    if (this.pictureBox1.Left + (Cursor.Position.X - mouseDownPoint.X) <= LastRight)
                    {
                        this.pictureBox1.Left = LastRight;
                    }
                    else
                    {
                        this.pictureBox1.Left = this.pictureBox1.Left + (Cursor.Position.X - mouseDownPoint.X);
                    }

                    #endregion
                    this.pictureBox1.Left = this.pictureBox1.Left + (Cursor.Position.X - mouseDownPoint.X);
                    this.pictureBox1.Top = this.pictureBox1.Top + (Cursor.Position.Y - mouseDownPoint.Y);

                    mouseDownPoint.X = Cursor.Position.X;
                    mouseDownPoint.Y = Cursor.Position.Y;
                  }
                else
                {//绘制矩形
                    if (e.Button != MouseButtons.Left)//判断是否按下左键
                        return;
                    Point tempEndPoint = e.Location; //记录框的位置和大小
                    rc.Location = new Point(
                    Math.Min(startPoint.X, tempEndPoint.X),
                    Math.Min(startPoint.Y, tempEndPoint.Y));
                    rc.Size = new Size(
                    Math.Abs(startPoint.X - tempEndPoint.X),
                    Math.Abs(startPoint.Y - tempEndPoint.Y));
                    TotalRects.Add(rc);
                    pictureBox1.Invalidate();
                }
            }
        }
        //定义两个变量 
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            if (IsDrawRect)
            {
                Pen pen = new Pen(Color.Red);
                pen.Width = 2;
                //实时的画矩形
                Graphics g = e.Graphics;
                g.DrawRectangle(pen, rc);
                pen.Dispose();

            }

        }
        private bool IsMouseInPanel()
        {
            if (this.pan_picture.Left < PointToClient(Cursor.Position).X && PointToClient(Cursor.Position).X < this.pan_picture.Left + this.pan_picture.Width
                && this.pan_picture.Top < PointToClient(Cursor.Position).Y && PointToClient(Cursor.Position).Y < this.pan_picture.Top + this.pan_picture.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void picBox_MouseUp(object sender, MouseEventArgs e)
        {
          
            Pen pen = new Pen(Color.Yellow);
            pen.Width = 2;
            //实时的画矩形
            if (TotalRects.Count > 0)
            {
                if (pictureBox1.Image == null) {
                    return;
                }
                Bitmap img = new Bitmap(pictureBox1.Image);
                Rectangle lastRect = TotalRects.ElementAt(TotalRects.Count - 1);
                Graphics g = Graphics.FromImage(img);
                g.DrawRectangle(pen, lastRect);

                Font font = new Font("宋体", 9);
                Point p = new Point(lastRect.X, lastRect.Y);
                g.DrawString("Car", font, new SolidBrush(Color.Yellow), p);

                pictureBox1.Image = img;
            }
            isSelected = false;
            IsDrawRect = false;//设为false后，就切换到拖拽模式
        }
        //pictureBox上面双击事件,图片恢复原始大小
        void pictureBox1_DoubleClick(object sender, System.EventArgs e)
        {
            // throw new System.NotImplementedException();
            //pictureBox1.Location = new Point(0, 0);
            //pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            int leftPost = 50;
            pictureBox1.Parent = pan_picture;
            pictureBox1.SetBounds(leftPost, 0, pan_picture.Width-150, pan_picture.Height);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            isDown = false;
            isDrag = false;
          
            Invalidate();
           
        }
        #endregion

        bool MouseIsDown = false;
        Rectangle MouseRect = Rectangle.Empty; //矩形（为鼠标画出矩形选区）
 
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!IsLogOn) {
                MessageBox.Show("Please log in first！");
                return;
            }
            this.timer1.Enabled = false;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!IsLogOn)
            {
                MessageBox.Show("Please log in first！");
                return;
            }
            this.timer1.Enabled = false;
            int w = e.X;
            if (e.Button == MouseButtons.Left)
            {
                int i = e.X / 32;
                int j = e.Y / 64;
                //int resultNo = i + (col + 1) * j;

            }
            if (e.Button == MouseButtons.Right)
            {
                //this.contextMenuStrip1.Show(this, e.X, e.Y);
            }
        }
        //获取屏幕图像
        public Bitmap GetScreen()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, bmp.Size);
            }
            return bmp;
        }
        //鼠标滚动影响事件
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {

            if (!IsLogOn)
            {
                MessageBox.Show("Please log in first！");
                return;
            }

            Bitmap m_bmp;               //画布中的图像
            Point m_ptCanvas;           //画布原点在设备上的坐标
            //Point m_ptCanvasBuf;        //重置画布坐标计算时用的临时变量
            
            System.Drawing.Size t = pictureBox1.Size;

            t.Width += (int)(e.Delta * 1.1f);
            t.Height += (int)(e.Delta * 1.1f);
            float m_nScale = 1.0F;      //缩放比例

            // Point m_ptMouseDown;        //鼠标点下是在设备坐标上的坐标

            // string m_strMousePt;        //鼠标当前位置对应的坐标
            m_bmp = new Bitmap(this.pictureBox1.Image);//GetScreen();
            //初始化 坐标
            m_ptCanvas = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);//new Point(this.pictureBox1.Location.X, this.pictureBox1.Location.Y);//
            // m_ptBmp = new Point(-(m_bmp.Width / 2), -(m_bmp.Height / 2));

            if (m_nScale <= 0.3 && e.Delta <= 0) return;        //缩小下线
            if (m_nScale >= 4.9 && e.Delta >= 0) return;        //放大上线
            if (e.Delta < 0)//xiao
            {
                if (m_bmp.Height <= 990) return;//已经就最小了，不能缩小

                //获取 当前点到画布坐标原点的距离
                SizeF szSub = (Size)m_ptCanvas - (Size)e.Location;
                //当前的距离差除以缩放比还原到未缩放长度
                float tempX = szSub.Width / m_nScale;           //这里
                float tempY = szSub.Height / m_nScale;          //将画布比例
                //还原上一次的偏移                               //按照当前缩放比还原到
                m_ptCanvas.X -= (int)(szSub.Width - tempX);     //没有缩放
                m_ptCanvas.Y -= (int)(szSub.Height - tempY);    //的状态
                //重置距离差为  未缩放状态                       
                szSub.Width = tempX;
                szSub.Height = tempY;
                m_nScale += e.Delta > 0 ? 0.2F : -0.2F;
                //重新计算 缩放并 重置画布原点坐标
                m_ptCanvas.X += (int)(szSub.Width * m_nScale - szSub.Width);
                m_ptCanvas.Y += (int)(szSub.Height * m_nScale - szSub.Height);
                pictureBox1.Invalidate();

                Image thisImg = this.pictureBox1.Image;


                //设置最小显示大小
                if (t.Width < 938)
                    t.Width = 938;
                if (t.Height < 745)
                    t.Height = 745;

                //设置最大显示大小
                if (t.Width > 1000)
                    t.Width = 1000;
                if (t.Height > 850)
                    t.Height = 850;
                this.pictureBox1.Width = t.Width;
                this.pictureBox1.Height = t.Height;
            }
            else//da
            {

                int OldWith = this.pictureBox1.Size.Width;//m_bmp.Width;
                int OldHeight = this.pictureBox1.Size.Height ;
                m_nScale = (float)m_bmp.Width / t.Width;

                int X = e.X; int Y = e.Y;
                int NewWith = (int)(m_bmp.Width * m_nScale);
                int NewHeight = (int)(m_bmp.Height * m_nScale);
                Size size = new Size(NewWith,NewHeight);
                Bitmap bmSmall = new Bitmap(m_bmp, size);

                BaseBll bBll = new BaseBll();
                bmSmall = bBll.GetThumbnail(m_bmp, NewHeight, NewWith);

                //获取 当前点到画布坐标原点的距离
                int x0 = (int)(X * (1 - 1.0/m_nScale));
                int y0 = (int)(Y * (1 - 1.0/m_nScale));

                x0 = (int)(X * m_nScale);
                y0 = (int)(Y * m_nScale);
                
              //xian yong chazhifa ba fangda xieru bmSmall, ruanhou cong zhong jiequ picuture daxiao de xin bitmap 
                Size s = new Size(OldWith, OldHeight);
                //Bitmap  = m_bmp;
                Rectangle rect = new Rectangle(x0, y0, OldWith, OldHeight);
                Bitmap desBitmap = bmSmall.Clone(rect, bmSmall.PixelFormat);
                pictureBox1.Invalidate();
                pictureBox1.Refresh();
                pictureBox1.Invalidate();
                this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = desBitmap;
             
            }
        }


        //响应界面的键盘上下左右键,enter键，space键
         protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
         {
             //List<string> typesFor1 = bucketList[LabelIndex - 1].buckettypes;
             switch (keyData)
             {

                 case Keys.Right:

                     //MessageBox.Show("Right");
                     this.pictureBox3_Click(null, null);
                     break;

                 case Keys.Left:

                    // MessageBox.Show("Left");
                     this.pictureBox2_Click(null, null);
                     break;

                 case Keys.Up://方向键不反应

                     //MessageBox.Show("up");
                      crtTypeBtn--;
                     //this.SelectTypeClick(null, null);
                     break;

                 case Keys.Down:

                     //MessageBox.Show("down");
                     crtTypeBtn++;
                     //this.SelectTypeClick(null, null);
                     break;

                 case Keys.D1:
                     crtTypeBtn = 1;

                     this.SelectTypeClick(null, null);
                     break;
                 case Keys.D2:
                     crtTypeBtn = 2;
                     this.SelectTypeClick(null, null);
                     break;
                 case Keys.D3:
                     crtTypeBtn = 3;
                     this.SelectTypeClick(null, null);
                     break;
                 case Keys.D4:
                     crtTypeBtn = 4;
                     this.SelectTypeClick(null, null);
                    
                     break;
                 case Keys.D5:
                     crtTypeBtn = 5;
                     this.SelectTypeClick(null, null);
                     break;
                 case Keys.D6:
                      crtTypeBtn = 6;
                     this.SelectTypeClick(null, null);
                     break;
                 case Keys.D7:
                     crtTypeBtn = 7;
                     this.SelectTypeClick(null, null);
                     break;
                 case Keys.D8:
                     crtTypeBtn = 8;
                     this.SelectTypeClick(null, null);
                     break;
                 case Keys.D9:
                     crtTypeBtn = 9;
                     this.SelectTypeClick(null, null);
                     break;
                 case Keys.A:
                      crtTypeBtn = 10;
                     this.SelectTypeClick(null, null);
                     break;
                 case Keys.B:
                     crtTypeBtn = 11;
                     this.SelectTypeClick(null, null);
                     break;
                 case Keys.C:
                     crtTypeBtn = 12;
                     this.SelectTypeClick(null, null);
                     break;
                 case Keys.D:
                     crtTypeBtn = 13;
                     this.SelectTypeClick(null, null);
                     break;
                 case Keys.E:
                     crtTypeBtn = 14;
                     this.SelectTypeClick(null, null);
                     break;
                 case Keys.F:
                     crtTypeBtn = 15;
                     this.SelectTypeClick(null, null);
                     break;
                 case Keys.G:
                     crtTypeBtn = 16;
                     this.SelectTypeClick(null, null);
                     break;
                 case Keys.H:
                     crtTypeBtn = 17;
                     this.SelectTypeClick(null, null);
                     break;
                 case Keys.I:
                     crtTypeBtn = 18;
                     this.SelectTypeClick(null, null);
                     break;
                 default:
                     break;
                     
             }

             //return false;//如果要调用KeyDown,这里一定要返回false才行,否则只响应重写方法里的按键.

             //这里调用一下父类方向,相当于调用普通的KeyDown事件.//所以按空格会弹出两个对话框

             return base.ProcessCmdKey(ref msg, keyData);

         }
       
        //按下事件
         private void timerInternal_KeyPress(object sender, EventArgs e)
         {
            // MessageBox.Show("timmerInternal");
         }
       
        //获得bucketlist，并显示
        private void buttonImportBudget_Click(object sender, EventArgs e)
        {
            if (this.timer1.Enabled == true)
            {
                this.timer1.Enabled = false;
            }
            this.panel2.Visible = true;
            this.Suo.Visible = true;
            CrtPicPos.Clear();
            BaseBll.ImPotntPt.Clear();
            if (!IsLogOn)
            {
                MessageBox.Show("Please log in first！");
                return;
            }

            Data d = new Data();
            ListBucketReturn Buckets = d.GetBudgetList(BaseBll.access_Token);
            if(Buckets == null){
                MessageBox.Show("No bucket now");
                return;
            }
            bucketList = Buckets.data.buckets;

            int len = bucketList.Count;//bucket list currently
          
            //  this.button5.Location = new System.Drawing.Point(3, 0);
            //this.button5.Name = "button5";
            //this.button5.Size = new System.Drawing.Size(96, 36);
            if (len > 12) { return; }
            for (int i = 0; i < bucketList.Count; i++)
            {
                Label AppLabel = new Label();

                AppLabel.Text = bucketList[i].name;

                //AppButton.Tag = app.AppAddress;
                AppLabel.MouseClick += new MouseEventHandler(label1_Click);
                AppLabel.Size = new System.Drawing.Size(120, 36);
                AppLabel.Location = new System.Drawing.Point(4, (8 + 36 * i));
                AppLabel.Font = new System.Drawing.Font("黑体", 14F);
                AppLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
                AppLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                panel2.Controls.Add(AppLabel);
                //将显示buckets的总个数
                Label AppLabelNum = new Label();
                AppLabelNum.Text = bucketList[i].totalfilenum;
                AppLabelNum.MouseClick += new MouseEventHandler(label1_Click);
                AppLabelNum.Size = new System.Drawing.Size(25, 36);
                AppLabelNum.Location = new System.Drawing.Point(123, (8 + 36 * i));
                AppLabelNum.Font = new System.Drawing.Font("黑体", 12F);
                AppLabelNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
                AppLabelNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                this.panel2.Controls.Add(AppLabelNum);
            }
            this.panel2.Size = new System.Drawing.Size(160, (20 + 36 * bucketList.Count));
            this.Suo.Left = panel2.Right + 1;
            //this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            for (int i = 0; i < len; i++)
            {
                CrtPicPos.Add(0);
                BaseBll.ImPotntPt.Add("");
            }

        }
        void ckb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox rbt = (CheckBox)sender;
            selectStr = rbt.Text;
        }
        //遍历所有控件
        public void ForeachPanelControls(string ctrlName)
        {
            if (ctrlName == "")
            {
                foreach (Control ctrl in panel3.Controls)
                {
                    if (ctrl is Button)
                    {
                        ctrl.BackColor = Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
                    }
                    if (ctrl is Label)
                    {
                        ctrl.BackColor = Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
                    }
                }
                return; 
            }
            foreach (Control ctrl in panel3.Controls)
            {
                if (ctrl is Button)
                {
                    string buttonText = ctrlName;
                    if (ctrl.Text.IndexOf(ctrlName) == 0)
                    {
                        ctrl.BackColor = Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
                        ctrl.ForeColor = System.Drawing.Color.White;
                        ctrl.Focus();
                    }
                    else
                    {
                        ctrl.BackColor =Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
                        ctrl.ForeColor = Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
                    }
                   
                }
                if (ctrl is Label) { 
                    //huangqiaoyun
                    string buttonText = ctrlName;
                    if (ctrl.Name.IndexOf(ctrlName) == 0)
                    {
                        ctrl.BackColor = Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
                        ctrl.ForeColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        ctrl.BackColor = Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
                        ctrl.ForeColor = Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
                    }
                }
             
            }
        }
        //根据bucket名找到编号
        protected int GetBucketIdByName(string Name) {
            int rResult = -1;
            if (bucketList == null)
            {
                rResult = - 1;
            }
            else {
                for (int i = 0; i < bucketList.Count; i++) {
                    if (bucketList[i].name == Name)
                    {
                        rResult = i;
                        break;
                    }
                }
            }
            return rResult;
        }
        //遍历所有控件
        public void ForeachPanel2Labels(string ctrlName)
        {
            foreach (Control ctrl in panel2.Controls)
            {
                if (ctrl is Label)
                {
                    string buttonText = ctrlName;
                    if (ctrl.Text.Trim() == ctrlName.Trim())
                    {
                       
                        ctrl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
                        ctrl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
                        ctrl.Focus();
                    }
                    else
                    {
                        ctrl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
                        ctrl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
                    }

                }

            }
        }
        //遍历toolBar中所有的小按钮
        public void ForeachBtnsInToolBar(string Name) {
            foreach (Button ctrl in  this.ToolBar.Controls)
            {
                if (ctrl is Button)
                {
                    string ctrlName = ctrl.Name;
                    if (ctrlName.Trim() == Name)
                    {
                        ctrl.BackColor = Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
                    }
                    else {
                        ctrl.BackColor = Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
                    }
                }
            }
        }
        /// <summary>
        /// 点击bucketName,从而下载图片文件，并动态显示RadioButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
             Data d = new Data();     
            Label MyLab = new Label();
            MyLab = (Label)sender;

      
            string BucketName = MyLab.Text;
            //现在得根据bucket名称得到期编号
            int IndexID = GetBucketIdByName(BucketName);

            if (IndexID == -1) {
                MessageBox.Show("Can not find this bucket!");
                return;
            }
            ForeachPanel2Labels(BucketName);
            CrtBucketId = IndexID;
            prePareUI(IndexID + 1);
            SetComboAndPic(IndexID + 1, false);
            //每一个label，对应一个bucket项目，数量级最多在10几个的数量级
        
        }
        public void prePareUI(int LabelIndex) {         
            Data d = new Data();
            string thisPoint = BaseBll.ImPotntPt[LabelIndex - 1];
            if (thisPoint == "")
            { //首次打开客户端
                string address = d.GetPointFile(bucketList[LabelIndex - 1].name);
                if (address == "") { MessageBox.Show("Please check if the key file is out of date!"); return; }
                Updown ud = new Updown();
                thisPoint = ud.GetFileContent(address);
                BaseBll.ImPotntPt[LabelIndex - 1] = thisPoint;
            }
            this.CrtBucketName.Text = bucketList[CrtBucketId].name;
            this.lblProgress.Text = String.Format("{0} / {1}", bucketList[CrtBucketId].annotatedfilenum, bucketList[CrtBucketId].totalfilenum);
            //放置的图片不能被压缩，不一定要填满整个pictureBox, 点击可以放大或缩小
            List<string> typesFor1 = bucketList[LabelIndex - 1].buckettypes;
            //动态设置类型按钮，并增加热键
            SetTypeButton(typesFor1);
            int len = typesFor1.Count;
            string[] Types1 = new string[len];

            for (int i = 0; i < len; i++)
            {
                string typeItem = typesFor1.ElementAt(i);
                Types1[i] = typeItem;

            }
            this.comboBox1.Items.Clear();
            this.comboBox1.Items.AddRange(Types1);
            button3_Click(null, null);
          
            string Status = bucketList[CrtBucketId].status;
            if (Status != "type") { 
                //需要绘制矩形才能标注
                //this.Annotate.Text = "EndAnnotate";
                this.Annotate.Visible = true;
            }
        }
        //获取未被标注的第一张图片
        public string GetNotTypedFirst(int LabelIndex,out string FileID)
        {
            Data d = new Data();
            PicFile crtFile = new PicFile();
            List<PicFile> files = d.GetFilesByThisBucket(bucketList[LabelIndex - 1].id);

            string MyFile = "";
            if (files != null)
            {
                crtFile = files[0];
                MyFile = d.GetOriPicFile(bucketList[LabelIndex - 1].id, crtFile, BaseBll.ImPotntPt[LabelIndex - 1], bucketList[LabelIndex - 1].domain);
                CrtPicPos[CrtBucketId] = Convert.ToInt32(files[0].fileid);
                FileID = crtFile.fileid;

                new Thread(() =>
                {
                    logger.Debug("File id in thread: " + files[1].fileid);
                    if (files[1] != null)
                    {
                        logger.Debug("In thread: step1");
                        d.GetOriPicFile(bucketList[CrtBucketId].id, files[1], BaseBll.ImPotntPt[CrtBucketId], bucketList[CrtBucketId].domain);
                        logger.Debug("In thread: step2");
                    }
                }).Start();
            }
            else {
                MyFile = "files is null";
                FileID = "noId";
            }
            return MyFile;
        } 
        //显示工作区图片
        public void ShowWorkPicture(string MyFile,string CrtFileId)
        {
            if (MyFile == "Failed to download this picture!")
            {
                MessageBox.Show("Failed to download this picture!！");
                return;
            }
            Mylocal bBll = new Mylocal();
            string Remark = bBll.GetMarkNameBy(CrtBucketId.ToString(), CrtFileId);
            ForeachPanelControls(Remark);

            int IfMarkedThenWhich = ReturnGreenNum(Remark);
            if (IfMarkedThenWhich != -1)
            {
                crtTypeBtn = IfMarkedThenWhich + 1;
            }
            Image img = Image.FromFile(MyFile);//
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = img;
            img_ori = pictureBox1.Image;
        }
        //动态设置类型
        public void SetComboAndPic(int LabelIndex,bool isNeed) {
            
            try
            {
                Data d = new Data();

                PicFile crtFile = new PicFile();

                //如果没有标注，则此时仍停在这一张，如果标注了，则此时显示下载后面两张图片，所以当前会always显示没被标注的第一张
                int crtPost = CrtPicPos[CrtBucketId];
                string MyFile = "";
                string CrtFileId = "";
                //如果已下载，则直接显示
                Mylocal bBall = new Mylocal();
                int zuihou = bBall.GetZuihouFileId(CrtBucketId.ToString());
                if (zuihou == 0)//no历史记录
                {
                    this.Last.Enabled = false; // ---->this.Last.Visible = false;
                    MyFile = GetNotTypedFirst(LabelIndex,out CrtFileId);
                    if (MyFile == "files is null") {
                        MessageBox.Show("no files in this buckets");
                        return;
                    }
                }
                else//有历史记录
                {
                    if (crtPost == 0)//有历史记录，却返回的了0，说明翻图上一张，返回值为0了,或者再次打开页面时
                    {
                        MyFile = GetNotTypedFirst(LabelIndex, out CrtFileId);
                        if (MyFile == "files is null")
                        {
                            crtFile.fileid = zuihou.ToString();
                            MyFile = d.GetOriPicFile(bucketList[LabelIndex - 1].id, crtFile, BaseBll.ImPotntPt[LabelIndex - 1], bucketList[LabelIndex - 1].domain);
                            CrtPicPos[CrtBucketId] = zuihou;
                            CrtFileId = crtFile.fileid;//---
                        }

                    }
                    else
                    {
                        if (isNeed == true)
                        { //有历史记录，返回的跟上次一样的fileId,说明要拿新文件来下载了
                            MyFile = GetNotTypedFirst(LabelIndex, out CrtFileId);

                            //List<PicFile> files = d.GetFilesByThisBucket(bucketList[LabelIndex - 1].id);
                            //crtFile = files[0];
                            //MyFile = d.GetOriPicFile(bucketList[LabelIndex - 1].id, crtFile, BaseBll.ImPotntPt[LabelIndex - 1], bucketList[LabelIndex - 1].domain);
                            //CrtPicPos[CrtBucketId] = Convert.ToInt32(files[0].fileid);
                        }
                        else
                        {
                            bool hasDownloaded = bBall.ExistFileIdInFile(CrtBucketId.ToString(), crtPost.ToString());
                            if (hasDownloaded)
                            {
                                crtFile.fileid = crtPost.ToString();
                                MyFile = d.GetOriPicFile(bucketList[LabelIndex - 1].id, crtFile, BaseBll.ImPotntPt[LabelIndex - 1], bucketList[LabelIndex - 1].domain);
                                CrtFileId = crtFile.fileid;//---
                            }
                            else
                            {//如果没下载，则取files[0]的文件
                                MyFile = GetNotTypedFirst(LabelIndex, out CrtFileId);
                                if (MyFile == "files is null")
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
                if (MyFile == "")
                {
                    MessageBox.Show("Current picture is the last one！");
                    //CrtPicPos[CrtBucketId] = 0;
                    this.timer1.Enabled = false;
                    return;
                }
                //需要定义一下string，下载图片
                if (MyFile != "Nofiles")
                {
                    ShowWorkPicture(MyFile, CrtFileId);
                   
                }
            }
            catch (Exception err) {
                logger.Debug("加载图片报错\n"+err.Message);
              
            }
     
        }
        //得到哪个按钮变为绿色，返回是类型按扭的编号
        public int ReturnGreenNum(string remark){
            if (remark == "") {
                return -1;
            }
            List<string> buckettypes = bucketList[CrtBucketId].buckettypes;
            int Total = buckettypes.Count;
            if (buckettypes.Contains(remark))
            {
                int re = -1;
                for (int i = 0; i <= Total - 1; i++) {
                    if (buckettypes[i] == remark) {
                        re = i;
                        break;
                    }
                }
                return re;
            }
            else {
                return -1;
            }
        }
        //动态设置类型按钮，并增加热键
        public void SetTypeButton(List<string> typesFor1)
        {
            #region
          
            int len = typesFor1.Count;
            if (len <= 0)
            {
                MessageBox.Show("This bucket picture has no types！");
                return;
            }
            int CrtFileId = CrtPicPos[CrtBucketId];
            string remark = "";
            if (CrtFileId == 0)
            {
                remark = "";
            }
            else
            {
                Mylocal bBll = new Mylocal();
                remark = bBll.GetMarkNameBy(CrtBucketId.ToString(), CrtFileId.ToString());
            }
            int IfMarkedThenWhich = ReturnGreenNum(remark);

            List<string> bucketTypes = bucketList[CrtBucketId].buckettypes;
            for (int i = 0; i < bucketTypes.Count; i++)
            {
                Label appLabel = new Label();
                //AppButton.KeyDown += new KeyEventHandler(AppButton_KeyDown);
                appLabel.AutoSize = true;
                appLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
                appLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                appLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
                appLabel.Location = new System.Drawing.Point(225, (30 + 46 * i));
                appLabel.Name = bucketTypes[i]+ "typeIndex";
                appLabel.Size = new System.Drawing.Size(20, 32);

                if (i <= 8)
                {
                    appLabel.Text = (i + 1).ToString();
                }
                else {
                    BaseBll bBLL = new BaseBll();
                    appLabel.Text = bBLL.getCharactor(i + 89);
                }
                if (bucketTypes[i] == remark)
                {
                    appLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
                    appLabel.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    appLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
                    appLabel.ForeColor = Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
                }
                this.panel3.Controls.Add(appLabel);
                Button AppButton = new Button();
                
                AppButton.Text = bucketTypes[i];
             

                AppButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
                AppButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
                AppButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                AppButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
                AppButton.Location = new System.Drawing.Point(41, (20 + 46 * i));
                AppButton.Name = bucketTypes[i];
                AppButton.Size = new System.Drawing.Size(200, 35);
               
                AppButton.Text = bucketTypes[i];
                AppButton.MouseClick += new MouseEventHandler(SelectTypeClick);
                AppButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                AppButton.UseVisualStyleBackColor = false;
                //将是类型的设置为
                if (bucketTypes[i] == remark)
                {
                    AppButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
                    appLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
                    
                }
                else
                {
                    AppButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
                    appLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
                }
                this.panel3.Controls.Add(AppButton); 
                
            }
            #endregion
           
         
        }
        private void AppButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1)// 
            {
                e.Handled = true;       //将Handled设置为true，指示已经处理过KeyDown事件   
                MessageBox.Show("You click 1!");
                //button1.PerformClick(); //执行单击button1的动作   
            }
        }


        private void button7_Click(object sender, EventArgs e)
        {
            IsLogOn = false;

           // this.Close();
        }

        //可以在数据加载的时候添加一些文字或图片到panel背景中
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
           
            //if (bucketList.Count > 0)
            //{
                ControlPaint.DrawBorder(e.Graphics, panel2.ClientRectangle,
           Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42))))), 1, ButtonBorderStyle.Solid, //左边
            Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42))))), 1, ButtonBorderStyle.Solid, //上边
           Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42))))), 1, ButtonBorderStyle.Solid, //右边
           Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42))))), 2, ButtonBorderStyle.Solid);//底边
            //}
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Go to next picture!");
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
        
        }
        public bool HasRemarked(string bucketid, string fileid) {
            bool rResult = false;
            Mylocal bBll = new Mylocal();

            rResult = bBll.ExistFileIdInFile(bucketid, fileid);
            return rResult;
           
        }
        //显示下一张图片（选择完类型后）
        public void GotoNextPicture() {
            Data d = new Data();
            int index = CrtPicPos[CrtBucketId];
            PicFile crtFile = new PicFile();
            Mylocal bBall = new Mylocal();
            List<PicFile> files = d.GetFilesByThisBucket(bucketList[CrtBucketId].id);
            int zuihou = bBall.GetZuihouFileId(CrtBucketId.ToString());

            string MyFile = "";
            if (zuihou == 0)//此时这种情况不会发生，因为是上传或更新成功才过来的
            {
                this.Last.Enabled = false;//----this.Last.Visible = false;

            }
            else//
            {
                if (index == zuihou)
                {
                    if (files != null)
                    {
                        crtFile = files[0];
                        MyFile = d.GetOriPicFile(bucketList[CrtBucketId].id, crtFile, BaseBll.ImPotntPt[CrtBucketId], bucketList[CrtBucketId].domain);
                        CrtPicPos[CrtBucketId] = Convert.ToInt32(files[0].fileid);
                        this.Next.Enabled = true;//----this.Next.Visible = true;
                    }
                    else { //后面没有图片了
                        MessageBox.Show("This is the last picture!");
                        return;
                    }
                }
                else
                {
                    int nextId = bBall.GetNextFileID(CrtBucketId.ToString(), index.ToString());
                    crtFile.fileid = nextId.ToString();
                    MyFile = d.GetOriPicFile(bucketList[CrtBucketId].id, crtFile, BaseBll.ImPotntPt[CrtBucketId], bucketList[CrtBucketId].domain);
                    CrtPicPos[CrtBucketId] = nextId;
                    this.Next.Enabled = true;//---this.Next.Visible = true;
                }
            }
            if (MyFile == "")
            {
                MessageBox.Show("Current picture is the last one！");
              
                return;
            }
            //需要定义一下string，下载图片
            if (MyFile != "Nofiles")
            {
                ShowWorkPicture(MyFile, crtFile.fileid);
               
            }
        }
        //定时器功能：完成显示下一步图片，并关闭定时器
        private int tenthSecond = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
           
            String str = tenthSecond.ToString();
            this.label15.Text = str;
            tenthSecond--;
            if (tenthSecond == -1)
            {
                timer1.Enabled = false;
                //--timer3.Enabled = false;
                label15.Text = "0";
                label15.ForeColor = Color.FromArgb(176, 176, 176);

                GotoNextPicture();
               
            }

        }
        //上传文件信息
        private void button2_Click(object sender, EventArgs e)
        {
            if (bucketList == null) return;
            Data d = new Data();
            List<PicFile> files = d.GetFilesByThisBucket(bucketList[CrtBucketId].id);
            if (this.comboBox1.SelectedItem == null && this.textBox1.Text == "") {
                MessageBox.Show("Please select type first!");
                this.timer1.Enabled = false;
                return;
            }
            string typeSel = (this.comboBox1.SelectedItem == null) ? this.textBox1.Text : comboBox1.SelectedItem.ToString();
            int index = CrtPicPos[CrtBucketId];
            if (HasRemarked(CrtBucketId.ToString(), (index).ToString()))//如果已经标注了，则更新标注
            {  
                string strResponse = d.UpdateRemark(CrtBucketId.ToString(),(index).ToString(), typeSel);
                if (strResponse == "success")
                {
                    //更新成功
                    MessageBox.Show("succefully updated annotation!");
                    this.comboBox1.SelectedItem = null;
                    this.textBox1.Text = "";
                    this.timer1.Enabled = false;
                    return;
                }
            }
            else
            {
                string strResponse = d.UploadRemark(CrtBucketId.ToString(), (CrtPicPos[CrtBucketId]).ToString(), typeSel);
                if (strResponse == "success")
                {
                    this.comboBox1.SelectedItem = null;
                    this.textBox1.Text = "";
                    MessageBox.Show("Successfuly upload annotation!");
                    timer1.Enabled = false;
                    return;
                  
                }
                else
                {
                    timer1.Enabled = false;
                    MessageBox.Show("Failed to upload annotation!");

                }

            }
           
        }
        //上传或更新标注
        private void SelectTypeClick(object sender, EventArgs e)
        {
            Button MyBtn = new Button();
            MyBtn = (Button)sender;
            string TypeName = "";
            if (MyBtn != null)
            {
                //每一个btn，对应一个分类，数量级最多在10几个的数量级
                string ControlName = MyBtn.Name;
                string btnText = MyBtn.Text;// typesFor1[i]: type+(&)
                //int splitPos = btnText.LastIndexOf("(&");
                TypeName = btnText;
            }
            else {
                int typeIndex = crtTypeBtn;
                if (bucketList == null) {
                    return;
                }
                if (crtTypeBtn > bucketList[CrtBucketId].buckettypes.Count) {
                    return;
                }
                List<string> buckettypes = bucketList[CrtBucketId].buckettypes;

                TypeName = buckettypes[typeIndex - 1];
            }

            this.comboBox1.SelectedItem = TypeName;
            //this.comboBox1.SelectedValue = TypeName;

            Data d = new Data();
            int index = CrtPicPos[CrtBucketId];
            BaseBll bBll = new BaseBll();
            if (HasRemarked(CrtBucketId.ToString(), (index).ToString()))//如果已经标注了，则更新标注
            {
                string strResponse = d.UpdateRemark(CrtBucketId.ToString(), (index).ToString(), this.comboBox1.SelectedItem.ToString());
                if (strResponse == "success")
                {
                    //更新成功    
                    if (this.TimerOC.Checked)
                    {
                        timer1.Interval = 1000;
                        tenthSecond = Convert.ToInt32(this.timerInternal.Text);
                        timer1.Enabled = true;//8888888888888
                        //this.pictureBox3.Visible = false;
                        if (MyBtn != null)
                        {
                            MyBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
                        }
                        else
                        {
                            ForeachPanelControls(TypeName);
                        }
                        
                    }
                    else {
                        GotoNextPicture();
                    }

                    this.comboBox1.SelectedItem = null;

                    //MessageBox.Show("succefully updated annotation!");
                }
                else {
                    MessageBox.Show("Upgrate annotaton error!");
                }
            }
            else
            {
                string strResponse = d.UploadRemark(CrtBucketId.ToString(), (index).ToString(), this.comboBox1.SelectedItem.ToString());

                if (strResponse == "success")
                {
                    if (this.TimerOC.Checked)
                    {
                        timer1.Interval = 1000;
                        tenthSecond = Convert.ToInt32(this.timerInternal.Text);
                        if (MyBtn != null)
                        {
                            MyBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
                        }
                        else
                        {
                            ForeachPanelControls(TypeName);
                        }
                        timer1.Enabled = true;//8888888888888  
                        //this.pictureBox3.Visible = false;

                    }
                    else {
                        GotoNextPicture();
                    }
                    string OriNumInfo = this.lblProgress.Text;
                    string annotatedFnbr = OriNumInfo.Split('/')[0].Trim();
                    string newAnnotatedFileNum = (Convert.ToInt32(annotatedFnbr) + 1).ToString();
                    this.lblProgress.Text = String.Format("{0} / {1}", newAnnotatedFileNum, bucketList[CrtBucketId].totalfilenum);                     
                    this.comboBox1.SelectedItem = null;


                    //MessageBox.Show("succefully upload annotation!");
                }
                else
                {
                    MessageBox.Show("Upload annotaton error!");
                }
            }

        }
        //last 上一张
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ForeachBtnsInToolBar("Last");
            if (bucketList == null) return;
            int index = CrtPicPos[CrtBucketId];
            Data d = new Data();

            Mylocal bBll = new Mylocal();
            int indexLPos = index;

            int zuihou = bBll.GetZuihouFileId(CrtBucketId.ToString());
            //默认第一张图片的fileId,是1
            if (indexLPos != 0 && zuihou == 0)
            {
                MessageBox.Show("No picture file before this one,please check your history record!");
                this.Last.Enabled = false;//----this.Last.Visible = false;
                return;
            }
            else {
                this.Last.Enabled = true;//----this.Last.Visible = true;
            }
            index = bBll.GetLastFileID(CrtBucketId.ToString(), index.ToString());
            if (index == 0)
            {
                if (zuihou != 0)
                {
                    this.Last.Enabled = true;//----this.Last.Visible = true;
                   // MessageBox.Show("Current Picture is the first record! Then Go to the lastest one you recorded!");
                    index = zuihou;
                }
                else
                {
                    this.Last.Enabled = false;//----- this.Last.Visible = false;
                }
                // MessageBox.Show("Current picture has no annotation!");
                // return;
            }//没有上一张

            this.Next.Enabled = true;//---this.Next.Visible = true;     
            CrtPicPos[CrtBucketId] = index;//得到上一张图片fileid
            SetComboAndPic(CrtBucketId + 1,false);
        }
        //next 下一张
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ForeachBtnsInToolBar("Next");
            if (bucketList == null) return;
            int index = CrtPicPos[CrtBucketId];
            Mylocal bBll = new Mylocal();
            int index0 = bBll.GetNextFileID(CrtBucketId.ToString(), index.ToString());
            Data d = new Data();
            int zuihou = bBll.GetZuihouFileId(CrtBucketId.ToString());
            List<PicFile> files = d.GetFilesByThisBucket(bucketList[CrtBucketId].id);
            if (index0 == 0)
            {
                if (files != null)
                {
                 
                    if(zuihou == index){
                         CrtPicPos[CrtBucketId] =  Convert.ToInt32(files[0].fileid);
                    }
                    else{
                        MessageBox.Show("Current picture has no annotation!");
                        this.Next.Enabled = false;//----this.Next.Visible = false;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("This picture is the last one!");
                    this.Next.Enabled = false;//this.Next.Visible = false;
                    return;
                }
              
            }
            else {

                bool hasDownloaded = bBll.ExistFileIdInFile(CrtBucketId.ToString(), index0.ToString());
               if (hasDownloaded)
               {
                   CrtPicPos[CrtBucketId] = index0;
               }
               else {
                   CrtPicPos[CrtBucketId] = Convert.ToInt32(files[0].fileid);
               }
            }
            bool rHoumianNedRemark = false;
            //if (index == index0)
            //{
            //    rHoumianNedRemark = true;
            //}
            //else {
            //    index = index0;
            //    CrtPicPos[CrtBucketId] = index;//得到下一张图片
            //}

            this.Last.Enabled = true;//--- this.Last.Visible = true;
           
            SetComboAndPic(CrtBucketId + 1,rHoumianNedRemark);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LogOn lOn = new LogOn();
            this.Close();
            lOn.Show();
        }
        //
        private void Annotate_Click(object sender, EventArgs e)
        {

            if (this.Annotate.Text == "Annotate")//不能再拖动，移动鼠标时，动作变成画矩形框
            {
                IsDrawRect = true;
                this.Annotate.Text = "EndAnnotate";
                this.Annotate.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            }
            else {//可以拖动，不能再画矩形框
                IsDrawRect = false;
                this.Annotate.Text = "Annotate";
                this.Annotate.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            }
        }

        private enum DireTypes
        {
            /// <summary>
            /// 东北方
            /// </summary>
            North_East,
            /// <summary>
            /// 正东方
            /// </summary>
            East,
            /// <summary>
            /// 东南方
            /// </summary>
            South_East,
            /// <summary>
            ///  正南方
            /// </summary>
            South,
            /// <summary>
            /// 西南方
            /// </summary>
            South_West,
            /// <summary>
            ///正西方 
            /// </summary>
            West,
            /// <summary>
            ///  西北方
            /// </summary>
            North_West,
            /// <summary>
            /// 正北方
            /// </summary>
            North,
            /// <summary>
            /// 不指示方向
            /// </summary>
            Null

        }
        //自动缩进
        private void button3_Click(object sender, EventArgs e)
        {
            if (panel2.Visible)
            {
                this.panel2.Visible = false;
                //int widthOffset = panel2.Width;
                //int OldLocX = pictureBox2.Location.X;
                //pictureBox2.SetBounds(OldLocX - widthOffset + 3, pictureBox2.Location.Y, pictureBox2.Width, pictureBox2.Height);

                //int OldLocX1 = pictureBox1.Location.X;
                //pictureBox1.SetBounds(OldLocX1 - widthOffset + 3, pictureBox1.Location.Y, pictureBox1.Width + widthOffset, pictureBox1.Height);
                this.Suo.Left = 0;

            }
            else {
                this.panel2.Visible = true;
              
                //pictureBox2.SetBounds(panel2.Width, pictureBox2.Location.Y, pictureBox2.Width, pictureBox2.Height);
                //int OldLocX1 = pictureBox2.Width;
                //pictureBox1.SetBounds(OldLocX1, pictureBox1.Location.Y, pictureBox1.Width - OldLocX1, pictureBox1.Height);

                this.Suo.Left = panel2.Right + 1;
            }

        }

        private void CloseWin_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void MinWin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void TitlePanel_Paint(object sender, PaintEventArgs e)
        {
            //ControlPaint.DrawBorder(e.Graphics, panel1.ClientRectangle,
            //         Color.White, 0, ButtonBorderStyle.Solid, //左边
            //         Color.White, 0, ButtonBorderStyle.Solid, //上边
            //         Color.White, 0, ButtonBorderStyle.Solid, //右边
            //         Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42))))), 1, ButtonBorderStyle.Solid);//底边
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel1.ClientRectangle,
                   Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42))))), 1, ButtonBorderStyle.Solid, //左边
                    Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42))))), 1, ButtonBorderStyle.Solid, //上边
                   Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42))))), 1, ButtonBorderStyle.Solid, //右边
                   Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42))))), 1, ButtonBorderStyle.Solid);//底边
        }
        //timer开关
        private void TimerOC_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.timerInternal.Visible)//(this.radioButton1.Checked)
            {
                this.TimerOC.Checked = true;//开关
                this.timerInternal.Visible = true;//时间间隔
                this.label14.Visible = true;//毫秒
                label15.Visible = true;//倒计时

            }
            else
            {
                this.TimerOC.Checked = false;
                this.timerInternal.Visible = false;
                this.label14.Visible = false;
                label15.Visible = false;
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ToolBar.ClientRectangle,
                   Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42))))), 1, ButtonBorderStyle.Solid, //左边
                    Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42))))), 1, ButtonBorderStyle.Solid, //上边
                   Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42))))), 1, ButtonBorderStyle.Solid, //右边
                   Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42))))), 1, ButtonBorderStyle.Solid);//底边
        }
    
   
        //点击放大，or 缩小
        private void Bigger_Click(object sender, EventArgs e)
        {
            ForeachBtnsInToolBar("Bigger");
            double step = 1.2;//缩放倍率

            if (pictureBox1.Height >= Screen.PrimaryScreen.Bounds.Height * 10)
                return;
            pictureBox1.Height = (int)(pictureBox1.Height * step);
            pictureBox1.Width = (int)(pictureBox1.Width * step);

            int px = Cursor.Position.X - pictureBox1.Location.X;
            int py = Cursor.Position.Y - pictureBox1.Location.Y;
            int px_add = (int)(px * (step - 1.0));
            int py_add = (int)(py * (step - 1.0));
            pictureBox1.Location = new Point(pictureBox1.Location.X - px_add, pictureBox1.Location.Y - py_add);
            Application.DoEvents();
           
        }
        //点击 缩小
        private void Smaller_Click(object sender, EventArgs e)
        {
            ForeachBtnsInToolBar("Smaller");
            double step = 1.2;//缩放倍率
            if (pictureBox1.Height <= Screen.PrimaryScreen.Bounds.Height)
                return;
            pictureBox1.Height = (int)(pictureBox1.Height / step);
            pictureBox1.Width = (int)(pictureBox1.Width / step);

            int px = Cursor.Position.X - pictureBox1.Location.X;
            int py = Cursor.Position.Y - pictureBox1.Location.Y;
            int px_add = (int)(px * (1.0 - 1.0 / step));
            int py_add = (int)(py * (1.0 - 1.0 / step));
            pictureBox1.Location = new Point(pictureBox1.Location.X + px_add, pictureBox1.Location.Y + py_add);
            Application.DoEvents();
        }

        private void Updown_Click(object sender, EventArgs e)
        {
            ForeachBtnsInToolBar("Updown");
            pictureBox1.Parent = pan_picture;
            pictureBox1.SetBounds(0, -30, pan_picture.Width, pan_picture.Height);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            isDown = false;
            isDrag = false;
            
            Invalidate();
        }

        private void LeftRight_Click(object sender, EventArgs e)
        {
            ForeachBtnsInToolBar("LeftRight");
            pictureBox1.Parent = pan_picture;
            pictureBox1.SetBounds(0, -30, pan_picture.Width, pan_picture.Height);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            isDown = false;
            isDrag = false;
          
            Invalidate();
        }
        //恢复1:1大小
        private void BackOne_Click(object sender, EventArgs e)
        {
            int width = img_ori.Width;
            int height = img_ori.Height;
            pictureBox1.Parent = pan_picture;
            pictureBox1.SetBounds(0, 0, width, height);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            isDown = false;
            isDrag = false;
           
            Invalidate();
        }
        //平铺
        private void Around_Click(object sender, EventArgs e)
        {
            pictureBox1.Parent = pan_picture;
            pictureBox1.SetBounds(0, 0, pan_picture.Width, pan_picture.Height);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            isDown = false;
            isDrag = false;
           
            Invalidate();
        }

        private void timerInternal_Click(object sender, EventArgs e)
        {
            SetTimer setTimer = new SetTimer(this);

            setTimer.ShowDialog();
         
        }
        #region 子窗口刷新父窗口的值

        private string strLabel1 = "";

        public string StrLabel1
        {
            get
            {
                return strLabel1;
            }
            set
            {
                strLabel1 = value;
                this.timerInternal.Text = strLabel1;
            }
        }
        #endregion

        private void AnnotateTag_Click(object sender, EventArgs e)
        {
            Label MyLab = new Label();
            MyLab = (Label)sender;
            string thisTabText = MyLab.Text;
            if (thisTabText.Trim() == "Type")
            {
                panel5.Visible = false;
                annoteLine.Visible = false;
                panel4.Visible = true;
                TypeTab.ForeColor = Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
                AnnotateTag.ForeColor = Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            }
            else if (thisTabText.Trim() == "Annotate")
            {
                panel5.Visible = true;
                annoteLine.Visible = true;
                panel4.Visible = false;
                TypeTab.ForeColor = Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
                AnnotateTag.ForeColor = Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(186)))), ((int)(((byte)(207)))));
            }
            else
            { //设置快捷键

            }

        }

        //annotateClick,选一种类型并开始标注
        private void AnnotateTypeClick(object sender, EventArgs e)
        {

            int width = img_ori.Width;
            int height = img_ori.Height;
            pictureBox1.Parent = pan_picture;
            pictureBox1.SetBounds(pictureBox1.Location.X, pictureBox1.Location.Y, width, height);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            Button MyBtn = new Button();
            MyBtn = (Button)sender;
            IsDrawRect = true;

            string btnStr = MyBtn.Text;
            CrtAnnotateName = btnStr;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Filter = "文本文件(*.txt)|*.txt";
            if (savefile.ShowDialog() == DialogResult.OK) {
                StreamWriter sw = File.AppendText(savefile.FileName);
                sw.Write("json string tester");
                sw.Flush();
                sw.Close();
            }

        }
    }
}
