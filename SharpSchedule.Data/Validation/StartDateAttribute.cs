using System;
using System.ComponentModel.DataAnnotations;
using SharpSchedule.Data.Extensions;

namespace SharpSchedule.Data.Validation
{
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter,
    AllowMultiple = false)]
  public class StartDateAttribute : ValidationAttribute
  {
    public string Name { get; set; } = string.Empty;
    public string MaxName { get; set; } = string.Empty;
    private DateTime? max { get; set; }

    public StartDateAttribute(string maxName)
    {
      MaxName = maxName;
    }

    public StartDateAttribute(string name, string maxName)
    {
      Name = name;
      MaxName = maxName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      if (Name.IsEmpty())
        Name = validationContext.MemberName.SplitPascalCase();

      if (value is null)
      {
        ErrorMessage ??=
          $"{Name} is Required";
        return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
      }

      if (MaxName.IsEmpty())
        return new ValidationResult("Max Value Required", new[] { validationContext.MemberName });

      var propertyName = validationContext.ObjectType.GetProperty(MaxName);

      max = propertyName.GetValue(validationContext.ObjectInstance, null) as DateTime?;

      if (max == null)
        return new ValidationResult($"{MaxName.SplitPascalCase()} Required", new[] { validationContext.MemberName });

      if (value != null && DateTime.TryParse(value.ToString(), out DateTime val))
      {
        if (val < max)
        {
          ErrorMessage ??=
            $"{Name} must be less than {MaxName.SplitPascalCase()}";
          return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
        }
        else
          return ValidationResult.Success;
      }
      else
        ErrorMessage ??=
          $"{Name} is Required";
      return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
    }
  }
}
