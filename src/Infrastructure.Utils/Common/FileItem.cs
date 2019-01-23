/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 15:32:47
** desc：    FileItem类
** Ver.:     V1.0.0
*********************************************************************************/
using System;

namespace Infrastructure.Common
{
    [Serializable]
    public class FileItem
    {
        public string Name { get; set; }

        public string FullName{ get; set; }

        public string ExtName{ get; set; }

        public DateTime CreationDate{ get; set; }

        public bool IsFolder{ get; set; }

        public long Size{ get; set; }

        public DateTime LastAccessDate{ get; set; }

        public DateTime LastWriteDate{ get; set; }

        public int FileCount{ get; set; }

        public int SubFolderCount{ get; set; }
    }
}
