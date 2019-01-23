/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2019/1/3 15:02:06
** desc：    IDictRepository类
** Ver.:     V1.0.0
*********************************************************************************/
using System.Collections.Generic;
using CrmScrew.Model.Entity;
using CrmScrew.Model.Search;

namespace CrmScrew.Domain
{
    public interface IDictRepository
    {
        /// <summary>
        /// 查询字典信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<CrmDictEntity> GetDictPageList(CrmDictSearch search, ref int totalCount, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 查询全部字典信息 状态为1
        /// </summary>
        /// <returns></returns>
        IList<CrmDictEntity> GetDicList();

        /// <summary>
        /// 获取字典类型
        /// </summary>
        /// <returns></returns>
        Dictionary<int, string> GetDictType();

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        bool AddDict(CrmDictEntity dict);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        bool ModifyDict(CrmDictEntity dict);
    }
}
