using SqlSugar;

namespace CrmScrew.Model.Entity
{
    ///<summary>
    ///字典表
    ///</summary>
    [SugarTable("crm_dict")]
    public class CrmDictEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "dictId")]
        public int DictId {get;set;}

        /// <summary>
        /// Desc:类型多个的需要分类（对应枚举）
        /// Default:0
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "dictType")]
        public int DictType {get;set;}

        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "dictKey")]
        public int DictKey {get;set;}

        /// <summary>
        /// Desc: 父级KEY
        /// Default:0
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "pDictKey")]
        public int PDictKey { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "dictValue")]
        public string DictValue {get;set;}

        /// <summary>
        /// Desc:排序
        /// Default:0
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "sort")]
        public int Sort {get;set;}

        /// <summary>
        /// Desc:
        /// Default:b'1'
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "status")]
        public bool Status {get;set;}

    }
}
