using System;
using System.Linq;
using System.Windows.Input;
using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.ViewModels.DialogViewModels;

namespace SharpSchedule.Commands.AddressVMCommands
{
  public class SearchCountriesCommand : ICommand
  {
    private readonly CityVM _vm;

    public SearchCountriesCommand(CityVM vm)
    {
      _vm = vm;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      if (parameter is string)
      {
        _vm.Countries.Clear();

        foreach (Country country in _vm.AllCountries.Where(pr => pr.Name.Contains((string)parameter)))
          _vm.Countries.Add(country);
      }
    }
  }
}
