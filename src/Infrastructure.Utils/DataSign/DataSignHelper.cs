/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 18:26:18
** desc：    DataSign类
** Ver.:     V1.0.0
*********************************************************************************/

using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Infrastructure.Common;

namespace Infrastructure.DataSign
{
    public class DataSignHelper
    {
        private string PfxPath;

        private string CerPath;

        private string Password;

        public DataSignHelper(string pfxPath, string cerPath, string password)
        {
            this.PfxPath = pfxPath;
            this.CerPath = cerPath;
            this.Password = password;
        }

        public string Sign(string strSrcData)
        {
            string result;
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(strSrcData);
                X509Certificate2 x509Certificate = new X509Certificate2(this.PfxPath, this.Password, X509KeyStorageFlags.MachineKeySet);
                RSACryptoServiceProvider rSACryptoServiceProvider = x509Certificate.PrivateKey as RSACryptoServiceProvider;
                byte[] inArray = rSACryptoServiceProvider.SignData(bytes, new SHA1CryptoServiceProvider());
                result = Convert.ToBase64String(inArray);
            }
            catch (Exception ex)
            {
                Logging.LogError("签名失败", ex, null);
                result = "";
            }
            return result;
        }

        public bool VerifyData(string source, string signature)
        {
            bool result;
            try
            {
                byte[] rgbHash = this.Hash(source);
                X509Certificate2 x509Certificate = new X509Certificate2(this.CerPath);
                string xmlString = x509Certificate.PublicKey.Key.ToXmlString(false);
                RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
                rSACryptoServiceProvider.FromXmlString(xmlString);
                RSAPKCS1SignatureDeformatter rSAPKCS1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rSACryptoServiceProvider);
                rSAPKCS1SignatureDeformatter.SetHashAlgorithm("SHA1");
                result = rSAPKCS1SignatureDeformatter.VerifySignature(rgbHash, Convert.FromBase64String(signature));
            }
            catch (Exception ex)
            {
                Logging.LogError("数据验签失败", ex, null);
                result = false;
            }
            return result;
        }

        public bool VerifyDataBase64(string strSrcData, string strSignData)
        {
            string cerPath = this.CerPath;
            byte[] bytes = Encoding.UTF8.GetBytes(strSrcData);
            byte[] signature = Convert.FromBase64String(strSignData);
            string text = this.LoadCerContent(cerPath);
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            SHA1CryptoServiceProvider halg = new SHA1CryptoServiceProvider();
            RSAParameters parameters = this.ConvertFromPemPublicKey(text);
            RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
            rSACryptoServiceProvider.ImportParameters(parameters);
            return rSACryptoServiceProvider.VerifyData(bytes, halg, signature);
        }

        private byte[] Hash(string text)
        {
            SHA1 sHA = SHA1.Create();
            return sHA.ComputeHash(Encoding.UTF8.GetBytes(text));
        }

        private RSAParameters ConvertFromPemPublicKey(string pemFileConent)
        {
            if (string.IsNullOrEmpty(pemFileConent))
            {
                throw new ArgumentNullException(nameof(pemFileConent), "This arg cann't be empty.");
            }
            pemFileConent = pemFileConent.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "").Replace("\n", "").Replace("\r", "");
            byte[] array = Convert.FromBase64String(pemFileConent);
            bool flag = array.Length == 162;
            bool flag2 = array.Length == 294;
            if (!flag && !flag2)
            {
                throw new ArgumentException("pem file content is incorrect, Only support the key size is 1024 or 2048");
            }
            byte[] array2 = flag ? new byte[128] : new byte[256];
            byte[] array3 = new byte[3];
            Array.Copy(array, flag ? 29 : 33, array2, 0, flag ? 128 : 256);
            Array.Copy(array, flag ? 159 : 291, array3, 0, 3);
            return new RSAParameters
            {
                Modulus = array2,
                Exponent = array3
            };
        }

        private string LoadCerContent(string strCerFilePath)
        {
            string result;
            try
            {
                string text = File.ReadAllText(strCerFilePath);
                result = text;
            }
            catch
            {
                result = null;
            }
            return result;
        }
    }
}
