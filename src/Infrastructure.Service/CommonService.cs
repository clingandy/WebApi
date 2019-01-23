/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2019/1/9 14:13:45
** desc：    CommonService类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using Infrastructure.Service.Base;

namespace Infrastructure.Service
{
    public class CommonService : SqlSugarBase
    {
        #region 构造函数、单列

        private CommonService() { }

        public static readonly CommonService Instance = new Lazy<CommonService>(() => new CommonService()).Value;

        #endregion
    }
}
