using System;
using System.Windows.Input;
using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.Data.Repositories;
using SharpSchedule.Data.Repositories.Location;
using SharpSchedule.Models;
using SharpSchedule.ViewModels;
using SharpSchedule.Views.Dialogs;

namespace SharpSchedule.Commands.AddressVMCommands
{
  public class NewCityCommand : ICommand
  {
    private readonly AddressesVM _addressVM;
    private readonly ICityRepository _cityRepository;
    private readonly IRepository<Country> _countryRepository;

    public NewCityCommand(AddressesVM addressVM, 
      ICityRepository cityRepository, 
      IRepository<Country> countryRepository)
    {
      _addressVM = addressVM;
      _cityRepository = cityRepository;
      _countryRepository = countryRepository;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      CityDialog dialog = new CityDialog();
      CityVM VM = new CityVM(_cityRepository, _countryRepository, CUD.Create, 
                              new Action(() => dialog.Close()));
      dialog.DataContext = VM;
      bool? result = dialog.ShowDialog();
      if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
        _addressVM.CityUpdate();
    }
  }
}
