using System;
using System.ComponentModel;
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
  public class UpdateCityCommand : ICommand
  {
    private readonly AddressesVM _addressVM;
    private readonly ICityRepository _cityRepository;
    private readonly IRepository<Country> _countryRepository;
    private readonly User _user;

    public UpdateCityCommand(AddressesVM addressVM,
      ICityRepository cityRepository,
      IRepository<Country> countryRepository,
      User user)
    {
      _addressVM = addressVM;
      _cityRepository = cityRepository;
      _countryRepository = countryRepository;
      _user = user;

      _addressVM.PropertyChanged += CityChanged;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return _addressVM.CitySelected != null;
    }

    public void Execute(object parameter)
    {
      if (_addressVM.CitySelected != null)
      {
        CityDialog dialog = new CityDialog();
        CityVM VM = new CityVM(_cityRepository, _countryRepository, CUD.Update,
                                new Action(() => dialog.Close()), _user, _addressVM.CitySelected);
        dialog.DataContext = VM;
        bool? result = dialog.ShowDialog();

        if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
        {
          _addressVM.CityUpdate();

          _addressVM.SearchCities.Execute(string.Empty);
        }

        _addressVM.CitySelected = null;
      }
    }

    private void CityChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(_addressVM.CitySelected))
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
  }
}
