/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/12/28 11:34:05
** desc：    Config类
** Ver.:     V1.0.0
*********************************************************************************/

using Infrastructure.Common;

namespace CrmScrew.Service.Base
{
    public class DbConfig
    {
        public static string ConnectionString = AppSettingsHelper.GetString("CrmScrewDbConnectionString", "");
    }
}
