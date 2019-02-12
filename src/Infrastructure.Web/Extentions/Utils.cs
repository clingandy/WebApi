using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Web.Extentions
{
    public class Utils
    {
        #region 上传

        /// <summary>
        /// 上传文件 限制10M
        /// </summary>
        /// <param name="context"></param>
        /// <param name="extList">扩展文件类型</param>
        /// <param name="fileName">上传后的文件名称(后面会加时间)</param>
        /// <param name="path">文件路径</param>
        /// <returns>全路径</returns>
        public static string FileSave(HttpContext context,string extList= "xlsx|jpg|png", string fileName = "import", string path = "upload")
        {
            var files = context.Request.Form.Files;
            //long size = files.Sum(f => f.Length);
            string webRootPath = Infrastructure.Common.IOUtils.GetBaseDirectory();
            foreach (var formFile in files)
            {
                if (formFile.Length <= 0) continue;
                string fileExt = GetFileExt(formFile.FileName); //文件扩展名，不含“.”
                if (!extList.Contains(fileExt)) return "";
                long fileSize = formFile.Length; //获得文件大小，以字节为单位
                if (10 * 1024 * 1024 < fileSize)
                {
                    return "";  //限制10M
                }
                string newFileName = fileName+ DateTime.Now.ToString("_MMdd_HHmmss") + "." + fileExt;
                var savePath = Path.Combine(webRootPath, path, DateTime.Now.ToString("yyyyMM"));  //路径拼接必须使用Path.Combine防止不同操作系统问题
                var filePath = Path.Combine(savePath, newFileName);
                try
                {
                    Common.IOUtils.CreateDirectory(savePath);  //没有创建路径
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                    return filePath;
                }
                catch (Exception)
                {
                    return "";
                }
                
            }
            return "";
        }

        /// <summary>
        /// 取后缀名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>.gif|.html格式</returns>
        public static string GetFileExt(string filename)
        {
            int start = filename.LastIndexOf(".", StringComparison.Ordinal);
            if (start == -1) return "";
            int length = filename.Length;
            string postfix = filename.Substring(start, length - start);
            return postfix.Replace(".", "");
        }

        #endregion
    }
}
