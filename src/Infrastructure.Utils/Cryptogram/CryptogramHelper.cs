/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 18:06:28
** desc：    CryptogramHelper类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Infrastructure.Cryptogram
{
    public class CryptogramHelper
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="srcData"></param>
        /// <param name="encryPWD">8位字符</param>
        /// <returns></returns>
        public static string DESEncrypt(string srcData, string encryPWD)
        {
            if (string.IsNullOrEmpty(srcData) || string.IsNullOrEmpty(encryPWD))
            {
                return string.Empty;
            }
            byte[] rgbIV = new byte[8];
            string s = encryPWD.Substring(0, 8);
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            rgbIV = Encoding.UTF8.GetBytes(s);
            DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
            byte[] bytes2 = Encoding.UTF8.GetBytes(srcData);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(bytes, rgbIV), CryptoStreamMode.Write);
            cryptoStream.Write(bytes2, 0, bytes2.Length);
            cryptoStream.FlushFinalBlock();
            StringBuilder stringBuilder = new StringBuilder();
            byte[] array = memoryStream.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                byte b = array[i];
                stringBuilder.AppendFormat("{0:X2}", b);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptData"></param>
        /// <param name="encryPWD">8位字符</param>
        /// <returns></returns>
        public static string DESDecrypt(string encryptData, string encryPWD)
        {
            if (string.IsNullOrEmpty(encryptData) || string.IsNullOrEmpty(encryPWD))
            {
                return string.Empty;
            }
            byte[] rgbIV = new byte[8];
            string s = encryPWD.Substring(0, 8);
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            rgbIV = Encoding.UTF8.GetBytes(s);
            byte[] array = new byte[encryptData.Length / 2];
            for (int i = 0; i < encryptData.Length / 2; i++)
            {
                int num = Convert.ToInt32(encryptData.Substring(i * 2, 2), 16);
                array[i] = (byte)num;
            }
            string result;
            try
            {
                DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(bytes, rgbIV), CryptoStreamMode.Write);
                cryptoStream.Write(array, 0, array.Length);
                cryptoStream.FlushFinalBlock();
                Encoding encoding = new UTF8Encoding();
                result = encoding.GetString(memoryStream.ToArray());
            }
            catch
            {
                result = "";
            }
            return result;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="srcData"></param>
        /// <param name="m_Key">8位字符</param>
        /// <returns></returns>
        public static string Encrypt(string srcData, string m_Key)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(srcData);
            DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
            string s = m_Key.Substring(0, 8);
            dESCryptoServiceProvider.Key = Encoding.ASCII.GetBytes(s);
            dESCryptoServiceProvider.IV = Encoding.ASCII.GetBytes(s);
            ICryptoTransform cryptoTransform = dESCryptoServiceProvider.CreateEncryptor();
            byte[] inArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptData">8位字符</param>
        /// <param name="m_Key"></param>
        /// <returns></returns>
        public static string Decrypt(string encryptData, string m_Key)
        {
            byte[] array = Convert.FromBase64String(encryptData);
            DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
            string s = m_Key.Substring(0, 8);
            dESCryptoServiceProvider.Key = Encoding.ASCII.GetBytes(s);
            dESCryptoServiceProvider.IV = Encoding.ASCII.GetBytes(s);
            ICryptoTransform cryptoTransform = dESCryptoServiceProvider.CreateDecryptor();
            byte[] bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string GetMd5Hash(string input)
        {
            MD5 mD = MD5.Create();
            byte[] array = mD.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                stringBuilder.Append(array[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public static string SignDataForCert(string srcData, string xpath)
        {
            X509Certificate2 x509Certificate = new X509Certificate2(xpath, "ucs2013");
            RSACryptoServiceProvider rSACryptoServiceProvider = x509Certificate.PrivateKey as RSACryptoServiceProvider;
            byte[] bytes = Encoding.UTF8.GetBytes(srcData);
            byte[] inArray = rSACryptoServiceProvider.SignData(bytes, "SHA1");
            return Convert.ToBase64String(inArray);
        }

        public static bool VerifyDataForCert(string srcData, string token, string xpath)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(srcData);
            X509Certificate2 x509Certificate = new X509Certificate2(xpath);
            RSACryptoServiceProvider rSACryptoServiceProvider = x509Certificate.PublicKey.Key as RSACryptoServiceProvider;
            byte[] signature = Convert.FromBase64String(token);
            return rSACryptoServiceProvider.VerifyData(bytes, new SHA1CryptoServiceProvider(), signature);
        }

        public static string CertEncrypt(string srcData, string cerPath)
        {
            string result;
            try
            {
                X509Certificate2 x509Certificate = new X509Certificate2(cerPath);
                RSACryptoServiceProvider rSACryptoServiceProvider = x509Certificate.PublicKey.Key as RSACryptoServiceProvider;
                byte[] bytes = Encoding.UTF8.GetBytes(srcData);
                byte[] inArray = rSACryptoServiceProvider.Encrypt(bytes, true);
                string text = Convert.ToBase64String(inArray);
                result = text;
            }
            catch (Exception)
            {
                result = string.Empty;
            }
            return result;
        }

        public static string CertDecrypt(string enData, string cerPath, string cerPwd)
        {
            string result;
            try
            {
                byte[] rgb = Convert.FromBase64String(enData);
                X509Certificate2 x509Certificate = new X509Certificate2(cerPath, cerPwd);
                RSACryptoServiceProvider rSACryptoServiceProvider = x509Certificate.PrivateKey as RSACryptoServiceProvider;
                byte[] bytes = rSACryptoServiceProvider.Decrypt(rgb, true);
                result = Encoding.UTF8.GetString(bytes);
            }
            catch (Exception)
            {
                result = string.Empty;
            }
            return result;
        }

        public static string EncodeBase64(string codeType, string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return "";
            }
            string result = "";
            byte[] bytes = Encoding.GetEncoding(codeType).GetBytes(code);
            try
            {
                result = Convert.ToBase64String(bytes);
            }
            catch
            {
                result = code;
            }
            return result;
        }

        public static string DecodeBase64(string codeType, string code)
        {
            string result = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                result = Encoding.GetEncoding(codeType).GetString(bytes);
            }
            catch
            {
                result = code;
            }
            return result;
        }
    }
}
