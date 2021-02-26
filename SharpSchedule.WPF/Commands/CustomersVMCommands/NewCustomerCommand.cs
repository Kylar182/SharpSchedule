using System;
using System.Windows.Input;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.Repositories.Location;
using SharpSchedule.Data.Repositories.Scheduling;
using SharpSchedule.Models;
using SharpSchedule.ViewModels;
using SharpSchedule.Views.Dialogs;

namespace SharpSchedule.Commands.CustomersVMCommands
{
  public class NewCustomerCommand : ICommand
  {
    private readonly CustomersVM _customerVM;
    private readonly ICustomerRepository _repository;
    private readonly IAddressRepository _addressRepository;
    private readonly User _user;

    public NewCustomerCommand(CustomersVM customerVM,
      ICustomerRepository repository,
      IAddressRepository addressRepository,
      User user)
    {
      _customerVM = customerVM;
      _repository = repository;
      _addressRepository = addressRepository;
      _user = user;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      CustomerDialog dialog = new CustomerDialog();
      CustomerVM VM = new CustomerVM(_repository, _addressRepository, CUD.Create,
                              new Action(() => dialog.Close()), _user);
      dialog.DataContext = VM;
      bool? result = dialog.ShowDialog();

      if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
      {
        _customerVM.Load().ConfigureAwait(true);

        _customerVM.SearchCustomers.Execute(string.Empty);
      }
    }
  }
}
