using System;
using System.ComponentModel.DataAnnotations;
using SharpSchedule.Data.Extensions;

namespace SharpSchedule.Data.Validation
{
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter,
    AllowMultiple = false)]
  public class EndDateAttribute : ValidationAttribute
  {
    public string Name { get; set; } = string.Empty;
    public string MinName { get; set; } = string.Empty;
    private DateTime? min { get; set; }

    public EndDateAttribute(string minName)
    {
      MinName = minName;
    }

    public EndDateAttribute(string name, string minName)
    {
      Name = name.SplitPascalCase();
      MinName = minName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      if (Name.IsEmpty())
        Name = validationContext.MemberName.SplitPascalCase();

      if (value is null)
      {
        ErrorMessage ??= $"{Name} is Required";
        return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
      }

      if (MinName.IsEmpty())
        return new ValidationResult("Min Value Required", new[] { validationContext.MemberName });

      var propertyName = validationContext.ObjectType.GetProperty(MinName);

      min = propertyName.GetValue(validationContext.ObjectInstance, null) as DateTime?;

      if (min == null)
        return new ValidationResult($"{MinName.SplitPascalCase()} Required", new[] { validationContext.MemberName });

      if (value != null && DateTime.TryParse(value.ToString(), out DateTime val))
      {
        if (val < min)
        {
          ErrorMessage ??= $"{Name} must surpass {MinName.SplitPascalCase()}";
          return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
        }
        else
          return ValidationResult.Success;
      }
      else
        ErrorMessage ??= $"{Name} is Required";
      return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
    }
  }
}
