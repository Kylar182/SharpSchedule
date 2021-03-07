using System;
using System.ComponentModel;
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
  public class UpdateCustomerCommand : CommandBase
  {
    private readonly CustomersVM _customerVM;
    private readonly ICustomerRepository _repository;
    private readonly IAddressRepository _addressRepository;
    private readonly User _user;

    public UpdateCustomerCommand(CustomersVM customerVM,
      ICustomerRepository repository,
      IAddressRepository addressRepository,
      User user)
    {
      _customerVM = customerVM;
      _repository = repository;
      _addressRepository = addressRepository;
      _user = user;

      _customerVM.PropertyChanged += CustomerChanged;
    }

    public override event EventHandler CanExecuteChanged;

    public override bool CanExecute(object parameter)
    {
      return _customerVM.CustomerSelected != null;
    }

    protected override async Task ExecuteAsync(object parameter)
    {
      if (_customerVM.CustomerSelected != null)
      {
        CustomerDialog dialog = new CustomerDialog();
        CustomerVM VM = new CustomerVM(_repository, _addressRepository, CUD.Update,
                                new Action(() => dialog.Close()), _user, _customerVM.CustomerSelected);
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

    private void CustomerChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(_customerVM.CustomerSelected))
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
  }
}
