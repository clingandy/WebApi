/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 17:59:53
** desc：    Authenticator类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Cryptogram
{
    public class Authenticator
    {

        private static readonly byte[] iv = {
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8
        };

        public static string GenerateAuthenticator(string key, string data)
        {
            string result;
            try
            {
                Encoding uTF = Encoding.UTF8;
                byte[] bytes = uTF.GetBytes(data);
                SHA1 sHA = SHA1.Create();
                sHA.ComputeHash(bytes);
                byte[] key2 = Convert.FromBase64String(key);
                byte[] inArray = Des3EncodeCBC(key2, iv, bytes);
                result = Convert.ToBase64String(inArray);
            }
            catch (Exception ex)
            {
                throw new Exception("GenerateAuthenticator生成签名验证码时发生错误" + ex.Message);
            }
            return result;
        }

        public static byte[] Des3EncodeCBC(byte[] key, byte[] iv, byte[] data)
        {
            byte[] result;
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, new TripleDESCryptoServiceProvider
                {
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                }.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();
                byte[] array = memoryStream.ToArray();
                cryptoStream.Close();
                memoryStream.Close();
                result = array;
            }
            catch (CryptographicException ex)
            {
                throw new Exception("Des3EncodeCBC时发生错误" + ex.Message);
            }
            return result;
        }

        public static byte[] Des3DecodeCBC(byte[] key, byte[] iv, byte[] data)
        {
            byte[] result;
            try
            {
                MemoryStream stream = new MemoryStream(data);
                CryptoStream cryptoStream = new CryptoStream(stream, new TripleDESCryptoServiceProvider
                {
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                }.CreateDecryptor(key, iv), CryptoStreamMode.Read);
                byte[] array = new byte[data.Length];
                cryptoStream.Read(array, 0, array.Length);
                result = array;
            }
            catch (CryptographicException ex)
            {
                throw new Exception("Des3DecodeCBC时发生错误" + ex.Message);
            }
            return result;
        }
    }
}
