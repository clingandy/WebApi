/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 18:21:55
** desc：    MinLengthAttribute类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MinLengthAttribute : ValidationAttribute
    {
        public int MinLength
        {
            get;
            set;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (value is ICollection)
            {
                return ((ICollection)value).Count < this.MinLength;
            }
            return base.IsValid(value);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(base.ErrorMessage);
            }
            if (value is ICollection && ((ICollection)value).Count >= this.MinLength)
            {
                return ValidationResult.Success;
            }
            if (value is Array && ((Array)value).Length >= this.MinLength)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(base.ErrorMessage);
        }
    }
}
