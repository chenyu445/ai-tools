using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;

using System.Drawing;
using System.Drawing.Drawing2D;
//using System.Configuration;
//using WCE.Utilities;
//using WCE.Model;
using NLog;
namespace RemarkTag
{
    public class BaseBll
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static public  string AttenceUrl = "";

        public int TimerSet = 2;

        static public List<string> ImPotntPt = new List<string>();//对数组bucketList大小一致
        public static string  access_Token = "xxxxxxx";

        public static string CrtUser = "";

        /// <summary>
        /// 序列化对象成json数据格式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public void SetCrtUrl(string crtUrl) {
            AttenceUrl = crtUrl;
        }

       
        /// <summary>
        /// Post数据接口
        /// </summary>
        /// <param name="postUrl">接口地址</param>
        /// <param name="paramData">提交json数据</param>
        /// <returns></returns>
        public string PostWebRequest(string postUrl, string paramData)
        {
            string returnValue = string.Empty;
            try
            {
                byte[] byteData = Encoding.UTF8.GetBytes(paramData);
                Uri uri = new Uri(postUrl);
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(uri);
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.ContentLength = byteData.Length;
                //定义Stream信息
                Stream stream = webReq.GetRequestStream();
                stream.Write(byteData, 0, byteData.Length);
                stream.Close();
                //获取返回信息
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                returnValue = streamReader.ReadToEnd();
                //关闭信息
                streamReader.Close();
                response.Close(); 
                stream.Close();
            }
            catch (Exception ex)
            {
                logger.Debug("post请求异常" + ex.Message);
                 
                
            }
            return returnValue;
        }

      

        /// <summary>
        /// Get数据接口
        /// </summary>
        /// <param name="postUrl">接口地址</param>
        /// <param name="paramData">提交json数据</param>
        /// <returns></returns>
        public string GetWebRequest(string postUrl)
        {
            string returnValue = string.Empty;
            try
            {

                Uri uri = new Uri(postUrl);
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(uri);
                webReq.Method = "POST";
                using (Stream stream = webReq.GetResponse().GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    returnValue = reader.ReadToEnd();
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                logger.Debug(ex.ToString());
            }
            return returnValue;
        }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="reqUrl"></param>
  /// <param name="method"></param>
  /// <param name="paramData"></param>
  /// <returns></returns>
        public string ReqUrl(string reqUrl, string method, string paramData)
        {
            try
            {
                string token = access_Token;
                reqUrl = AttenceUrl + reqUrl;

                HttpWebRequest request = WebRequest.Create(reqUrl) as HttpWebRequest;
                request.Method = method.ToUpperInvariant();

                if (!string.IsNullOrEmpty(token) && token.Length > 1) { request.Headers.Add("token", token); }
                if (request.Method.ToString() != "GET" && !string.IsNullOrEmpty(paramData) && paramData.Length > 0)
                {
                    request.ContentType = "application/json";
                    byte[] buffer = Encoding.UTF8.GetBytes(paramData);
                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                }

                using (HttpWebResponse resp = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader stream = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
                    {
                        string result = stream.ReadToEnd();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public Bitmap GetThumbnail(Bitmap b, int destHeight, int destWidth)
        {
            System.Drawing.Image imgSource = b;
            System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            // 按比例缩放           
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;
            if (sHeight > destHeight || sWidth > destWidth)
            {
                if ((sWidth * destHeight) > (sHeight * destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth * sHeight) / sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth * destHeight) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }
            Bitmap outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);
            // 设置画布的描绘质量         
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
            //// 以下代码为保存图片时，设置压缩质量     
            //EncoderParameters encoderParams = new EncoderParameters();
            //long[] quality = new long[1];
            //quality[0] = 100;
            //EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            //encoderParams.Param[0] = encoderParam;
            imgSource.Dispose();
            return outBmp;
        }
        //将数字转换成对应的字母
        public string getCharactor(int oriNumber) {
            byte[] array = new byte[1];
            array[0] = (byte)(Convert.ToInt32(oriNumber)); //ASCII码强制转换二进制
            string a = Convert.ToString(System.Text.Encoding.ASCII.GetString(array));

            return a;
        }

    }
}
