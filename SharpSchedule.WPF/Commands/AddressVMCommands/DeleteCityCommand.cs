using System;
using System.ComponentModel;
using System.Threading.Tasks;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.Data.Repositories;
using SharpSchedule.Data.Repositories.Location;
using SharpSchedule.Models;
using SharpSchedule.ViewModels;
using SharpSchedule.ViewModels.DialogViewModels;
using SharpSchedule.Views.Dialogs;

namespace SharpSchedule.Commands.AddressVMCommands
{
  public class DeleteCityCommand : CommandBase
  {
    private readonly AddressesVM _addressVM;
    private readonly ICityRepository _cityRepository;
    private readonly IRepository<Country> _countryRepository;
    private readonly User _user;

    public DeleteCityCommand(AddressesVM addressVM,
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

    public override event EventHandler CanExecuteChanged;

    public override bool CanExecute(object parameter)
    {
      return _addressVM.CitySelected != null;
    }

    protected override async Task ExecuteAsync(object parameter)
    {
      if (_addressVM.CitySelected != null)
      {
        CityDialog dialog = new CityDialog();
        CityVM VM = new CityVM(_cityRepository, _countryRepository, CUD.Delete,
                                new Action(() => dialog.Close()), _user, _addressVM.CitySelected);
        dialog.DataContext = VM;
        bool? result = dialog.ShowDialog();

        if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
        {
          _addressVM.AllCities = await _addressVM.GetAllCities().ConfigureAwait(true);

          _addressVM.Cities.Clear();

          foreach (City city in _addressVM.AllCities)
            _addressVM.Cities.Add(city);
        }
      }
    }

    private void CityChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(_addressVM.CitySelected))
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
  }
}
