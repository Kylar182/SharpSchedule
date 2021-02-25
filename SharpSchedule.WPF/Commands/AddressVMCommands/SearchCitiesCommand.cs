using System;
using System.Linq;
using System.Windows.Input;
using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.ViewModels;

namespace SharpSchedule.Commands.AddressVMCommands
{
  public class SearchCitiesCommand : ICommand
  {
    private readonly AddressesVM _addressesVM;
    private readonly AddressVM _addressVM;

    public SearchCitiesCommand(AddressesVM addressVM)
    {
      _addressesVM = addressVM;
    }

    public SearchCitiesCommand(AddressVM addresVM)
    {
      _addressVM = addresVM;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      if (parameter is string input)
      {
        if (_addressesVM != null)
        {
          _addressesVM.Cities.Clear();

          foreach (City city in _addressesVM.AllCities.Where(pr => pr.Name.Contains(input)))
            _addressesVM.Cities.Add(city);
        }
        else
        {
          _addressVM.Cities.Clear();

          foreach (City city in _addressVM.AllCities.Where(pr => pr.Name.Contains(input)))
            _addressVM.Cities.Add(city);
        }
      }
    }
  }
}
