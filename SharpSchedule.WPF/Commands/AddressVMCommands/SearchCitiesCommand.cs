using System;
using System.Linq;
using System.Windows.Input;
using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.ViewModels;

namespace SharpSchedule.Commands.AddressVMCommands
{
  public class SearchCitiesCommand : ICommand
  {
    private readonly AddressesVM _addressVM;

    public SearchCitiesCommand(AddressesVM addressVM)
    {
      _addressVM = addressVM;
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
        _addressVM.Cities.Clear();

        foreach (City city in _addressVM.AllCities.Where(pr => pr.Name.Contains((string)parameter)))
          _addressVM.Cities.Add(city);
      }
    }
  }
}
