using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using SharpSchedule.Data.Extensions;

namespace SharpSchedule.Data.Validation
{
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
  public class RequiredInfoAttribute : ValidationAttribute
  {
    private string Name { get; set; } = string.Empty;
    private string Info { get; set; }

    public RequiredInfoAttribute() { }

    public RequiredInfoAttribute(string name)
    {
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

      if (value is null)
      {
        if (Info == "en")
          ErrorMessage = $"A {Name} is required";
        return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
      }

      if (value.ToString().IsEmpty())
      {
        if (Info == "en")
          ErrorMessage = $"A {Name} is required";
        return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
      }

      return ValidationResult.Success;
    }
  }
}
