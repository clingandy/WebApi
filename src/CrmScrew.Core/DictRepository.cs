/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2019/1/3 15:01:32
** desc：    DictRepository类
** Ver.:     V1.0.0
*********************************************************************************/

using System.Collections.Generic;
using CrmScrew.Domain;
using CrmScrew.Model.Entity;
using CrmScrew.Model.Enum;
using CrmScrew.Model.Search;
using CrmScrew.Service;
using Infrastructure.Common;

namespace CrmScrew.Core
{
    public class DictRepository : IDictRepository
    {
        #region 属性

        private readonly DictService _service = DictService.Instance;

        #endregion

        #region 基本操作

        /// <summary>
        /// 查询字典信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<CrmDictEntity> GetDictPageList(CrmDictSearch search, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return _service.FindDictPageList(search, ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 查询全部字典信息 状态为1
        /// </summary>
        /// <returns></returns>
        public IList<CrmDictEntity> GetDicList()
        {
            return _service.FindByFunc<CrmDictEntity>(t => t.Status);
        }

        /// <summary>
        /// 获取字典类型
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetDictType()
        {
            return typeof(CrmDictTypeEnum).ToDictionary();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public bool AddDict(CrmDictEntity dict)
        {
            var count = _service.FindCountByFunc<CrmDictEntity>(t => t.DictType == dict.DictType && t.DictKey == dict.DictKey);
            if (count > 0)
            {
                return false;
            }
            return _service.Insert(dict);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public bool ModifyDict(CrmDictEntity dict)
        {
            return _service.Update(dict);
        }

        #endregion
    }
}
