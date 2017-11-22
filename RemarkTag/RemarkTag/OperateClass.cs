using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace RemarkTag
{
    public class OperateClass
    {

        public static void SetSuitPictureBox(PictureBox picBox, Panel pan_picture)
        {
            picBox.Dock = DockStyle.Fill;
            picBox.SizeMode = PictureBoxSizeMode.Zoom;
            int x, y, height, width, vmax, hmax;
            x = pan_picture.HorizontalScroll.LargeChange;
            y = pan_picture.VerticalScroll.LargeChange;
            height =pan_picture.Height;
            width = pan_picture.Width;
            vmax = pan_picture.HorizontalScroll.Maximum;
            hmax = pan_picture.VerticalScroll.Maximum;
            try
            {

                Image image = Image.FromFile(picBox.ImageLocation);
                if (image.Height <= height && image.Width <= width)
                {
                    picBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    picBox.Dock = DockStyle.Fill;
                }
                else
                {
                    picBox.SizeMode = PictureBoxSizeMode.AutoSize;
                    picBox.Dock = DockStyle.None;
                    picBox.Location = new Point(1, 0);
                    //MessageBox.Show("v--"+vmax+"-"+x+" h-- "+hmax+"-"+y);
                    pan_picture.AutoScrollPosition = new Point((vmax - x) / 2, (hmax - y) / 2);//设置滚动条位置为中间位置
                }
            }
            catch (Exception)
            {

            }

        }

        public static void resetPic(PictureBox picBox)
        {
            //int OldWidth = picBox.Width;
            //picBox.Dock = DockStyle.None;
            //picBox.SizeMode = PictureBoxSizeMode.AutoSize;
            //int NewWidth = picBox.Width;
            //picBox.Left = picBox.Left - (NewWidth - OldWidth) / 2;
        }

        /// <summary>
        /// 放大
        /// </summary>
        /// <param name="picBox"></param>
        public static void max(PictureBox picBox)
        {
            int w = picBox.Image.Width;
            int h = picBox.Image.Height;
            double div = Convert.ToDouble(h) / Convert.ToDouble(w);
                        
            w = w + 30;
            h = Convert.ToInt32(w * div);

            picBox.Left -= 15 + Cursor.Position.X-(picBox.Left+picBox.Width/2);
            picBox.Top -= (h - picBox.Image.Height) / 2;
            Bitmap NewBitmap = new Bitmap(picBox.Image, w, h);
            picBox.Image = NewBitmap;
            NewBitmap.Dispose();
            //zoom_image(true,picBox,w,h);
        }

        /// <summary>
        /// 缩小
        /// </summary>
        /// <param name="picBox"></param>
        public static void min(PictureBox picBox)
        {
            int w = picBox.Image.Width;
            int h = picBox.Image.Height;
            double div = Convert.ToDouble(h) / Convert.ToDouble(w);

            if (w > 30 && (w - 30) * div > 1)
            {
                w = w - 30;
                h = Convert.ToInt32(w * div);
                picBox.Left += 15;
                picBox.Top -= (h - picBox.Image.Height) / 2;
            }
            Bitmap NewBitmap = new Bitmap(picBox.Image, w, h);
            picBox.Image = NewBitmap;
            NewBitmap.Dispose();
            //zoom_image(true, picBox, w, h);
        }


        /// <summary>
        /// 放大缩小
        /// </summary>
        /// <param name="picBox"></param>
        /// <param name="img">原始图片</param>
        /// <param name="zoomtime">放大缩小次数，负为缩小，正为放大</param>
        /// <returns></returns>
        public static void maxMin(PictureBox picBox,Image img,int zoomtime,int Tag,int Index)
        {
            int w = img.Width;
            int h = img.Height;

            int OldWidth = w;
           
            Bitmap NewBitmap = new Bitmap(img);
            try
            {                
                double div = Convert.ToDouble(h) / Convert.ToDouble(w);

                if (w > 30 && (w - 30) * div > 1)
                {
                    w = w + 30 * zoomtime;
                    h = Convert.ToInt32(w * div);

                    NewBitmap = new Bitmap(img, w, h);
                    
                }

                //if (w < img.Width && h < img.Height)
                //{
                //    picBox.Location =new Point(0,0);
                // }

                picBox.Image = NewBitmap;


                //int NewWidth = NewBitmap.Width;

                //if (NewWidth >= 945)
                //{
                //    picBox.Left = picBox.Left - 15;
                //    if (picBox.Left <= 0) {
                //        picBox.Left = 200;
                //    }
                //}
                //if (NewWidth < 945)
                //{
                //    picBox.Left = picBox.Location.X + 15;
                //    if (picBox.Right >= 1000) {
                //        picBox.Left = 400;
                //    }
                //}
                picBox.Top = 0;

         
                GC.Collect();
                
            }
            catch { }
            
        }


        /// <summary>
        /// 小于容器大小就不缩放
        /// </summary>
        /// <param name="picBox"></param>
        /// <param name="pan_picture"></param>
        /// <returns></returns>
        public static bool IsMaxMin(PictureBox picBox,Panel pan_picture)
        {
            bool yn = true;

            int w = picBox.Image.Width;
            int h = picBox.Image.Height;

            if (w < pan_picture.Width && h < pan_picture.Height)
            {
                SetSuitPictureBox(picBox, pan_picture);
                yn = false;
            }
            else
            {
                resetPic(picBox);
                yn = true;
            }

            return yn;
        }
        public static bool IsMin(PictureBox picBox, int with)
        {
            bool yn = true;

            int w = picBox.Image.Width;

            if (w < with)
            {
                yn = false;
            }
            else
            {
                yn = true;
            }

            return yn;
        }
        /// <summary>
        /// 绘制新Img
        /// </summary>
        /// <param name="chec"></param>
        /// <param name="pictureBox1"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        private static void zoom_image(bool chec, PictureBox pictureBox1,int w,int h)
        {
            int begin_x=pictureBox1.Location.X;        //图片开始位置x
            int begin_y=pictureBox1.Location.Y;        //图片开始位置y

            Image image_ori = pictureBox1.Image;

            //int w, h;                      //缩放后的图片大小            
            if (chec)
            {
                if (begin_x + pictureBox1.Width > w) begin_x = w - pictureBox1.Width;
                if (begin_y + pictureBox1.Height > h) begin_y = h - pictureBox1.Height;
                if (begin_x < 0) begin_x = 0;
                if (begin_y < 0) begin_y = 0;
            }
            Bitmap resizedBmp = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(resizedBmp);
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.DrawImage(image_ori, new Rectangle(0, 0, w, h), new Rectangle(0, 0, image_ori.Width, image_ori.Height), GraphicsUnit.Pixel);
            int ww, hh;
            ww = w;
            hh = h;
            if (pictureBox1.Width < ww) ww = pictureBox1.Width;
            if (pictureBox1.Height < hh) hh = pictureBox1.Height;
            try
            {
                pictureBox1.Image = resizedBmp;   //在图片框上显示区域图片
            }
            catch
            {
            }
            g.Dispose();
        }
    }
}
