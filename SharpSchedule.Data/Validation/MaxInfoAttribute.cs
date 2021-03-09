using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using SharpSchedule.Data.Extensions;

namespace SharpSchedule.Data.Validation
{
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
  public class MaxInfoAttribute : ValidationAttribute
  {
    private string Name { get; set; } = string.Empty;
    private string Info { get; set; }
    private int Length { get; set; } = 0;

    public MaxInfoAttribute(int length)
    {
      Length = length;
    }

    public MaxInfoAttribute(int length, string name)
    {
      Length = length;
      Name = name;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      if (Name.IsEmpty())
        Name = validationContext.MemberName.SplitPascalCase();

      PropertyInfo propertyName = validationContext.ObjectType.GetProperty(nameof(Info));
      if (propertyName == null)
        return new ValidationResult($"Unknown property {nameof(Info)}", new[] { validationContext.MemberName });

      Info = propertyName.GetValue(validationContext.ObjectInstance, null) as string;

      if (Info == null)
      {
        ErrorMessage = $"{nameof(Info)} is Required";
        return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
      }

      if (value is null) return ValidationResult.Success;

      if (value.ToString().IsEmpty()) return ValidationResult.Success;

      if (value.ToString().Length > Length)
      {
        if (Info == "en")
          ErrorMessage = $"Max Length {Length} Characters";
        return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
      }

      return ValidationResult.Success;
    }
  }
}
