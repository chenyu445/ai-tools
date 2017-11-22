using System;
using System.IO;
using System.Text;
using Qiniu.Util;
using Qiniu.Http;

using System.Web;
using System.Net;
using System.Collections.Generic;
namespace Qiniu.Storage
{
    /// <summary>
    /// 空间文件下载，只提供简单下载逻辑
    /// 对于大文件下载、断点续下载等需求，可以根据实际情况自行实现
    /// </summary>
    public class DownloadManager
    {
        /// <summary>
        /// 生成授权的下载链接(访问私有空间中的文件时需要使用这种链接)
        /// </summary>
        /// <param name="mac">账号(密钥)</param>
        /// <param name="domain">(私有)空间文件的下载域名</param>
        /// <param name="fileName">（私有）空间文件名</param>
        /// <param name="expireInSeconds">从生成此链接的时刻算起，该链接有效时间(单位:秒)</param>
        /// <returns>已授权的下载链接</returns>
        public string CreatePrivateUrl(Mac mac, string domain, string fileName, int expireInSeconds = 3600)
        {
            long deadline = UnixTimestamp.GetUnixTimestamp(expireInSeconds);

            string publicUrl = CreatePublishUrl(domain, fileName);
            StringBuilder sb = new StringBuilder(publicUrl);
            if (publicUrl.Contains("?"))
            {
                sb.AppendFormat("&e={0}", deadline);
            }
            else
            {
                sb.AppendFormat("?e={0}", deadline);
            }
            
            string token = Auth.CreateDownloadToken(mac, sb.ToString());
            sb.AppendFormat("&token={0}", token);

            return sb.ToString();
        }

        /// <summary>
        /// 生成公开空间的下载链接
        /// </summary>
        /// <param name="domain">公开空间的文件下载域名</param>
        /// <param name="fileName">公开空间文件名</param>
        /// <returns>公开空间文件下载链接</returns>
        public static string CreatePublishUrl(string domain, string fileName)
        {
            return string.Format("http://{0}/{1}", domain, Uri.EscapeUriString(fileName));
        }
        /// <summary>
        /// HTTP-GET方法
        /// </summary>
        /// <param name="url">请求目标URL</param>
        /// <param name="token">令牌(凭证)[可选->设置为null]</param>
        /// <param name="binaryMode">是否以二进制模式读取响应内容(默认:否，即表示以文本方式读取)</param>
        /// <returns>HTTP-GET的响应结果</returns>
        public static HttpResult Get(string url, string token, bool binaryMode = false)
        {
            HttpResult result = new HttpResult();

            HttpWebRequest wReq = null;
            string userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; SV1; .NET CLR 2.0.1124)";
            try
            {
                wReq = WebRequest.Create(url) as HttpWebRequest;
                wReq.Method = "GET";
                if (!string.IsNullOrEmpty(token))
                {
                    wReq.Headers.Add("Authorization", token);
                }

                wReq.UserAgent = userAgent;
                wReq.AllowAutoRedirect = true;
                wReq.ServicePoint.Expect100Continue = false;

                HttpWebResponse wResp = wReq.GetResponse() as HttpWebResponse;

                if (wResp != null)
                {
                    result.Code = (int)wResp.StatusCode;
                    result.RefCode = (int)wResp.StatusCode;

                    getHeaders(ref result, wResp);

                    if (binaryMode)
                    {
                        int len = (int)wResp.ContentLength;
                        result.Data = new byte[len];
                        int bytesLeft = len;
                        int bytesRead = 0;

                        using (BinaryReader br = new BinaryReader(wResp.GetResponseStream()))
                        {
                            while (bytesLeft > 0)
                            {
                                bytesRead = br.Read(result.Data, len - bytesLeft, bytesLeft);
                                bytesLeft -= bytesRead;
                            }
                        }
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(wResp.GetResponseStream()))
                        {
                            result.Text = sr.ReadToEnd();
                        }
                    }

                    wResp.Close();
                }
            }
            catch (WebException wex)
            {
                HttpWebResponse xResp = wex.Response as HttpWebResponse;
                if (xResp != null)
                {
                    result.Code = (int)xResp.StatusCode;
                    result.RefCode = (int)xResp.StatusCode;

                    getHeaders(ref result, xResp);

                    using (StreamReader sr = new StreamReader(xResp.GetResponseStream()))
                    {
                        result.Text = sr.ReadToEnd();
                    }

                    xResp.Close();
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [{1}] [HTTP-GET] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), userAgent);
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCode.USER_UNDEF;
                result.RefText += sb.ToString();
            }
            finally
            {
                if (wReq != null)
                {
                    wReq.Abort();
                }
            }

            return result;
        }


        /// <summary>
        /// 下载文件到本地
        /// </summary>
        /// <param name="url">(可访问的或者已授权的)链接</param>
        /// <param name="saveasFile">(另存为)本地文件名</param>
        /// <returns>下载资源的结果</returns>
        public static HttpResult Download(string url, string saveasFile)
        {
            HttpResult result = new HttpResult();

            try
            {
                result =  Get(url, "" , true);
                if (result.Code == (int)HttpCode.OK)
                {
                    using (FileStream fs = File.Create(saveasFile, result.Data.Length))
                    {
                        fs.Write(result.Data, 0, result.Data.Length);
                        fs.Flush();
                    }
                    result.RefText += string.Format("[{0}] [Download] Success: (Remote file) ==> \"{1}\"\n",
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), saveasFile);
                }
                else
                {
                    result.RefText += string.Format("[{0}] [Download] Error: code = {1}\n",
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), result.Code);
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [Download] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCode.USER_UNDEF;
                result.RefText += sb.ToString();
            }

            return result;
        }

     
        /// <summary>
        /// 获取返回信息头
        /// </summary>
        /// <param name="hr">即将被HTTP请求封装函数返回的HttpResult变量</param>
        /// <param name="resp">正在被读取的HTTP响应</param>
        private static void getHeaders(ref HttpResult hr, HttpWebResponse resp)
        {
            if (resp != null)
            {
                if (hr.RefInfo == null)
                {
                    hr.RefInfo = new Dictionary<string, string>();
                }

                hr.RefInfo.Add("ProtocolVersion", resp.ProtocolVersion.ToString());

                if (!string.IsNullOrEmpty(resp.CharacterSet))
                {
                    hr.RefInfo.Add("Characterset", resp.CharacterSet);
                }

                if (!string.IsNullOrEmpty(resp.ContentEncoding))
                {
                    hr.RefInfo.Add("ContentEncoding", resp.ContentEncoding);
                }

                if (!string.IsNullOrEmpty(resp.ContentType))
                {
                    hr.RefInfo.Add("ContentType", resp.ContentType);
                }

                hr.RefInfo.Add("ContentLength", resp.ContentLength.ToString());

                var headers = resp.Headers;
                if (headers != null && headers.Count > 0)
                {
                    if (hr.RefInfo == null)
                    {
                        hr.RefInfo = new Dictionary<string, string>();
                    }
                    foreach (var key in headers.AllKeys)
                    {
                        hr.RefInfo.Add(key, headers[key]);
                    }
                }
            }
        }


      

    }
}
