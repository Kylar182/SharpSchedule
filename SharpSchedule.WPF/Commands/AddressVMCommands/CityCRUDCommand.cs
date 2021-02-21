using System;
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
  }
}
