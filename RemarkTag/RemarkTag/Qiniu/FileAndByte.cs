﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace RemarkTag.Qiniu
{
     class FileAndByte
    {
        
            /// <summary>
            /// 将文件转换为byte数组
            /// </summary>
            /// <param name="path">文件地址</param>
            /// <returns>转换后的byte数组</returns>
            public byte[] File2Bytes(string path)
            {
                if (!System.IO.File.Exists(path))
                {
                    return new byte[0];
                }

                FileInfo fi = new FileInfo(path);
                byte[] buff = new byte[fi.Length];

                FileStream fs = fi.OpenRead();
                fs.Read(buff, 0, Convert.ToInt32(fs.Length));
                fs.Close();

                return buff;
            }
  
            /// <summary>
            /// 将byte数组转换为文件并保存到指定地址
            /// </summary>
            /// <param name="buff">byte数组</param>
            /// <param name="savepath">保存地址</param>
            public  void Bytes2File(byte[] buff, string savepath)
            {
                if (System.IO.File.Exists(savepath))
                {
                    System.IO.File.Delete(savepath);
                }
                System.IO.File.WriteAllBytes(savepath, buff);
                //FileStream fs = new FileStream(savepath, FileMode.CreateNew);
                //BinaryWriter bw = new BinaryWriter(fs);
                //bw.Write(buff, 0, buff.Length);
                //bw.Close();
                //fs.Close();
            }
        
    }
}