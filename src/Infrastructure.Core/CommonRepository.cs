/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2019/1/9 14:12:00
** desc：    CommonRepository类
** Ver.:     V1.0.0
*********************************************************************************/

using System.Collections.Generic;
using Infrastructure.Domain;
using Infrastructure.Model.Entity.Common;
using Infrastructure.Service;

namespace Infrastructure.Core
{
    public class CommonRepository : ICommonRepository
    {
        #region 属性

        private readonly CommonService _service = CommonService.Instance;

        #endregion

        #region 省市

        /// <summary>
        /// 获取省市地区全部信息
        /// </summary>
        /// <returns></returns>
        public IList<ChinaEntity> GetChinaCityList()
        {
            return _service.FindByFunc<ChinaEntity>(t => t.Id != 0);
        }

        #endregion
    }
}
