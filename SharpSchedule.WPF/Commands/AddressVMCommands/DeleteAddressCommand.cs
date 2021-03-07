using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.Data.Repositories.Location;
using SharpSchedule.Models;
using SharpSchedule.ViewModels;
using SharpSchedule.ViewModels.DialogViewModels;
using SharpSchedule.Views.Dialogs;

namespace SharpSchedule.Commands.AddressVMCommands
{
  public class DeleteAddressCommand : CommandBase
  {
    private readonly AddressesVM _addressVM;
    private readonly IAddressRepository _repository;
    private readonly ICityRepository _cityRepository;
    private readonly User _user;

    public DeleteAddressCommand(AddressesVM addressVM,
      IAddressRepository repository,
      ICityRepository cityRepository,
      User user)
    {
      _addressVM = addressVM;
      _repository = repository;
      _cityRepository = cityRepository;
      _user = user;

      _addressVM.PropertyChanged += AddressChanged;
    }

    public override event EventHandler CanExecuteChanged;

    public override bool CanExecute(object parameter)
    {
      return _addressVM.AddressSelected != null;
    }

    protected override async Task ExecuteAsync(object parameter)
    {
      if (_addressVM.AddressSelected != null)
      {
        AddressDialog dialog = new AddressDialog();

        List<City> allCities = await _cityRepository.GetAll().ConfigureAwait(true);

        AddressVM VM = new AddressVM(_repository, allCities, CUD.Delete,
                                new Action(() => dialog.Close()), _user, _addressVM.AddressSelected);

        dialog.DataContext = VM;
        bool? result = dialog.ShowDialog();

        if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
        {
          _addressVM.AllAddresses = await _addressVM.GetAllAddresses().ConfigureAwait(true);

          _addressVM.Addresses.Clear();

          foreach (Address address in _addressVM.AllAddresses)
            _addressVM.Addresses.Add(address);
        }
      }
    }

    private void AddressChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(_addressVM.AddressSelected))
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
  }
}
