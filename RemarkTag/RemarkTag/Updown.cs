using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;

using Qiniu.Storage;
using Qiniu.Util;
using NLog;
namespace RemarkTag
{

    public class Updown
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //public string DownLoadPic(string domain,string fileName) {
        //    string access = BaseBll.AccessKey;
        //    string secret = BaseBll.SecretKey;
        //    Mac mac = new Mac(access, secret);
           
        //    string key = fileName;

        //    DownloadManager dMan = new DownloadManager();
        //    string privateUrl = dMan.CreatePrivateUrl(mac, domain, key);

        //    return privateUrl;
        ////Console.WriteLine(privateUrl);

        //}
        //下载文件至本地
        public void DownLoad(string url, string saveFilePath)
        {

            WebClient client = new WebClient();
            client.DownloadFile(url, saveFilePath);
            //return fileName; 
            return;
         

        }

        public string GetFileContent(string Add) {

            try
            {
                string temp = @"my";
                WebClient client = new WebClient();
                client.DownloadFile(Add, temp);

                enctryKey enKey = new enctryKey();
                string Md5Str = enKey.GetMD5HashFromFile(temp);
             
                string contents = System.IO.File.ReadAllText(temp);

                if (File.Exists(temp))
                {
                    File.Delete(temp);
                }
                return contents;
            }
            catch(Exception err){
               logger.Debug("获取bucket的密钥时异常。。。"+ err.Message);

                return "";
            }
        }
    }
}
