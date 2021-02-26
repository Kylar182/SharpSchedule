using System;
using System.Windows.Input;
using SharpSchedule.Data.EntityModels;
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
    private readonly User _user;

    public NewCityCommand(AddressesVM addressVM,
      ICityRepository cityRepository,
      IRepository<Country> countryRepository,
      User user)
    {
      _addressVM = addressVM;
      _cityRepository = cityRepository;
      _countryRepository = countryRepository;
      _user = user;
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
                              new Action(() => dialog.Close()), _user);
      dialog.DataContext = VM;
      bool? result = dialog.ShowDialog();

      if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
      {
        _addressVM.CityUpdate().ConfigureAwait(true);

        _addressVM.SearchCities.Execute(string.Empty);
      }
    }
  }
}
