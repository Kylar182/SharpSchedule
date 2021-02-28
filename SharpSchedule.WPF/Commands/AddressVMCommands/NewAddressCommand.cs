using System;
using System.Windows.Input;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.Repositories.Location;
using SharpSchedule.Models;
using SharpSchedule.ViewModels;
using SharpSchedule.ViewModels.DialogViewModels;
using SharpSchedule.Views.Dialogs;

namespace SharpSchedule.Commands.AddressVMCommands
{
  public class NewAddressCommand : ICommand
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

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      AddressDialog dialog = new AddressDialog();
      AddressVM VM = new AddressVM(_repository, _cityRepository, CUD.Create,
                              new Action(() => dialog.Close()), _user);
      dialog.DataContext = VM;
      bool? result = dialog.ShowDialog();

      if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
      {
        _addressVM.AddressUpdate().ConfigureAwait(true);

        _addressVM.SearchAddresses.Execute(string.Empty);
      }
    }
  }
}
