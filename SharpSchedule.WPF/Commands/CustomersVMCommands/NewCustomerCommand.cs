using System;
using System.Threading.Tasks;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.EntityModels.Scheduling;
using SharpSchedule.Data.Repositories.Location;
using SharpSchedule.Data.Repositories.Scheduling;
using SharpSchedule.Models;
using SharpSchedule.ViewModels;
using SharpSchedule.ViewModels.DialogViewModels;
using SharpSchedule.Views.Dialogs;

namespace SharpSchedule.Commands.CustomersVMCommands
{
  public class NewCustomerCommand : CommandBase
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

    protected override async Task ExecuteAsync(object parameter)
    {
      CustomerDialog dialog = new CustomerDialog();
      CustomerVM VM = new CustomerVM(_repository, _addressRepository, CUD.Create,
                              new Action(() => dialog.Close()), _user);
      dialog.DataContext = VM;
      bool? result = dialog.ShowDialog();

      if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
      {
        _customerVM.AllCustomers = await _customerVM.GetAll().ConfigureAwait(true);

        _customerVM.Customers.Clear();

        foreach (Customer customer in _customerVM.AllCustomers)
          _customerVM.Customers.Add(customer);
      }
    }
  }
}
