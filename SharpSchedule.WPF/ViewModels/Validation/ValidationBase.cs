using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SharpSchedule.ViewModels.Validation
{
  public class ValidationBase : ViewModelBase, INotifyDataErrorInfo
  {
    protected Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

    public ValidationBase()
    {
      ErrorsChanged += ValidationBase_ErrorsChanged;
    }

    private void ValidationBase_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
    {
      OnPropChanged(nameof(HasErrors));
      OnPropChanged(nameof(ErrorsList));
    }

    #region INotifyDataErrorInfo Members

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    public IEnumerable GetErrors(string propertyName)
    {
      if (!string.IsNullOrEmpty(propertyName))
      {
        if (_errors.ContainsKey(propertyName) && (_errors[propertyName].Any()))
          return _errors[propertyName].ToList();
        else
          return new List<string>();
      }
      else
        return _errors.SelectMany(err => err.Value.ToList()).ToList();
    }

    public bool PropertyHasErrors(string propertyName)
    {
      if (!string.IsNullOrEmpty(propertyName))
      {
        if (_errors.ContainsKey(propertyName) && (_errors[propertyName].Any()))
          return true;
        else
          return false;
      }
      else
        return false;
    }

    public bool HasErrors => _errors.Any(propErrors => propErrors.Value.Any());

    #endregion

    protected virtual void ValidateProp(object value, [CallerMemberName] string propertyName = null)
    {
      ValidationContext context = new ValidationContext(this, null)
      {
        MemberName = propertyName
      };

      List<ValidationResult> results = new List<ValidationResult>();
      Validator.TryValidateProperty(value, context, results);

      RemoveErrorsByPropName(propertyName);

      HandleValidationResults(results);
    }

    private void RemoveErrorsByPropName(string propertyName)
    {
      if (_errors.ContainsKey(propertyName))
        _errors.Remove(propertyName);

      RaiseErrorsChanged(propertyName);
    }

    private void HandleValidationResults(List<ValidationResult> validationResults)
    {
      var resultsByPropertyName = from results in validationResults
                                  from memberNames in results.MemberNames
                                  group results by memberNames into groups
                                  select groups;

      foreach (var property in resultsByPropertyName)
      {
        _errors.Add(property.Key, property.Select(r => r.ErrorMessage).ToList());
        RaiseErrorsChanged(property.Key);
      }
    }

    private void RaiseErrorsChanged(string propertyName)
    {
      ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    public IList<string> ErrorsList => GetErrors(string.Empty).Cast<string>().ToList();
  }
}
