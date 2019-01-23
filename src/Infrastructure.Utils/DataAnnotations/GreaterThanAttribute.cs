/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 18:20:37
** desc：    GreaterThanAttribute类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class GreaterThanAttribute : ValidationAttribute
    {
        public double Value
        {
            get;
            set;
        }

        public override bool IsValid(object value)
        {
            double num = Convert.ToDouble(value);
            return num > this.Value;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            double num = Convert.ToDouble(value);
            if (num > this.Value)
            {
                return ValidationResult.Success;
            }
            if (string.IsNullOrEmpty(base.ErrorMessage))
            {
                base.ErrorMessage = validationContext.MemberName + " 属性值应该大于{0}";
            }
            return new ValidationResult(string.Format(base.ErrorMessage, this.Value));
        }
    }
}
