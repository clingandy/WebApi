using System;
using SqlSugar;

namespace CrmScrew.Model.Entity
{
    ///<summary>
    ///客户表
    ///</summary>
    [SugarTable("crm_client")]
    public class CrmClientEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "clientId")]
        public int ClientId {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "clientName")]
        public string ClientName {get;set;}

        /// <summary>
        /// Desc:1男0女
        /// Default:b'1'
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "sex")]
        public bool Sex {get;set;}

        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "age")]
        public int Age {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "weChat")]
        public string WeChat {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "qq")]
        public string QQ {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "mobile")]
        public string Mobile {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "modifyTime")]
        public DateTime ModifyTime {get;set;}

    }
}
