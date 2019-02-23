using SqlSugar;

namespace CrmScrew.Model.Entity
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("crm_client_address")]
    public class CrmClientAddressEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "clientAddressId")]
        public int ClientAddressId {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "clientId")]
        public int ClientId {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName= "fullAddress")]
        public string FullAddress {get;set;}

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
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName= "address")]
        public string Address {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName= "provinceId")]
        public int? ProvinceId {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName= "provinceName")]
        public string ProvinceName {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName= "cityId")]
        public int? CityId {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName= "cityName")]
        public string CityName {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "areaId")]
        public int? AreaId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "areaName")]
        public string AreaName { get; set; }

    }
}
