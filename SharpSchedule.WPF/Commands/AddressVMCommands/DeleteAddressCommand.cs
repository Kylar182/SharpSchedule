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
  public class DeleteAddressCommand : ICommand
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

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return _addressVM.AddressSelected != null;
    }

    public void Execute(object parameter)
    {
      if (_addressVM.AddressSelected != null)
      {
        AddressDialog dialog = new AddressDialog();
        AddressVM VM = new AddressVM(_repository, _cityRepository, CUD.Delete,
                                new Action(() => dialog.Close()), _user, _addressVM.AddressSelected);
        dialog.DataContext = VM;
        bool? result = dialog.ShowDialog();

        if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
        {
          _addressVM.AddressUpdate();

          _addressVM.SearchAddresses.Execute(string.Empty);
        }

        _addressVM.AddressSelected = null;
      }
    }

    private void AddressChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(_addressVM.AddressSelected))
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
  }
}
