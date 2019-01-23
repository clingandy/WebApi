/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2019/1/3 15:03:36
** desc：    DictService类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using CrmScrew.Model.Entity;
using CrmScrew.Model.Search;
using CrmScrew.Service.Base;
using Infrastructure.Common;
using SqlSugar;

namespace CrmScrew.Service
{
    public class DictService : SqlSugarBase
    {
        #region 构造函数、单列

        private DictService() { }

        public static readonly DictService Instance = new Lazy<DictService>(() => new DictService()).Value;

        #endregion

        #region 字典

        /// <summary>
        /// 查询字典信息
        /// </summary>
        /// <param name="search">mobile</param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<CrmDictEntity> FindDictPageList(CrmDictSearch search, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return GetInstance().Queryable<CrmDictEntity>()
                .WhereIF(!search.DictValue.IsNullOrWhiteSpace(), t => t.DictValue.Contains(search.DictValue.Trim()))
                .WhereIF(search.DictType != null, t => t.DictType == search.DictType)
                .WhereIF(search.PDictKey != null, t => t.PDictKey == search.PDictKey)
                .OrderBy(t => t.DictId, OrderByType.Desc)
                .ToPageList(pageIndex, pageSize, ref totalCount).ToList();
        }

        #endregion

    }
}
