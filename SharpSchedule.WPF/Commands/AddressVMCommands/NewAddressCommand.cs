using System;
using System.Collections.Generic;
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
  public class NewAddressCommand : CommandBase
  {
    private readonly AddressesVM _addressVM;
    private readonly IAddressRepository _repository;
    private readonly ICityRepository _cityRepository;
    private readonly User _user;

    public NewAddressCommand(AddressesVM addressVM,
      IAddressRepository repository,
      ICityRepository cityRepository,
      User user)
    {
      _addressVM = addressVM;
      _repository = repository;
      _cityRepository = cityRepository;
      _user = user;
    }

    protected override async Task ExecuteAsync(object parameter)
    {
      AddressDialog dialog = new AddressDialog();

      List<City> allCities = await _cityRepository.GetAll().ConfigureAwait(true);

      AddressVM VM = new AddressVM(_repository, allCities, CUD.Create,
                              new Action(() => dialog.Close()), _user);
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
}
