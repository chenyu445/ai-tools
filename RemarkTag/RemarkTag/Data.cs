using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Newtonsoft.Json;

using System.IO;

using Qiniu.Util;
using RemarkTag.Qiniu;
using System.Security.Cryptography;
using System.Drawing;
//using System.Text;
//using System.IO;
using Org.BouncyCastle;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Parameters;
using NLog;
namespace RemarkTag
{
    public class Data : BaseBll
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 获取用户的登录状态---判断用户是否启用
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>api/login
        public string GetLogStatus(string User_id, string PassWord)
        {
            try
            {
                enctryKey enKey = new enctryKey();
                string SHA_pswd = enKey.GetSHA256HashFromString(PassWord);

                UserInfo user = new UserInfo();
                user.name = User_id;
                user.passwd = SHA_pswd;//PassWord;
                user.access_token = BaseBll.access_Token;
                user.user_id = "";
                user.status = "";
                user.role_id = "";

                //BaseBll.AttenceUrl
                Mylocal bll = new Mylocal();
                string urlFormat = bll.GetAttenceUrl() + "api/login";
                //urlFormat = string.Format(urlFormat, user.access_token);

                string jsonDataReturn = PostWebRequest(urlFormat, SerializeObject(user));

                ModelReturn modelReturn = (ModelReturn)JsonConvert.DeserializeObject(jsonDataReturn, typeof(ModelReturn), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                if (modelReturn == null)
                {
                    return "";
                }
                TokenObject tObj = (TokenObject)modelReturn.data;
                if (modelReturn.msg == "success")
                {
                    BaseBll.access_Token = tObj.token;
                    BaseBll.CrtUser = user.name;
                    return "success";
                }
                else
                {
                    //LogResult(model.errmsg);
                    return "";
                }
            }
            catch (Exception err)
            {
                logger.Debug("登录时异常。。。"+err.Message);
              
                return "";
            }
        }
        /// <summary>
        /// 得到budgetList
        /// </summary>
        /// <param name="User_id"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>api/listBucket
        //page =1, size =10,
        public ListBucketReturn GetBudgetList(string token)
        {
            try{
                Mylocal bll = new Mylocal();
                string urlFormat = bll.GetAttenceUrl() + "api/listBucket";
            TokenObject token1 = new TokenObject();
            token1.token = BaseBll.access_Token;

            //
            string jsonDataReturn = PostWebRequest(urlFormat, SerializeObject(token1)); //"{\"msg\":\"success\", \"code\": 1, \"data\": {\"buckets\": [{\"domain\": \"www.baidu.com\", \"privatekey\": \"privatekey\", \"id\": 1, \"buckettypes\": [\"type1\", \"type2\", \"type3\", \"type4\"], \"name\": \"bucket\"}, {\"domain\": \"ovy6ljhqz.bkt.clouddn.com\", \"privatekey\": \"-----BEGIN RSA PRIVATE KEY-----\nMIICXQIBAAKBgQCXekKnGDbPXe1x/CmUSXJXjzja9vB+f85/52E+cXV2c6Y93GWs\nrGHML6KeNXxFP5SJzZUtA9RdSndX8N2/h1LLDPlaXbtnusdFQzryWXUfOcQL3Pex\nhy312AYNO6nRTmeYQJVj6oFUlSwbdrh7yTJQhBZaZ2pOqKwlfqKrkVMMGQIDAQAB\nAoGAZy4XcrxSsWO4lrj+FY0F/tCHGNe2L2SdY5BXM/KS4XGbXm3fMARnxW75JS0n\njf/mDQvlPjlqkXVk35kpYwopgSu1CN0YSm6sVmx6VOKiPBdEvh9Xxk7uUG2hOQVt\n87xfTpW8m3PMQ2GgZSUipO/yfnC9wKCApsRXlD2HJQdRqwECQQC8SHi6EwdJSAEs\n4ED1au9mzJzLbS939EBRUfGF6ruLM079mo2GnVatfGExbfdE9OSKy2Y5pnLIxg7F\nd8Qz0RihAkEAzfULuCWOHT1J1CkmOQeBLDud0Rn8JtBsv4qjKIrmGxZbevdMUKKj\ni4JW2hg93MfAINZ2yKCcvHxDnoLCz11oeQJBALtrGGGAl3wmpKTbBU5GB9A4VMta\nACpTg8Ju4w/+vHivti1TLxWUgLdoXAcsAzss2mIeXb99JD0eiY2ezjsWJIECQQDH\nlLLqm9n/k6i7o7Sahx8x0gO44clrg4YehyZc31zk2uGDY6ncaf657nBjoCIylI8m\nQy2QdbPtIy4TqPtHH2lhAkBm5CIUrFgtapKh31EUHIPg+qzqdjeRAy6PX6a+d4wX\ns4OUj2y/EhTOTqXs72r30AFnBuhLoGOvjH8RgcREIgeA\n-----END RSA PRIVATE KEY-----\", \"id\": 2, \"buckettypes\": [\"normal\", \"shenjingshangpituoli\", \"sesushangpicengtuoli\", \"shiwangmoqianmo\", \"others\"], \"name\": \"eyesoct\"}]}}";

            ListBucketReturn modelReturn = (ListBucketReturn)JsonConvert.DeserializeObject(jsonDataReturn, typeof(ListBucketReturn), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            if (modelReturn.data == null) {
                return null;
            }
            List<bucketObj> buckets = modelReturn.data.buckets;
           
            if (modelReturn.msg == "success")
            {
                return modelReturn;
            }
            else
            {
                //LogResult(model.errmsg);
                return null;
            }
            }
            catch (Exception err)
            {
                logger.Debug("获取bucket列表时异常。。。" + err.Message);
                return null;
            }
        }
        /// <summary>
        /// 获取用户的登录状态---判断用户是否启用
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>api/login
        public string GetPointFile(string Name)
        {
            try
            {
                PointFile user = new PointFile();
                user.name = Name;
                user.token = BaseBll.access_Token;

                Mylocal bll = new Mylocal();
                string urlFormat = bll.GetAttenceUrl() + "api/getBucketFile";

                string jsonDataReturn = PostWebRequest(urlFormat, SerializeObject(user));
                PointFileReturn modelReturn = (PointFileReturn)JsonConvert.DeserializeObject(jsonDataReturn, typeof(PointFileReturn), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                if (modelReturn.msg == "success")
                {
                    string address = modelReturn.data.url;
                    return address;
                }
                else
                {
                    //LogResult(model.errmsg);
                    return "";
                }
            }
             
            catch (Exception err)
            {
                logger.Debug("得到密钥文件路径时异常。。。" + err.Message);
                return "";
            }
          

        }
        //
        public List<PicFile> GetFilesByThisBucket(string BucketId)
        {
            try
            {
                BucketFile bFile = new BucketFile();
                bFile.bucketid = BucketId;
                bFile.count = "2";
                bFile.token = BaseBll.access_Token;

                Mylocal bll = new Mylocal();
                string urlFormat = bll.GetAttenceUrl() + "api/ownFile";
                //urlFormat = string.Format(urlFormat, user.access_token);

                string jsonDataReturn = PostWebRequest(urlFormat, SerializeObject(bFile));

                BucketFileReturn modelReturn = (BucketFileReturn)JsonConvert.DeserializeObject(jsonDataReturn, typeof(BucketFileReturn), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });


                if (modelReturn.msg == "success")
                {
                    List<PicFile> PicFiles = modelReturn.data.imgs;
                    return PicFiles;
                }
                else if (modelReturn.msg.IndexOf("empty") != -1)
                {//empty files
                    return null;
                }
                else
                {
                    //LogResult(model.errmsg);
                    return null;
                }
            }
            catch (Exception err)
            {
                logger.Debug("得到bucket图片文件时异常。。。" + err.Message);
                return null;
            }
           
        }
        //上传标注
        public string UploadRemark(string bucketId, string FileId, string MarkInfo) {
            try
            {
                uploadFile upFile = new uploadFile();
                upFile.fileid = FileId;
                upFile.filetypename = MarkInfo;
                upFile.token = BaseBll.access_Token;
                upFile.annotation = "";

                Mylocal bll = new Mylocal();
                string urlFormat = bll.GetAttenceUrl() + "api/uploadAnnotation";
                string jsonDataReturn = PostWebRequest(urlFormat, SerializeObject(upFile));
                uploadReturn modelReturn = (uploadReturn)JsonConvert.DeserializeObject(jsonDataReturn, typeof(uploadReturn), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                if (modelReturn.msg == "success")
                {
                    Mylocal bBll = new Mylocal();
                    bBll.LogHistory( bucketId, FileId, MarkInfo);
                    return "success";
                }
                else
                {
                    //LogResult(model.errmsg);
                    return "sql error";
                }
            }
            catch (Exception err)
            {
                logger.Debug("上传标注时异常。。。" + err.Message);
                return "";
            }

        }
        //上传标注
        public string UpdateRemark(string bucketId, string FileId, string MarkInfo)
        {
            try
            {
                uploadFile upFile = new uploadFile();
                upFile.fileid = FileId;
                upFile.filetypename = MarkInfo;
                upFile.token = BaseBll.access_Token;
                upFile.annotation = "";

                Mylocal bll = new Mylocal();
                string urlFormat = bll.GetAttenceUrl() + "api/updateAnnotation";
                string jsonDataReturn = PostWebRequest(urlFormat, SerializeObject(upFile));
                uploadReturn modelReturn = (uploadReturn)JsonConvert.DeserializeObject(jsonDataReturn, typeof(uploadReturn), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                if (modelReturn.msg == "success")
                {
                    Mylocal bBll = new Mylocal();
                    bBll.UpdateTypeLogHistory(bucketId, FileId, MarkInfo);
                    return "success";
                }
                else
                {
                    logger.Debug("更新标注时不成功！！");
                    return "";
                }
            }
            catch (Exception err)
            {
                logger.Debug("更新标注时异常。。。" + err.Message);
                return "";
            }

        }

        //
        public static void PEMConvertToXML()//PEM格式密钥转XML
        {
            AsymmetricCipherKeyPair keyPair;
            using (var sr = new StreamReader("e:\\PrivateKey.pem"))
            {
                var pemReader = new Org.BouncyCastle.OpenSsl.PemReader(sr);
                keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();
            }
            var key = (RsaPrivateCrtKeyParameters)keyPair.Private;
            var p = new RSAParameters
            {
                Modulus = key.Modulus.ToByteArrayUnsigned(),
                Exponent = key.PublicExponent.ToByteArrayUnsigned(),
                D = key.Exponent.ToByteArrayUnsigned(),
                P = key.P.ToByteArrayUnsigned(),
                Q = key.Q.ToByteArrayUnsigned(),
                DP = key.DP.ToByteArrayUnsigned(),
                DQ = key.DQ.ToByteArrayUnsigned(),
                InverseQ = key.QInv.ToByteArrayUnsigned(),
            };
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(p);
            using (var sw = new StreamWriter("e:\\PrivateKey.xml"))
            {
                sw.Write(rsa.ToXmlString(true));
            }
        }

        ///得到补位之前文件
        ///因为服务器端加密文件之前对文件进行了16位倍数的补位，故而去掉
        public byte[] GetZeroNumber(byte[] reFile,int picLen)
        {
            try
            {
                FileAndByte fb = new FileAndByte();
                byte[] fileBytes = reFile;

                if (fileBytes.Length > picLen)
                {
                    byte[] tryBytes = new byte[picLen];
                    Array.Copy(fileBytes, tryBytes, picLen);
                    return tryBytes;
                }
                else {
                    return fileBytes;
                }
            }
            catch (Exception er){
                logger.Debug("获取MD5之前，得到图片原本16进制长度时异常" + er.Message);
                return reFile;
               
            }
            #region ori
            /*
            int i = 1;
            try
            {
                while (i <= 16)
                {
                    byte[] tryBytes = new byte[fileBytes.Length - i];
                    Array.Copy(fileBytes, tryBytes, fileBytes.Length - i);
                    string filePath = "temp" + i.ToString() + ".jpg";

                    fb.Bytes2File(tryBytes, filePath);
                    Bitmap img = new Bitmap(filePath);
                    img.Dispose();
                    i++;
                    //File.Delete(filePath);
                }
                int m = 0;
                while (m <= i)
                {
                    File.Delete("temp" + m.ToString() + ".jpg");
                    m++;
                }
                int result = i-1;
                byte[] reBytes = new byte[fileBytes.Length - result];
                Array.Copy(fileBytes, reBytes, fileBytes.Length - result);
                return reBytes;
            }
            catch (Exception er)
            {
                int m = 0;
                while (m <= i)
                {
                    File.Delete("temp" + m.ToString() + ".jpg");

                    m++;
                }
                int result = i - 1;
                byte[] tryBytes = new byte[fileBytes.Length - result];
                Array.Copy(fileBytes, tryBytes, fileBytes.Length - result);
                return tryBytes;
            }
             * */
            #endregion
        }

        //获得解密后的图片文件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucketid">bucket对应的ID</param>
        /// <param name="SaveFile">要显示的文件</param>
        /// <param name="BucketKey">bucket对应的privatekey</param>
        /// <param name="domain">bucket对应的domain</param>
        /// <returns></returns>
        public string GetOriPicFile(string bucketid,PicFile SaveFile,string BucketKey,string domain)
        {
               string localFile = "";

               int BucketID = Convert.ToInt32(bucketid);
            string mi = bucketid + "_" + SaveFile.fileid;
             string encryptFile = @"Pic/" + mi;//被加密的文件

             localFile = encryptFile + ".jpg";
             if (File.Exists(localFile))
             {
                 //文件已存在不需要下载
                 return localFile;
             }
             if (SaveFile.qiniukey == null) { //说明没有
                 return "Nofiles";
             }
             //SaveFile.url为空的这种情况默认是已经下载到本地的图片了
            Updown up = new Updown();
            string PrivateUrl = SaveFile.url; //up.DownLoadPic(domain, SaveFile.qiniukey);//获取被下载文件的链接地址

            if(File.Exists(encryptFile)){
                File.Delete(encryptFile);
            }

             up.DownLoad(PrivateUrl,encryptFile);
            string encryptkey = SaveFile.encryptkey;
           
             enctryKey enKey = new enctryKey();
             string MYkey = enKey.RSADecrypt( BucketKey,encryptkey);
            
           // string MYkey = enKey.RSADecrypt("","");

            int len = MYkey.Length;//32位
           if (len != 32) {
               return "Failed when decode the file.";
           }
              
           string Key = MYkey.Substring(0, 16);
           string IV = MYkey.Substring(16, 16);


           FileAndByte fb = new FileAndByte();
           byte[] fileBytes = fb.File2Bytes(encryptFile);
           byte[] reFile = enKey.AESDecrypt(fileBytes, Key, IV);
           try
           {
               localFile = encryptFile + ".jpg";
               if (File.Exists(localFile))
               {
                   File.Delete(localFile);
               }
               int PicutureLength = Convert.ToInt32( SaveFile.pictureLength);
               byte[] tryBytes = GetZeroNumber(reFile,PicutureLength);//--需要测试
               fb.Bytes2File(tryBytes, localFile);
           }
            catch(Exception er){

                logger.Debug("解密文件得到新文件，因文件正在被占用，无法删除！详细信息为：" + er.Message + er.Source);
            }

            //把文件读成二进制流，然后解密成一个新的二进制流，另存为一个png文件
           
           string Md5Str = enKey.GetMD5HashFromFile(localFile); 
           if (SaveFile.qiniukey.Trim() == Md5Str.Trim())
           {
               //说明服务器上文件没有换
               return localFile;
           }
           else { 
               //说明服务器上的文件换了

               return "Failed to download this picture!";
           }
           
        }

    }
}
