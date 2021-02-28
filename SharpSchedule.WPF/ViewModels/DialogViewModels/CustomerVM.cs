using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using SharpSchedule.Commands.AddressVMCommands;
using SharpSchedule.Commands.CustomersVMCommands;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.Data.EntityModels.Scheduling;
using SharpSchedule.Data.Repositories.Location;
using SharpSchedule.Data.Repositories.Scheduling;
using SharpSchedule.Models;
using SharpSchedule.ViewModels.Validation;

namespace SharpSchedule.ViewModels.DialogViewModels
{
  /// <summary>
  /// View Model for Customer CRUD in the Customer Dialog
  /// </summary>
  public class CustomerVM : ValidationBase
  {
    private readonly ICustomerRepository _repository;
    private readonly IAddressRepository _addressRepository;
    private readonly CUD _cud;

    private bool enabled;
    public bool Enabled
    {
      get => enabled;
      set
      {
        enabled = value;
        OnPropChanged(nameof(Enabled));
      }
    }

    private bool active;
    public bool Active
    {
      get => active;
      set
      {
        active = value;
        OnPropChanged(nameof(Active));

        Customer.Active = value;
      }
    }

    /// <summary>
    /// Helpertext for the Customer's Active Status
    /// </summary>
    public static string ActiveText => "Toggle Customer's Status";

    private string name;
    /// <summary>
    /// Name of the Customer
    /// </summary>
    [Required(ErrorMessage = "Customer's Name is required")]
    [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
    public string Name
    {
      get => name;
      set
      {
        ValidateProp(value);
        name = value;
        OnPropChanged(nameof(Name));
        OnPropChanged(nameof(NameText));
        OnPropChanged(nameof(NameValid));

        if (NameValid)
          Customer.Name = value;
      }
    }

    /// <summary>
    /// Helpertext for the Customer's Name
    /// </summary>
    /// <remarks>
    /// If Name is invalid, returns the first Error message, 
    /// otherwise it returns the designated Helpertext
    /// </remarks>
    public string NameText => PropHasErrors(nameof(Name)) ?
      GetErrors(nameof(Name)).OfType<string>().First() : "Input Customer's Name";

    /// <summary>
    /// Bool to determine if Name is Valid or not
    /// </summary>
    public bool NameValid => !PropHasErrors(nameof(Name));

    /// <summary>
    /// Filtered Addresses
    /// </summary>
    public ObservableCollection<Address> Addresses { get; set; } = new ObservableCollection<Address>();

    /// <summary>
    /// All Addresses currently in the System
    /// </summary>
    public List<Address> AllAddresses { get; set; } = new List<Address>();

    public string WindowLabel => cudString + " Customer";

    private Address address;
    /// <summary>
    /// Address of the Customer
    /// </summary>
    [Required(ErrorMessage = "Address is Required")]
    public Address AddressSelected
    {
      get => address;
      set
      {
        ValidateProp(value);
        address = value;

        OnPropChanged(nameof(AddressSelected));

        if (!PropHasErrors(nameof(AddressSelected)))
          Customer.AddressId = value.Id;
      }
    }

    private string cudString;
    /// <summary>
    /// String of CUD Enum, used for Display
    /// </summary>
    public string CUDString
    {
      get => cudString;
      set
      {
        cudString = value;
        OnPropChanged(nameof(CUDString));
        OnPropChanged(nameof(WindowLabel));
      }
    }

    public Action CloseAction { get; set; }

    public ICommand CRUDCommand { get; }

    public ICommand SearchAddresses { get; }

    /// <summary>
    /// The Customer this VM is performing CRUD ops on
    /// </summary>
    public Customer Customer { get; set; }

    public CustomerVM(ICustomerRepository customerRepository, IAddressRepository addressRepository,
                    CUD cud, Action action, User user, Customer customer = null)
    {
      _repository = customerRepository;
      _addressRepository = addressRepository;

      _cud = cud;
      Enabled = cud != CUD.Delete;
      CUDString = cud.ToString();
      CloseAction = action;

      Load().ConfigureAwait(true);

      if (customer != null)
      {
        Customer = customer;
        Customer.LastUpdatedBy = user.Username;
        Name = Customer.Name;
        Active = Customer.Active;
        AddressSelected = AllAddresses.Where(pr => pr.Id == customer.AddressId).First();
      }
      else
      {
        Customer = new Customer
        {
          CreatedBy = user.Username,
          CreateDate = DateTime.UtcNow,
          LastUpdatedBy = user.Username
        };

        Name = string.Empty;
        Active = true;
        AddressSelected = null;
      }

      OnPropChanged(nameof(ActiveText));

      CRUDCommand = new CustomerCRUDCommand(this);
      SearchAddresses = new SearchAddressesCommand(this);
    }

    /// <summary>
    /// Loads DB Data for Dialog
    /// </summary>
    private async Task Load()
    {
      await _addressRepository.GetAll().ContinueWith(t =>
      {
        if (t.Exception == null)
        {
          AllAddresses = t.Result;
          foreach (Address address in AllAddresses)
            Addresses.Add(address);
        }
      }).ConfigureAwait(true);
    }

    public async Task DBUpdate()
    {
      Customer.LastUpdate = DateTime.UtcNow;
      Customer.Address = null;

      switch (_cud)
      {
        case CUD.Create:
          await _repository.Create(Customer).ConfigureAwait(true);
          Close();
          break;
        case CUD.Update:
          await _repository.Update(Customer).ConfigureAwait(true);
          Close();
          break;
        case CUD.Delete:
          await _repository.Delete(Customer.Id).ConfigureAwait(true);
          Close();
          break;
      };
    }

    public void Close()
    {
      CloseAction();
    }
  }
}
