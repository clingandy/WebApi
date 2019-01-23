/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 14:54:14
** desc：    DataMaskingAttribute类
** Ver.:     V1.0.0
*********************************************************************************/
using System;

namespace Infrastructure.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class DataMaskingAttribute : Attribute
    {
        public enum DataMaskingType
        {
            Mobile = 1,
            Email,
            BankAccount
        }

        public DataMaskingType MaskType
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Table转Entity
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class TabaleColNameAttribute : Attribute
    {
        public string TabaleColName { get; set; }

        public TabaleColNameAttribute(string tabaleColName)
        {
            TabaleColName = tabaleColName;
        }
    }
}
