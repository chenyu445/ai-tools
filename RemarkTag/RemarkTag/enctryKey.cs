using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;
using System.IO;
using Org.BouncyCastle;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Parameters;

namespace RemarkTag
{
    public class enctryKey
    {     
        //编码 
        public string EncodeBase64(string code_type,string code) 
        { 
            string encode = ""; 

            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code); 
            try 
            { 
                 encode = Convert.ToBase64String(bytes); 
            } 
            catch 
            { 
                 encode = code; 
            } 
            return encode; 
        } 
            //解码 --base64
        public byte[] DecodeBase64(string code_type, string code) 
        { 
           // string decode = "";
            byte[] bytes = Convert.FromBase64String(code.Replace('-', '+').Replace('_', '/'));
            return bytes;
            //try 
            //{
            //    if (code_type == "UTF8")
            //    {
            //        decode = Encoding.UTF8.GetString(bytes);//Encoding.GetEncoding(System.Text.Encoding.UTF8.ToString()).GetString(bytes); 
            //    }
            //    else { 
            //        decode = Encoding.ASCII.GetString(bytes);
            //    }
            //} 
            //catch 
            //{ 
            //      decode = code; 
            //} 
            //    return decode; 
        } 
      
        //AES解密算法
        /// <summary>
        /// 获取Aes32位密钥
        /// </summary>
        /// <param name="key">Aes密钥字符串</param>
        /// <returns>Aes32位密钥</returns>
        static byte[] GetAesKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "Aes密钥不能为空");
            }
            if (key.Length < 32)
            {
                // 不足32补全
                key = key.PadRight(32, '0');
            }
            if (key.Length > 32)
            {
                key = key.Substring(0, 32);
            }
            return Encoding.UTF8.GetBytes(key);
        }
        /// <summary>  
        /// AES加密  
        /// </summary>  
        /// <param name="Data">被加密的明文</param>  
        /// <param name="Key">密钥</param>  
        /// <param name="Vector">向量</param>  
        /// <returns>密文</returns>  
        public static String AESEncrypt(String Data, String Key, String Vector)
        {
            Byte[] plainBytes = Encoding.UTF8.GetBytes(Data);

            Byte[] bKey = new Byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);
            Byte[] bVector = new Byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(bVector.Length)), bVector, bVector.Length);

            Byte[] Cryptograph = null; // 加密后的密文  

            Rijndael Aes = Rijndael.Create();
            try
            {
                // 开辟一块内存流  
                using (MemoryStream Memory = new MemoryStream())
                {
                    // 把内存流对象包装成加密流对象  
                    using (CryptoStream Encryptor = new CryptoStream(Memory,
                     Aes.CreateEncryptor(bKey, bVector),
                     CryptoStreamMode.Write))
                    {
                        // 明文数据写入加密流  
                        Encryptor.Write(plainBytes, 0, plainBytes.Length);
                        Encryptor.FlushFinalBlock();

                        Cryptograph = Memory.ToArray();
                    }
                }
            }
            catch
            {
                Cryptograph = null;
            }

            return Convert.ToBase64String(Cryptograph);
        }

        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="Data">被解密的密文</param>  
        /// <param name="Key">密钥</param>  
        /// <param name="Vector">向量</param>  
        /// <returns>明文</returns>  
        public Byte[] AESDecrypt(byte[] encryptedBytes, String Key, String Vector)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            //rijndaelCipher.Mode = CipherMode.CBC;
            //rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.Mode = CipherMode.CBC; //CipherMode.ECB;CipherMode.OFB;//
            rijndaelCipher.Padding = PaddingMode.Zeros;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] encryptedData = encryptedBytes;//Convert.FromBase64String(text);
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(Key);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes(Vector);
            rijndaelCipher.IV = ivBytes;
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return plainText;
            ////////////////////////////////////////////////////////////

        }


        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="Data">被解密的密文</param>  
        /// <param name="Key">密钥</param>  
        /// <param name="Vector">向量</param>  
        /// <returns>明文</returns>  
        public  String AESDecryptPicFile(String localFilePath, String Key, String Vector)
        {
            //第一步把本地文件转
            Byte[] encryptedBytes = Convert.FromBase64String(localFilePath);

            Byte[] bKey = new Byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);
            Byte[] bVector = new Byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(bVector.Length)), bVector, bVector.Length);

            Byte[] original = null; // 解密后的明文  

            Rijndael Aes = Rijndael.Create();
            try
            {
                // 开辟一块内存流，存储密文  
                using (MemoryStream Memory = new MemoryStream(encryptedBytes))
                {
                    // 把内存流对象包装成加密流对象  
                    using (CryptoStream Decryptor = new CryptoStream(Memory,
                    Aes.CreateDecryptor(bKey, bVector),
                    CryptoStreamMode.Read))
                    {
                        // 明文存储区  
                        using (MemoryStream originalMemory = new MemoryStream())
                        {
                            Byte[] Buffer = new Byte[1024];
                            Int32 readBytes = 0;
                            while ((readBytes = Decryptor.Read(Buffer, 0, Buffer.Length)) > 0)
                            {
                                originalMemory.Write(Buffer, 0, readBytes);
                            }

                            original = originalMemory.ToArray();
                        }
                    }
                }
            }
            catch
            {
                original = null;
            }
            return Encoding.UTF8.GetString(original);
        }
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="publickey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string RSAEncrypt(string publickey, string content)
        {
            publickey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publickey);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);

            return Convert.ToBase64String(cipherbytes);
        }
        private  int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)        //expect integer
                return 0;
            bt = binr.ReadByte();


            if (bt == 0x81)
                count = binr.ReadByte();    // data size in next byte
            else
                if (bt == 0x82)
                {
                    highbyte = binr.ReadByte();    // data size in next 2 bytes
                    lowbyte = binr.ReadByte();
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                    count = BitConverter.ToInt32(modint, 0);
                }
                else
                {
                    count = bt;        // we already have the data size
                }


            while (binr.ReadByte() == 0x00)
            {    //remove high order zeros in data
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);        //last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }

        public  RSACryptoServiceProvider DecodeRSAPrivateKey(string priKey)
        {
            //var privkey = Convert.FromBase64String(priKey);
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;


            // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
            //MemoryStream mem = new MemoryStream(privkey);
            //BinaryReader binr = new BinaryReader(mem);
            string path = @"Pic/li_pri.der";
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

            BinaryReader binr = new BinaryReader(fs);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;
            int elems = 0;
            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();        //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();       //advance 2 bytes
                else
                    return null;


                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102) //version number
                    return null;
                bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;




                //------  all private key components are Integer sequences ----
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);


                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSAParameters RSAparams = new RSAParameters();
                RSAparams.Modulus = MODULUS;
                RSAparams.Exponent = E;
                RSAparams.D = D;
                RSAparams.P = P;
                RSAparams.Q = Q;
                RSAparams.DP = DP;
                RSAparams.DQ = DQ;
                RSAparams.InverseQ = IQ;
                RSA.ImportParameters(RSAparams);




                return RSA;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
                return null;
            }
            finally
            {
                binr.Close();
            }
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privatekey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string RSADecryptFromFile(string File, string content)
        {
            // privatekey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent><P>/hf2dnK7rNfl3lbqghWcpFdu778hUpIEBixCDL5WiBtpkZdpSw90aERmHJYaW2RGvGRi6zSftLh00KHsPcNUMw==</P><Q>6Cn/jOLrPapDTEp1Fkq+uz++1Do0eeX7HYqi9rY29CqShzCeI7LEYOoSwYuAJ3xA/DuCdQENPSoJ9KFbO4Wsow==</Q><DP>ga1rHIJro8e/yhxjrKYo/nqc5ICQGhrpMNlPkD9n3CjZVPOISkWF7FzUHEzDANeJfkZhcZa21z24aG3rKo5Qnw==</DP><DQ>MNGsCB8rYlMsRZ2ek2pyQwO7h/sZT8y5ilO9wu08Dwnot/7UMiOEQfDWstY3w5XQQHnvC9WFyCfP4h4QBissyw==</DQ><InverseQ>EG02S7SADhH1EVT9DD0Z62Y0uY7gIYvxX/uq+IzKSCwB8M2G7Qv9xgZQaQlLpCaeKbux3Y59hHM+KpamGL19Kg==</InverseQ><D>vmaYHEbPAgOJvaEXQl+t8DQKFT1fudEysTy31LTyXjGu6XiltXXHUuZaa2IPyHgBz0Nd7znwsW/S44iql0Fen1kzKioEL3svANui63O3o5xdDeExVM6zOf1wUUh/oldovPweChyoAdMtUzgvCbJk1sYDJf++Nr0FeNW1RB1XG30=</D></RSAKeyValue>";

            FileStream fsRead = new FileStream(File, FileMode.Open);
            int fsLen = (int)fsRead.Length;
            byte[] array = new byte[fsLen];
            int r = fsRead.Read(array, 0, array.Length);
            string myStr = System.Text.Encoding.UTF8.GetString(array);

            string reStr = RSADecrypt(myStr, content);
            return reStr;
           
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privatekey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public  string RSADecrypt(string privatekey, string content)
        {
           // privatekey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent><P>/hf2dnK7rNfl3lbqghWcpFdu778hUpIEBixCDL5WiBtpkZdpSw90aERmHJYaW2RGvGRi6zSftLh00KHsPcNUMw==</P><Q>6Cn/jOLrPapDTEp1Fkq+uz++1Do0eeX7HYqi9rY29CqShzCeI7LEYOoSwYuAJ3xA/DuCdQENPSoJ9KFbO4Wsow==</Q><DP>ga1rHIJro8e/yhxjrKYo/nqc5ICQGhrpMNlPkD9n3CjZVPOISkWF7FzUHEzDANeJfkZhcZa21z24aG3rKo5Qnw==</DP><DQ>MNGsCB8rYlMsRZ2ek2pyQwO7h/sZT8y5ilO9wu08Dwnot/7UMiOEQfDWstY3w5XQQHnvC9WFyCfP4h4QBissyw==</DQ><InverseQ>EG02S7SADhH1EVT9DD0Z62Y0uY7gIYvxX/uq+IzKSCwB8M2G7Qv9xgZQaQlLpCaeKbux3Y59hHM+KpamGL19Kg==</InverseQ><D>vmaYHEbPAgOJvaEXQl+t8DQKFT1fudEysTy31LTyXjGu6XiltXXHUuZaa2IPyHgBz0Nd7znwsW/S44iql0Fen1kzKioEL3svANui63O3o5xdDeExVM6zOf1wUUh/oldovPweChyoAdMtUzgvCbJk1sYDJf++Nr0FeNW1RB1XG30=</D></RSAKeyValue>";
            //privatekey = privatekey.Replace("-----BEGIN RSA PRIVATE KEY-----", "");
            //privatekey = privatekey.Replace("-----END RSA PRIVATE KEY-----", "");

            AsymmetricCipherKeyPair keyPair;
            /////
            string str = privatekey;             //convert string 2 stream            
            byte[] array = Encoding.ASCII.GetBytes(str);
            MemoryStream stream = new MemoryStream(array);             //convert stream 2 string      
           
            using ( StreamReader sr = new StreamReader(stream))
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

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
         
            rsa.ImportParameters(p);
            String PrivateKeyXml = rsa.ToXmlString(true);

           
            byte[] cipherbytes;
            rsa.FromXmlString(PrivateKeyXml);
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);

            return Encoding.UTF8.GetString(cipherbytes);
        }

        public string MyRSADecrypt(string privatekey, byte[] content)
        {
            // privatekey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent><P>/hf2dnK7rNfl3lbqghWcpFdu778hUpIEBixCDL5WiBtpkZdpSw90aERmHJYaW2RGvGRi6zSftLh00KHsPcNUMw==</P><Q>6Cn/jOLrPapDTEp1Fkq+uz++1Do0eeX7HYqi9rY29CqShzCeI7LEYOoSwYuAJ3xA/DuCdQENPSoJ9KFbO4Wsow==</Q><DP>ga1rHIJro8e/yhxjrKYo/nqc5ICQGhrpMNlPkD9n3CjZVPOISkWF7FzUHEzDANeJfkZhcZa21z24aG3rKo5Qnw==</DP><DQ>MNGsCB8rYlMsRZ2ek2pyQwO7h/sZT8y5ilO9wu08Dwnot/7UMiOEQfDWstY3w5XQQHnvC9WFyCfP4h4QBissyw==</DQ><InverseQ>EG02S7SADhH1EVT9DD0Z62Y0uY7gIYvxX/uq+IzKSCwB8M2G7Qv9xgZQaQlLpCaeKbux3Y59hHM+KpamGL19Kg==</InverseQ><D>vmaYHEbPAgOJvaEXQl+t8DQKFT1fudEysTy31LTyXjGu6XiltXXHUuZaa2IPyHgBz0Nd7znwsW/S44iql0Fen1kzKioEL3svANui63O3o5xdDeExVM6zOf1wUUh/oldovPweChyoAdMtUzgvCbJk1sYDJf++Nr0FeNW1RB1XG30=</D></RSAKeyValue>";
            privatekey = privatekey.Replace("-----BEGIN RSA PRIVATE KEY-----\n", "");
            privatekey = privatekey.Replace("\n-----END RSA PRIVATE KEY-----", "");
           
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                //RSA.ImportParameters(RSAKeyInfo);
                byte[] bytes_Public_Key = Convert.FromBase64String(privatekey);
                RSA.ImportCspBlob(bytes_Public_Key);

                //OAEP padding is only available on Microsoft Windows XP or later. 
                byte[] bytes_Plain_Text = RSA.Decrypt(content, false);
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                string str_Plain_Text = ByteConverter.GetString(bytes_Plain_Text);
                return str_Plain_Text;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        //RSA解密
         public string RSA_Decrypt(string str_Cypher_Text, string str_Private_Key)
        {
            byte[] DataToDecrypt = Convert.FromBase64String(str_Cypher_Text);
            try
            {
                str_Private_Key = str_Private_Key.Replace("-----BEGIN RSA PRIVATE KEY-----", "");
                str_Private_Key = str_Private_Key.Replace("-----END RSA PRIVATE KEY-----", "");
           
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                //RSA.ImportParameters(RSAKeyInfo);
                byte[] bytes_Public_Key = Convert.FromBase64String(str_Private_Key);
                RSA.ImportCspBlob(bytes_Public_Key);

                //OAEP padding is only available on Microsoft Windows XP or later. 
                byte[] bytes_Plain_Text = RSA.Decrypt(DataToDecrypt, false);
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                string str_Plain_Text = ByteConverter.GetString(bytes_Plain_Text);
                return str_Plain_Text;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

         /// <summary>
         /// 计算文件的MD5校验
         /// </summary>
         /// <param name="fileName"></param>
         /// <returns></returns>
         public string GetMD5HashFromFile(string fileName)
         {
             try
             {
                 FileStream file = new FileStream(fileName, FileMode.Open);
                 System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                 byte[] retVal = md5.ComputeHash(file);
                 file.Close();
                 StringBuilder sb = new StringBuilder();
                 for (int i = 0; i < retVal.Length; i++)
                 {
                     sb.Append(retVal[i].ToString("x2"));
                 }
                 return sb.ToString();
             }
             catch (Exception ex)
             {
                 throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
             }
         }
         /// <summary>
         /// SHA256
         /// </summary>
         /// <param name="strData"></param>
         /// <returns></returns>
         public string GetSHA256HashFromString(string strData)
         {
             byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(strData);
             try
             {
                 SHA256 sha256 = new SHA256CryptoServiceProvider();
                 byte[] retVal = sha256.ComputeHash(bytValue);
                 StringBuilder sb = new StringBuilder();
                 for (int i = 0; i < retVal.Length; i++)
                 {
                     sb.Append(retVal[i].ToString("x2"));
                 }
                 return sb.ToString();
             }
             catch (Exception ex)
             {
                 throw new Exception("GetSHA256HashFromString() fail,error:" + ex.Message);
             }
         }  
      
  }
}
