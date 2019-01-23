/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 18:23:14
** desc：    RequiredTimeAttribute类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class RequiredTimeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = (DateTime)value;
            return d == DateTime.MinValue || d == DateTime.MinValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(base.ErrorMessage);
            }
            DateTime d = (DateTime)value;
            if (d != DateTime.MinValue && d != DateTime.MinValue)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(base.ErrorMessage);
        }
    }
}
