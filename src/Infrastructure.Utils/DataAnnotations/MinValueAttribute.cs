/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 18:22:35
** desc：    MinValueAttribute类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MinValueAttribute : ValidationAttribute
    {
        public double Value
        {
            get;
            set;
        }

        public override bool IsValid(object value)
        {
            double num = Convert.ToDouble(value);
            return num >= this.Value;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            double num = Convert.ToDouble(value);
            if (num >= this.Value)
            {
                return ValidationResult.Success;
            }
            if (string.IsNullOrEmpty(base.ErrorMessage))
            {
                base.ErrorMessage = validationContext.MemberName + "属性值不能小于{0}";
            }
            return new ValidationResult(string.Format(base.ErrorMessage, this.Value));
        }
    }
}
