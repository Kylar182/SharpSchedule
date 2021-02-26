using System;
using System.Linq;
using System.Windows.Input;
using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.ViewModels;

namespace SharpSchedule.Commands.AddressVMCommands
{
  public class SearchAddressesCommand : ICommand
  {
    private readonly AddressesVM _addressVM;
    private readonly CustomerVM _customerVM;

    public SearchAddressesCommand(AddressesVM addressVM)
    {
      _addressVM = addressVM;
    }

    public SearchAddressesCommand(CustomerVM customerVM)
    {
      _customerVM = customerVM;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter)
    {
      if (parameter is string street)
      {
        if (_addressVM != null)
        {
          _addressVM.Addresses.Clear();

          foreach (Address address in _addressVM.AllAddresses.Where(pr => pr.StreetAddress.Contains(street)))
            _addressVM.Addresses.Add(address);
        }
        else
        {
          _customerVM.Addresses.Clear();

          foreach (Address address in _customerVM.AllAddresses.Where(pr => pr.StreetAddress.Contains(street)))
            _customerVM.Addresses.Add(address);
        }
      }
    }
  }
}
