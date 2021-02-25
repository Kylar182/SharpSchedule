using System;
using System.ComponentModel;
using System.Windows.Input;
using SharpSchedule.ViewModels;

namespace SharpSchedule.Commands.AddressVMCommands
{
  public class AddressCRUDCommand : ICommand
  {
    private readonly AddressVM _addressVM;

    public AddressCRUDCommand(AddressVM addressVM)
    {
      _addressVM = addressVM;

      _addressVM.PropertyChanged += ErrorsChanged;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return !_addressVM.HasErrors;
    }

    public async void Execute(object parameter)
    {
      if (!_addressVM.HasErrors)
      {
        try
        {
          await _addressVM.DBUpdate().ConfigureAwait(true);
        }
        catch (Exception)
        {
          return;
        }
      }
    }

    private void ErrorsChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(_addressVM.HasErrors))
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
  }
}
