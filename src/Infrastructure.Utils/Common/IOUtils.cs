/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 15:32:09
** desc：    IOUtils类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Infrastructure.Common
{
    public class IOUtils
    {
        private static string _strRootFolder;

        public static string GetRootPath()
        {
            return _strRootFolder;
        }

        public static void SetRootPath(string path)
        {
            _strRootFolder = path;
        }

        public static List<FileItem> GetDirectoryItems()
        {
            return GetDirectoryItems(_strRootFolder);
        }

        public static List<FileItem> GetDirectoryItems(string path)
        {
            if (!Directory.Exists(path))
            {
                return new List<FileItem>();
            }
            List<FileItem> list = new List<FileItem>();
            string[] directories = Directory.GetDirectories(path);
            string[] array = directories;
            for (int i = 0; i < array.Length; i++)
            {
                string path2 = array[i];
                FileItem fileItem = new FileItem();
                DirectoryInfo directoryInfo = new DirectoryInfo(path2);
                fileItem.Name = directoryInfo.Name;
                fileItem.FullName = directoryInfo.FullName;
                fileItem.CreationDate = directoryInfo.CreationTime;
                fileItem.IsFolder = true;
                list.Add(fileItem);
            }
            return list;
        }

        public static bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static List<FileItem> GetFileItems()
        {
            return GetFileItems(_strRootFolder);
        }

        public static List<FileItem> GetFileItems(string path)
        {
            List<FileItem> list = new List<FileItem>();
            string[] files = Directory.GetFiles(path);
            string[] array = files;
            for (int i = 0; i < array.Length; i++)
            {
                string text = array[i];
                FileItem fileItem = new FileItem();
                FileInfo fileInfo = new FileInfo(text);
                fileItem.Name = fileInfo.Name;
                fileItem.FullName = fileInfo.FullName;
                fileItem.CreationDate = fileInfo.CreationTime;
                fileItem.IsFolder = false;
                fileItem.Size = fileInfo.Length;
                fileItem.ExtName = Path.GetExtension(text);
                list.Add(fileItem);
            }
            return list;
        }

        public static void CreateFile(string filename, string path)
        {
            FileStream fileStream = File.Create(Path.Combine(path, filename));
            fileStream.Close();
        }

        public static void CreateFile(string filename, string path, byte[] contents)
        {
            FileStream fileStream = File.Create(Path.Combine(path, filename));
            fileStream.Write(contents, 0, contents.Length);
            fileStream.Close();
        }

        public static string Read(string parentName)
        {
            StreamReader streamReader = File.OpenText(parentName);
            StringBuilder stringBuilder = new StringBuilder();
            string value;
            while ((value = streamReader.ReadLine()) != null)
            {
                stringBuilder.Append(value);
            }
            streamReader.Close();
            return stringBuilder.ToString();
        }

        public static FileItem GetItemInfo(string path)
        {
            FileItem fileItem = new FileItem();
            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                fileItem.Name = directoryInfo.Name;
                fileItem.FullName = directoryInfo.FullName;
                fileItem.CreationDate = directoryInfo.CreationTime;
                fileItem.IsFolder = true;
                fileItem.LastAccessDate = directoryInfo.LastAccessTime;
                fileItem.LastWriteDate = directoryInfo.LastWriteTime;
                fileItem.FileCount = directoryInfo.GetFiles().Length;
                fileItem.SubFolderCount = directoryInfo.GetDirectories().Length;
            }
            else
            {
                FileInfo fileInfo = new FileInfo(path);
                fileItem.Name = fileInfo.Name;
                fileItem.FullName = fileInfo.FullName;
                fileItem.CreationDate = fileInfo.CreationTime;
                fileItem.LastAccessDate = fileInfo.LastAccessTime;
                fileItem.LastWriteDate = fileInfo.LastWriteTime;
                fileItem.IsFolder = false;
                fileItem.Size = fileInfo.Length;
            }
            return fileItem;
        }

        public static void WriteAllText(string parentName, string contents)
        {
            File.WriteAllText(parentName, contents, Encoding.Unicode);
        }

        public static void CopyFile(string sourceFileName, string destFileName)
        {
            File.Copy(sourceFileName, destFileName);
        }

        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public static void MoveFile(string oldPath, string newPath)
        {
            File.Move(oldPath, newPath);
        }

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void CreateFolder(string name, string parentName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(parentName);
            directoryInfo.CreateSubdirectory(name);
        }

        public static void DeleteFolder(string path)
        {
            Directory.Delete(path);
        }

        public static void MoveFolder(string oldPath, string newPath)
        {
            Directory.Move(oldPath, newPath);
        }

        public static bool CopyFolder(string source, string destination)
        {
            bool result;
            try
            {
                if (destination[destination.Length - 1] != Path.DirectorySeparatorChar)
                {
                    destination += Path.DirectorySeparatorChar;
                }
                if (!Directory.Exists(destination))
                {
                    Directory.CreateDirectory(destination);
                }
                string[] fileSystemEntries = Directory.GetFileSystemEntries(source);
                string[] array = fileSystemEntries;
                for (int i = 0; i < array.Length; i++)
                {
                    string text = array[i];
                    if (Directory.Exists(text))
                    {
                        CopyFolder(text, destination + Path.GetFileName(text));
                    }
                    else
                    {
                        File.Copy(text, destination + Path.GetFileName(text), true);
                    }
                }
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static bool IsSafeName(string strExtension)
        {
            strExtension = strExtension.ToLower();
            if (strExtension.LastIndexOf(".", StringComparison.Ordinal) >= 0)
            {
                strExtension = strExtension.Substring(strExtension.LastIndexOf(".", StringComparison.Ordinal));
            }
            else
            {
                strExtension = ".txt";
            }
            string[] array = new string[]
            {
                ".htm",
                ".html",
                ".txt",
                ".js",
                ".css",
                ".xml",
                ".sitemap",
                ".jpg",
                ".gif",
                ".png",
                ".rar",
                ".zip"
            };

            return array.Contains(strExtension);
        }

        public static bool IsUnsafeName(string strExtension)
        {
            strExtension = strExtension.ToLower();
            if (strExtension.LastIndexOf(".", StringComparison.Ordinal) >= 0)
            {
                strExtension = strExtension.Substring(strExtension.LastIndexOf(".", StringComparison.Ordinal));
            }
            else
            {
                strExtension = ".txt";
            }
            string[] array = new string[]
            {
                ".",
                ".asp",
                ".aspx",
                ".cs",
                ".net",
                ".dll",
                ".config",
                ".ascx",
                ".master",
                ".asmx",
                ".asax",
                ".cd",
                ".browser",
                ".rpt",
                ".ashx",
                ".xsd",
                ".mdf",
                ".resx",
                ".xsd"
            };
            return array.Contains(strExtension);
        }

        public static bool IsCanEdit(string strExtension)
        {
            strExtension = strExtension.ToLower();
            if (strExtension.LastIndexOf(".", StringComparison.Ordinal) >= 0)
            {
                strExtension = strExtension.Substring(strExtension.LastIndexOf(".", StringComparison.Ordinal));
            }
            else
            {
                strExtension = ".txt";
            }
            string[] array = new string[]
            {
                ".htm",
                ".html",
                ".txt",
                ".js",
                ".css",
                ".xml",
                ".sitemap"
            };
            return array.Contains(strExtension);
        }

        public static string GetBaseDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
