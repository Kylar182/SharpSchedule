using System;
using System.ComponentModel.DataAnnotations;
using SharpSchedule.Data.Extensions;

namespace SharpSchedule.Data.Validation
{
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
  public class TimeConflictAttribute : ValidationAttribute
  {
    private string Name { get; set; } = string.Empty;
    private bool? Conflict { get; set; }

    public TimeConflictAttribute() { }

    public TimeConflictAttribute(string name)
    {
      Name = name;
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

      var propertyName = validationContext.ObjectType.GetProperty(nameof(Conflict));
      if (propertyName == null)
        return new ValidationResult($"Unknown property {nameof(Conflict)}", new[] { validationContext.MemberName });

      Conflict = propertyName.GetValue(validationContext.ObjectInstance, null) as bool?;

      if (Conflict == null)
      {
        ErrorMessage ??= $"{nameof(Conflict)} is Required";
        return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
      }

      if (Conflict == true)
      {
        ErrorMessage ??= $"{nameof(Conflict)} with other Appointment Time";
        return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
      }

      return ValidationResult.Success;
    }
  }
}
