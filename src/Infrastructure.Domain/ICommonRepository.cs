/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2019/01/19 10:21:02
** desc：    ICommonRepository类
** Ver.:     V1.0.0
*********************************************************************************/

using System.Collections.Generic;
using Infrastructure.Model.Entity.Common;

namespace Infrastructure.Domain
{
    public interface ICommonRepository
    {
        #region 省市

        /// <summary>
        /// 获取省市地区全部信息
        /// </summary>
        /// <returns></returns>
        IList<ChinaEntity> GetChinaCityList();

        #endregion

    }
}
