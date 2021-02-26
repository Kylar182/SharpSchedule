using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows.Input;
using SharpSchedule.Commands.CustomersVMCommands;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.EntityModels.Scheduling;
using SharpSchedule.Data.Repositories.Location;
using SharpSchedule.Data.Repositories.Scheduling;

namespace SharpSchedule.ViewModels
{
  public class CustomersVM : ViewModelBase
  {
    private readonly ICustomerRepository _repository;
    private readonly IAddressRepository _addressRepository;
    private readonly User _user;

    private Customer customer;
    [Required(ErrorMessage = "Customer is Required")]
    public Customer CustomerSelected
    {
      get => customer;
      set
      {
        customer = value;
        OnPropChanged(nameof(CustomerSelected));
      }
    }

    /// <summary>
    /// Filtered Customers
    /// </summary>
    public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();

    /// <summary>
    /// All Customers currently in the System
    /// </summary>
    public List<Customer> AllCustomers { get; set; } = new List<Customer>();

    public ICommand SearchCustomers { get; }

    public ICommand NewCustomer { get; }

    public ICommand UpdateCustomer { get; }

    public ICommand DeleteCustomer { get; }

    public CustomersVM(ICustomerRepository repository,
      IAddressRepository addressRepository, User user)
    {
      _repository = repository;
      _addressRepository = addressRepository;
      _user = user;

      Load().ConfigureAwait(true);

      CustomerSelected = null;

      SearchCustomers = new SearchCustomersCommand(this);
    }

    /// <summary>
    /// Loads DB Data for View
    /// </summary>
    public async Task Load()
    {
      await _repository.GetAll().ContinueWith(t =>
      {
        if (t.Exception == null)
        {
          AllCustomers = t.Result;
          foreach (Customer customer in AllCustomers)
            Customers.Add(customer);
        }
      }).ConfigureAwait(true);
    }
  }
}
