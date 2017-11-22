using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Runtime.InteropServices;
namespace RemarkTag
{
    public class Der2xml
    {
        private static int GetIntegerSize(BinaryReader binr)
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


        //E:\01Doc\010技术文档\openssl-1.0.2a\OpenSSL_SrcandLib\win32lib\bin>openssl.exe r
        //sa -in rsakeydec.txt -outform der -out pri.der
        //writing RSA key

        public static RSACryptoServiceProvider DecodeRSAPrivateKey(string priKey)
        {
            //var privkey = Convert.FromBase64String(priKey);
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;


            // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
            //MemoryStream mem = new MemoryStream(privkey);
            //BinaryReader binr = new BinaryReader(mem);
            string path = @"D:\\project\\ConsoleApplication1\\li_pri.der";
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


        /************************************************************************/
        /* 你的是pem key，需要下转换为c# rsa provider认识的key。
            首先，干掉头部和尾部的无用字符，然后利用如下方法转换：*/
        /************************************************************************/
        public static void PrivateKeyDecFun()
        {
            string priKey = @"-----BEGIN RSA PRIVATE KEY-----
                MIICWwIBAAKBgQCf1a4LQyipBqeUCZ9kKsfasQzkEFCBmGsM21Sakb5BO0sY07GD
                cproJHF2xNQrV0cM7+liE3pBUFsarui2WaHZhAibpLbl9z4FSfoN5hSg6sEgbB17
                SvKe3ZN/75GoEsQiQtYW4gUJgzrBovVZ+TeTnN+NHHBqUqBKhNIgPFVapQIDAQAB
                AoGAG0OMs5kaF3LuJN9bU+/ENXab908dHG4OXJwRG2ie5muhzLNXhU+IQu7sd9Dt
                TBNQKFHIIpWl9fwp/iw1v90cMUQGj0zhSXHAz7Vak/ryQLTyeIIciL8MQWvnbAaN
                lIoFq2wBl7SYs3n71B4MlvvTysaG0krsjiPh5LVgnBvzjGECQQDcAwe4XnF7SHWO
                nfljrG29soKNiUhYKtDGcV9fvam9u50Ek882wvFmsJP+tk+1CXjMRSNlOi40bxKC
                uaBa1JOtAkEAufq9FmZHfBFf3e6n57wLiAj5C1MeyHAtt6qdAF49OZJBGZh1pePn
                jDGNezFvy7U5bMp7/updisLCFueS5eKB2QJAF84QIMe/OZqedZ7sI/e9LABLlerb
                tAZ17nLH4gEQg6HwHFWt3vv6yKSkbrPlLe5nbpqweLxx0WSPOSvCiPFlRQJAPAfF
                NQ+6jz+EdDxukgxOpJBQ4ujnjMc42ooFt3KzzHt66+ocP3m66bOs+VDRxy0t5gHN
                2FCJ9Ro8T+xbrDxasQJAARHpcG6tE0F+lmUthtep1U8OrF+AQvqDhBq8MYK+/pF/
                LRZkFHkqTsj89OyWDlSH3LeYkOWsr9mAFxsvHZ9BSA==
                -----END RSA PRIVATE KEY-----";




            priKey = priKey.Replace("-----BEGIN RSA PRIVATE KEY-----", "")
                .Replace("-----END RSA PRIVATE KEY-----", "");




            RSACryptoServiceProvider rsaProvider = DecodeRSAPrivateKey(priKey);
            //RSACryptoServiceProvider rsaProvider = DecodeRSAPrivateKey();
            //rsaProvider.FromXmlString();
            String PrivateKey = rsaProvider.ToXmlString(true);//将RSA算法的私钥导出到字符串PrivateKey中，参数为true表示导出私钥
            Console.WriteLine(PrivateKey);
            /************************************************************************/
            /* 程序运行结果如下：
             * <RSAKeyValue><Modulus>n9WuC0MoqQanlAmfZCrH2rEM5BBQgZhrDNtUmpG+QTtLGNOxg3Ka6CRxds
                TUK1dHDO/pYhN6QVBbGq7otlmh2YQIm6S25fc+BUn6DeYUoOrBIGwde0rynt2Tf++RqBLEIkLWFuIFCY
                M6waL1Wfk3k5zfjRxwalKgSoTSIDxVWqU=</Modulus><Exponent>AQAB</Exponent><P>3AMHuF5x
                e0h1jp35Y6xtvbKCjYlIWCrQxnFfX72pvbudBJPPNsLxZrCT/rZPtQl4zEUjZTouNG8SgrmgWtSTrQ==
                </P><Q>ufq9FmZHfBFf3e6n57wLiAj5C1MeyHAtt6qdAF49OZJBGZh1pePnjDGNezFvy7U5bMp7/updi
                sLCFueS5eKB2Q==</Q><DP>F84QIMe/OZqedZ7sI/e9LABLlerbtAZ17nLH4gEQg6HwHFWt3vv6yKSkb
                rPlLe5nbpqweLxx0WSPOSvCiPFlRQ==</DP><DQ>PAfFNQ+6jz+EdDxukgxOpJBQ4ujnjMc42ooFt3Kz
                zHt66+ocP3m66bOs+VDRxy0t5gHN2FCJ9Ro8T+xbrDxasQ==</DQ><InverseQ>ARHpcG6tE0F+lmUth
                tep1U8OrF+AQvqDhBq8MYK+/pF/LRZkFHkqTsj89OyWDlSH3LeYkOWsr9mAFxsvHZ9BSA==</Inverse
                Q><D>G0OMs5kaF3LuJN9bU+/ENXab908dHG4OXJwRG2ie5muhzLNXhU+IQu7sd9DtTBNQKFHIIpWl9fw
                p/iw1v90cMUQGj0zhSXHAz7Vak/ryQLTyeIIciL8MQWvnbAaNlIoFq2wBl7SYs3n71B4MlvvTysaG0kr
                sjiPh5LVgnBvzjGE=</D></RSAKeyValue>
                  请按任意键继续. . .*/
            /************************************************************************/
        }


        public static void PrivateKeyDecFun1()
        {
            RSACryptoServiceProvider rsaProvider = DecodeRSAPrivateKey(null);
            String PrivateKey = rsaProvider.ToXmlString(true);//将RSA算法的私钥导出到字符串PrivateKey中，参数为true表示导出私钥
            Console.WriteLine(PrivateKey);


        }


    }
}
