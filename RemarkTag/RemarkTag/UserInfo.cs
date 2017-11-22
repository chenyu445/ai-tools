using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemarkTag
{
    public class UserInfo
    {
        public string user_id { set; get; }
        public string name { set; get; }
        public string passwd { set; get; }
        public string role_id { set; get; }
        public string status { set; get; }
        public string access_token { set; get; }
       // public string order { set; get; }
       // public string id { set; get; }
       
    }
    public class bucketObj {
        public string id { set; get; }
        public string name { set; get; }
        public string privatekey { set; get; }
        public string status { set; get; }
        public List<string> buckettypes { set; get; }  //根据此值判断生成几个radio
        public string domain { set; get; }

        public string annotatedfilenum { set; get; }
        public string totalfilenum { set; get; }
      
    }

    public class ModelReturn
    {
        public string code { set; get; }
        public string msg { set; get; }
        public TokenObject data { set; get; }
    }

    public class TokenObject {
        public string token { set; get; }
    }

    public class ListBucketReturn
    {
        public string code { set; get; }
        public string msg { set; get; }
        public BucketReturnObj data { set; get; }
    }

    public class BucketReturnObj {
        public List<bucketObj> buckets { set; get; }
    }

    public class BucketFile {
        public string bucketid { set; get; }
        public string count { set; get; }
        public string token { set; get; }
    }

    public class BucketFileReturn {
        public string code { set; get; }
        public string msg { set; get; }
        public PicFileObj data { set; get; }
       
    }

    public class PicFileObj {
        public List<PicFile> imgs { set; get; }
    }

    public class PicFile{
        public string fileid { set; get; }
        public string qiniukey { set; get; }//md5的str串,需要与解密后的文件MD5做对比
        public string encryptkey { set; get; }
        public string bucketid { set; get; }
        public string url { set; get; }
        public string pictureLength { set; get; }
    }

    public class uploadFile {
        public string fileid { set; get; }
        public string filetypename { set; get; }
        public string annotation { set; get; }
        public string token { set; get; }
    }

    public class uploadReturn {
        public string code { set; get; }
        public string msg { set; get; }
        public string data { set; get; }
    }

    public class PointFile {
        public string name { set; get; }
        public string token { set; get; }
    }
    public class PointFileReturn {
        public string code { set; get; }
        public string msg { set; get; }
        public ImptntFile data { set; get; }
    }
    public class ImptntFile {
        public string url { set; get; }
    }
}
