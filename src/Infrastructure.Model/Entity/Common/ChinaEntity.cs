using SqlSugar;

namespace Infrastructure.Model.Entity.Common
{
    ///<summary>
    ///省市表
    ///</summary>
    [SugarTable("china")]
    public  class ChinaEntity
    {
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnName="id")]
           public int Id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           [SugarColumn(ColumnName="name")]
           public string Name {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           [SugarColumn(ColumnName= "pId")]
           public int PId { get;set;}

    }
}
