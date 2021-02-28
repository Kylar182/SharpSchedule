using System;
using System.ComponentModel;
using System.Windows.Input;
using SharpSchedule.ViewModels.DialogViewModels;

namespace SharpSchedule.Commands.CustomersVMCommands
{
  public class CustomerCRUDCommand : ICommand
  {
    private readonly CustomerVM _customerVM;

    public CustomerCRUDCommand(CustomerVM customerVM)
    {
      _customerVM = customerVM;

      _customerVM.PropertyChanged += ErrorsChanged;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return !_customerVM.HasErrors;
    }

    public async void Execute(object parameter)
    {
      if (!_customerVM.HasErrors)
      {
        try
        {
          await _customerVM.DBUpdate().ConfigureAwait(true);
        }
        catch (Exception)
        {
          return;
        }
      }
    }

    private void ErrorsChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(_customerVM.HasErrors))
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
  }
}
