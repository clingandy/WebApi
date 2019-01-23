/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 15:21:05
** desc：    HttpContextItem类
** Ver.:     V1.0.0
*********************************************************************************/
using System.IO;
using System.Net;
using System.Text;

namespace Infrastructure.Common
{
    public class HttpHelper
    {
        public static string PostData(string url, string data)
        {
            return PostData(url, data, 60, out _);
        }

        public static string PostData(string url, string data, out int statusCode)
        {
            statusCode = 0;
            return PostData(url, data, 60, out statusCode);
        }

        public static string PostData(string url, string data, int timeOut, out int statusCode)
        {
            return PostData(url, data, 60, Encoding.GetEncoding("UTF-8"), out statusCode);
        }

        public static string PostData(string url, string data, Encoding code, out int statusCode)
        {
            return PostData(url, data, 60, code, out statusCode);
        }

        public static string PostData(string url, string data, int timeOut, Encoding code, out int statusCode)
        {
            string result = string.Empty;
            byte[] bytes = code.GetBytes(data);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.ContentLength = (long)data.Length;
            httpWebRequest.Timeout = timeOut * 1000;
            using (Stream requestStream = httpWebRequest.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
            }
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (Stream responseStream = httpWebResponse.GetResponseStream())
            {
                StreamReader streamReader = new StreamReader(responseStream, code);
                result = streamReader.ReadToEnd();
            }
            statusCode = (int)httpWebResponse.StatusCode;
            httpWebResponse.Close();
            return result;
        }
    }
}
