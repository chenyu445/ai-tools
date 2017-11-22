using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Configuration;
using NLog;

namespace RemarkTag
{
    public class Mylocal
    {
        string conString = string.Format(@"Data Source={0}; Pooling=false; FailIfMissing=false;", System.Windows.Forms.Application.StartupPath + @"\test.db");
        private static Logger logger = LogManager.GetCurrentClassLogger();
        #region Sqlite
        public bool IsExistedTypeTable()
        {
            bool rResult = false;
            try
            {
                using (var dbConn = new System.Data.SQLite.SQLiteConnection(conString))
                {
                    dbConn.Open();
                    using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                    {
                        //create table
                        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS TypeRecords ( BucketID INTEGER NOT NULL, FileID INTEGER NOT NULL, TypeName TEXT, UserName TEXT,ID	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE );";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    if (dbConn.State != System.Data.ConnectionState.Closed) dbConn.Close();
                    dbConn.Dispose();
                   

                    rResult = true;
                }
            }
            catch
            {
                logger.Debug("创建操作记录表格时异常");
            }
            return rResult;
        }
        //写入数据
        public void LogHistory(string bucketId, string fileId, string remarkName)
        {
            if (IsExistedTypeTable())
            {
                try
                {
                    using (var dbConn = new System.Data.SQLite.SQLiteConnection(conString))
                    {
                        dbConn.Open();
                        using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                        {
                            cmd.CommandText = @"INSERT INTO TypeRecords (BucketID,FileID,TypeName,UserName) VALUES(@bucketid,@fileid,@typename,@username)";

                            var p1 = cmd.CreateParameter();
                            p1.ParameterName = "@bucketid";
                            p1.Value = bucketId;

                            var p2 = cmd.CreateParameter();
                            p2.ParameterName = "@fileid";
                            p2.Value = fileId;

                            //typename, username
                            var p3 = cmd.CreateParameter();
                            p3.ParameterName = "@typename";
                            p3.Value = remarkName;
                            var p4 = cmd.CreateParameter();
                            p4.ParameterName = "@username";
                            p4.Value = BaseBll.CrtUser;
                            cmd.Parameters.Add(p1);
                            cmd.Parameters.Add(p2);
                            cmd.Parameters.Add(p3);
                            cmd.Parameters.Add(p4);

                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }
                        if (dbConn.State != System.Data.ConnectionState.Closed) dbConn.Close();
                        dbConn.Dispose();
                       

                    }
                }
                catch
                {
                    logger.Debug("写入操作记录时异常");
                }
            }
        }//LogHistory
        public string GetMarkNameBy(string BucketId, string FileId)//原逻辑的GetMarkBy接口
        {
            try
            {
                using (var dbConn = new System.Data.SQLite.SQLiteConnection(conString))
                {
                    dbConn.Open();
                    using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                    {
                        //cmd.CommandText = @"INSERT INTO TypeRecords (BucketID,FileID,TypeName,UserName) VALUES(@bucketid,@fileid,@typename,@username)";

                        //read from the table
                        cmd.CommandText = @"SELECT TypeName FROM TypeRecords WHERE BucketID=@bucketid AND FileID=@fileid AND UserName = @username limit 1 ";
                        var p1 = cmd.CreateParameter();
                        p1.ParameterName = "@bucketid";
                        p1.Value = BucketId;

                        var p2 = cmd.CreateParameter();
                        p2.ParameterName = "@fileid";
                        p2.Value = FileId;
                        var p3 = cmd.CreateParameter();
                        p3.ParameterName = "@username";
                        p3.Value = BaseBll.CrtUser;

                        cmd.Parameters.Add(p1);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p3);
                        using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //long id = reader.GetInt64(0);
                                string TypeInfo = reader.GetString(0);
                                return TypeInfo;
                            }
                        }
                        cmd.Dispose();
                    }
                    if (dbConn.State != System.Data.ConnectionState.Closed) dbConn.Close();
                    dbConn.Dispose();
                  

                }
            }
            catch
            {
                logger.Debug("获取历史操作记录数据时异常");
            }
            return "";
        }
        public void UpdateTypeLogHistory(string BucketId, string FileId, string mark)//原逻辑的UpdateLogHistory
        {
            if (IsExistedTypeTable())
            {

                try
                {
                    using (var dbConn = new System.Data.SQLite.SQLiteConnection(conString))
                    {
                        dbConn.Open();
                        using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                        {
                            cmd.CommandText = @"UPDATE TypeRecords SET TypeName=@typename  WHERE BucketID=@bucketid AND FileID=@fileid AND UserName=@username ";

                            var p1 = cmd.CreateParameter();
                            p1.ParameterName = "@bucketid";
                            p1.Value = BucketId;

                            var p2 = cmd.CreateParameter();
                            p2.ParameterName = "@fileid";
                            p2.Value = FileId;

                            //typename, username
                            var p3 = cmd.CreateParameter();
                            p3.ParameterName = "@typename";
                            p3.Value = mark;
                            var p4 = cmd.CreateParameter();
                            p4.ParameterName = "@username";
                            p4.Value = BaseBll.CrtUser;
                            cmd.Parameters.Add(p1);
                            cmd.Parameters.Add(p2);
                            cmd.Parameters.Add(p3);
                            cmd.Parameters.Add(p4);

                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }
                        if (dbConn.State != System.Data.ConnectionState.Closed) dbConn.Close();
                        dbConn.Dispose();
                      

                    }
                }
                catch
                {
                    logger.Debug("更新操作记录时异常");
                }
            }
        }
        public bool ExistFileIdInFile(string BucketId, string FileId)//原逻辑的ExistInFile
        {
            bool rResult = false;
            try
            {
                using (var dbConn = new System.Data.SQLite.SQLiteConnection(conString))
                {
                    dbConn.Open();
                    using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                    {
                        //read from the table
                        cmd.CommandText = @"SELECT count(*) FROM TypeRecords WHERE BucketID=@bucketid AND FileID=@fileid ";
                        var p1 = cmd.CreateParameter();
                        p1.ParameterName = "@bucketid";
                        p1.Value = BucketId;

                        var p2 = cmd.CreateParameter();
                        p2.ParameterName = "@fileid";
                        p2.Value = FileId;

                        cmd.Parameters.Add(p1);
                        cmd.Parameters.Add(p2);

                        using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                long result = reader.GetInt64(0);
                                //string result = reader.GetString(1);
                                int count = Convert.ToInt32(result);
                                if (count > 0)
                                    return true;
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        cmd.Dispose();
                    }
                    if (dbConn.State != System.Data.ConnectionState.Closed) dbConn.Close();
                    dbConn.Dispose();
                  

                }
            }
            catch (Exception err)
            {
                logger.Debug(err.ToString());
            }
            return rResult;
        }
        public int GetZuihouFileId(string BucketId)//原逻辑的ZuihouFile
        {
            try
            {
                using (var dbConn = new System.Data.SQLite.SQLiteConnection(conString))
                {
                    dbConn.Open();
                    using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                    {
                        //cmd.CommandText = @"INSERT INTO TypeRecords (BucketID,FileID,TypeName,UserName) VALUES(@bucketid,@fileid,@typename,@username)";

                        //read from the table
                        cmd.CommandText = @"SELECT FileID FROM TypeRecords WHERE BucketID=@bucketid AND UserName = @username ORDER BY ID DESC limit 1 ";
                        var p1 = cmd.CreateParameter();
                        p1.ParameterName = "@bucketid";
                        p1.Value = BucketId;
                        var p2 = cmd.CreateParameter();
                        p2.ParameterName = "@username";
                        p2.Value = BaseBll.CrtUser;

                        cmd.Parameters.Add(p1);
                        cmd.Parameters.Add(p2);

                        using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                long FileId = reader.GetInt64(0);
                                return Convert.ToInt32(FileId);
                            }
                        }
                        cmd.Dispose();
                    }
                    if (dbConn.State != System.Data.ConnectionState.Closed) dbConn.Close();
                    dbConn.Dispose();

                }
            }
            catch
            {
                logger.Debug("获取历史操作记录数据时异常");
            }
            return 0;
        }
        public int GetLastFileID(string BucketId, string FileId)//原逻辑的GetLastFileId
        {
            try
            {
                using (var dbConn = new System.Data.SQLite.SQLiteConnection(conString))
                {
                    dbConn.Open();
                    using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT FileID FROM TypeRecords 
                                            WHERE BucketID=@bucketid 
                                                AND UserName = @username 
	                                            AND ID < (select id from TypeRecords where FileID = @fileid)
	                                            ORDER BY ID DESC limit 1 ";
                        var p1 = cmd.CreateParameter();
                        p1.ParameterName = "@bucketid";
                        p1.Value = BucketId;
                        var p2 = cmd.CreateParameter();
                        p2.ParameterName = "@username";
                        p2.Value = BaseBll.CrtUser;

                        var p3 = cmd.CreateParameter();
                        p3.ParameterName = "@fileid";
                        p3.Value = FileId;

                        cmd.Parameters.Add(p1);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p3);

                        using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                long FileId0 = reader.GetInt64(0);
                                return Convert.ToInt32(FileId0);
                            }
                        }
                        cmd.Dispose();
                    }
                    if (dbConn.State != System.Data.ConnectionState.Closed) dbConn.Close();
                    dbConn.Dispose();

                }
            }
            catch
            {
                logger.Debug("获取历史操作记录数据时异常");
            }
            return 0;
        }
        public int GetNextFileID(string BucketId, string FileId)//原逻辑的GetNextFileId
        {
            try
            {
                using (var dbConn = new System.Data.SQLite.SQLiteConnection(conString))
                {
                    dbConn.Open();
                    using (System.Data.Common.DbCommand cmd = dbConn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT FileID FROM TypeRecords 
                                            WHERE BucketID=@bucketid 
                                                AND UserName = @username 
	                                            AND ID > (select id from TypeRecords where FileID = @fileid)
	                                            ORDER BY ID ASC limit 1 ";
                        var p1 = cmd.CreateParameter();
                        p1.ParameterName = "@bucketid";
                        p1.Value = BucketId;
                        var p2 = cmd.CreateParameter();
                        p2.ParameterName = "@username";
                        p2.Value = BaseBll.CrtUser;

                        var p3 = cmd.CreateParameter();
                        p3.ParameterName = "@fileid";
                        p3.Value = FileId;

                        cmd.Parameters.Add(p1);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p3);

                        using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                long FileId0 = reader.GetInt64(0);
                                return Convert.ToInt32(FileId0);
                            }
                        }
                        cmd.Dispose();
                    }
                    if (dbConn.State != System.Data.ConnectionState.Closed) dbConn.Close();
                    dbConn.Dispose();
                  

                }
            }
            catch
            {
                logger.Debug("获取历史操作记录数据时异常");
            }
            return 0;
        }
        #endregion
        //检查是否有web的配置信息
        public bool ExistWebFile()
        {
            string reFile = @"WebInfo.txt";
            bool isExist = File.Exists(reFile);
            if (isExist)
            {
                List<string> list = loadFileInfo(reFile);
                if (list.Count > 0)
                {
                    BaseBll bll = new BaseBll();
                    bll.SetCrtUrl(list[list.Count - 1]);
                    //bll.AttenceUrl = list[list.Count-1];
                    return true;
                }
                else
                    return false;
            }
            else {
                return false;
            }
            //string item = host + ":" + port + "/";

        }
        //写网站信息
        public void WriteWebInfo(string host, string port)
        {
            string reFile = @"WebInfo.txt";
            if (!ExistWebFile())
            {
                string item = host + ":" + port + "/";
                List<string> mylist = new List<string>();
                mylist.Add(item);
                writeNew(mylist, reFile);
            }
            else {
                return;
            }
        }
        public string GetAttenceUrl()
        {
            string url = "";
            string reFile = @"WebInfo.txt";
            List<string> list = loadFileInfo(reFile);
            if (list.Count == 0)
            {
                url = "";
            }
            else
            {
                url = list.ElementAt(list.Count - 1);
            }
            return url;
        }
    
        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <returns></returns>
        public static bool checkFileExists(String fileName)
        {
            if (File.Exists(fileName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<string> loadFileInfo(string fileName)
        {
            bool success = false;
            List<string> list = new List<string>();
            while (success == false)
            {
                try
                {

                    list.Clear();
                    if (checkFileExists(fileName) == false)
                    {
                        success = true;
                        return list;
                    }

                    StreamReader txtReader = new StreamReader(fileName);
                    while (txtReader.Peek() >= 0)
                    {
                        string str = txtReader.ReadLine();
                        list.Add(str);
                    }
                    txtReader.Close();
                    success = true;

                }
                catch (Exception e1)
                {
                    Console.WriteLine(e1.Message + e1.StackTrace);
                }
            }
            return list;
        }


    
        public void writeNew(List<string> list, string fileName)
        {
            bool success = false;
            while (success == false)
            {
                try
                {
                    StreamWriter TxtWriter = new StreamWriter(fileName);
                    for (int i = 0; i < list.Count; i++)
                    {
                        string FileContent = (list[i]).ToString();
                        TxtWriter.WriteLine(FileContent);

                    }
                    TxtWriter.Close();
                    success = true;
                }
                catch (Exception e1)
                {
                    Console.WriteLine(e1.Message + e1.StackTrace);
                }

            }

        }
    }
}
