using System;
using System.ComponentModel;
using System.Windows.Input;
using SharpSchedule.ViewModels;

namespace SharpSchedule.Commands.AddressVMCommands
{
  public class CityCRUDCommand : ICommand
  {
    private readonly CityVM _cityVM;

    public CityCRUDCommand(CityVM cityVM)
    {
      _cityVM = cityVM;

      _cityVM.PropertyChanged += ErrorsChanged;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return !_cityVM.HasErrors;
    }

    public async void Execute(object parameter)
    {
      if (!_cityVM.HasErrors)
      {
        try
        {
          await _cityVM.DBUpdate();
        }
        catch (Exception)
        {
          return;
        }
      }
    }

    private void ErrorsChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(_cityVM.HasErrors))
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
  }
}
