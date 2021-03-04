using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using SharpSchedule.Commands.CustomersVMCommands;
using SharpSchedule.Data.EntityModels.Scheduling;
using SharpSchedule.Data.Repositories.Scheduling;
using SharpSchedule.Data.Validation;
using SharpSchedule.Models;
using SharpSchedule.ViewModels.Validation;

namespace SharpSchedule.ViewModels.DialogViewModels
{
  /// <summary>
  /// View Model for Appointment CRUD in the Appointment Dialog
  /// </summary>
  public class AppointmentFilterVM : ValidationBase
  {
    private readonly ICustomerRepository _customerRepository;

    public static string WindowLabel => "Filter Appointments";

    private string title;
    /// <summary>
    /// Information about the Appointment
    /// </summary>
    [MaxLength(255, ErrorMessage = "Max Length 255 Characters")]
    public string Title
    {
      get => title;
      set
      {
        ValidateProp(value);
        title = value;
        OnPropChanged(nameof(Title));
        OnPropChanged(nameof(TitleText));
        OnPropChanged(nameof(TitleValid));

        if (TitleValid)
          Appointment.Title = value;
      }
    }

    /// <summary>
    /// Helpertext for the Appointment's Title
    /// </summary>
    /// <remarks>
    /// If Title is invalid, returns the first Error message, 
    /// otherwise it returns the designated Helpertext
    /// </remarks>
    public string TitleText => PropHasErrors(nameof(Title)) ?
      GetErrors(nameof(Title)).OfType<string>().First() : "Input Appointment's Title";

    /// <summary>
    /// Bool to determine if Title is Valid or not
    /// </summary>
    public bool TitleValid => !PropHasErrors(nameof(Title));

    private string description;
    /// <summary>
    /// Description of the Appointment
    /// </summary>
    public string Description
    {
      get => description;
      set
      {
        ValidateProp(value);
        description = value;
        OnPropChanged(nameof(Description));
        OnPropChanged(nameof(DescriptionText));
        OnPropChanged(nameof(DescriptionValid));

        if (DescriptionValid)
          Appointment.Description = value;
      }
    }

    /// <summary>
    /// Helpertext for the Appointment's Description
    /// </summary>
    /// <remarks>
    /// If Description is invalid, returns the first Error message, 
    /// otherwise it returns the designated Helpertext
    /// </remarks>
    public string DescriptionText => PropHasErrors(nameof(Description)) ?
      GetErrors(nameof(Description)).OfType<string>().First() : "Input Appointment's Description";

    /// <summary>
    /// Bool to determine if Appointment's Description is Valid or not
    /// </summary>
    public bool DescriptionValid => !PropHasErrors(nameof(Description));

    private string location;
    /// <summary>
    /// Appointment's Location
    /// </summary>
    public string Location
    {
      get => location;
      set
      {
        ValidateProp(value);
        location = value;
        OnPropChanged(nameof(Location));
        OnPropChanged(nameof(LocationText));
        OnPropChanged(nameof(LocationValid));

        if (LocationValid)
          Appointment.Location = value;
      }
    }

    /// <summary>
    /// Helpertext for the Appointment's Location
    /// </summary>
    /// <remarks>
    /// If Location is invalid, returns the first Error message, 
    /// otherwise it returns the designated Helpertext
    /// </remarks>
    public string LocationText => PropHasErrors(nameof(Location)) ?
      GetErrors(nameof(Location)).OfType<string>().First() : "Input Appointment's Location";

    /// <summary>
    /// Bool to determine if Appointment's Location is Valid or not
    /// </summary>
    public bool LocationValid => !PropHasErrors(nameof(Location));

    private string contact;
    /// <summary>
    /// Point of Contact for the Appointment
    /// </summary>
    public string Contact
    {
      get => contact;
      set
      {
        ValidateProp(value);
        contact = value;
        OnPropChanged(nameof(Contact));
        OnPropChanged(nameof(ContactText));
        OnPropChanged(nameof(ContactValid));

        if (ContactValid)
          Appointment.Contact = value;
      }
    }

    /// <summary>
    /// Helpertext for the Appointment's Contact
    /// </summary>
    /// <remarks>
    /// If Contact is invalid, returns the first Error message, 
    /// otherwise it returns the designated Helpertext
    /// </remarks>
    public string ContactText => PropHasErrors(nameof(Contact)) ?
      GetErrors(nameof(Contact)).OfType<string>().First() : "Input Appointment's Contact";

    /// <summary>
    /// Bool to determine if Appointment's Contact is Valid or not
    /// </summary>
    public bool ContactValid => !PropHasErrors(nameof(Contact));

    private string type;
    /// <summary>
    /// Type of the Appointment
    /// </summary>
    public string Type
    {
      get => type;
      set
      {
        ValidateProp(value);
        type = value;
        OnPropChanged(nameof(Type));
        OnPropChanged(nameof(TypeText));
        OnPropChanged(nameof(TypeValid));

        if (TypeValid)
          Appointment.Type = value;
      }
    }

    /// <summary>
    /// Helpertext for the Appointment's Type
    /// </summary>
    /// <remarks>
    /// If Type is invalid, returns the first Error message, 
    /// otherwise it returns the designated Helpertext
    /// </remarks>
    public string TypeText => PropHasErrors(nameof(Type)) ?
      GetErrors(nameof(Type)).OfType<string>().First() : "Input Appointment's Type";

    /// <summary>
    /// Bool to determine if Appointment's Type is Valid or not
    /// </summary>
    public bool TypeValid => !PropHasErrors(nameof(Type));

    private string url;
    /// <summary>
    /// Hotlink to info on the Appointment or it's attendees
    /// </summary>
    [MaxLength(255, ErrorMessage = "Max Length 255 Characters")]
    public string URL
    {
      get => url;
      set
      {
        ValidateProp(value);
        url = value;
        OnPropChanged(nameof(URL));
        OnPropChanged(nameof(URLText));
        OnPropChanged(nameof(URLValid));

        if (URLValid)
          Appointment.URL = value;
      }
    }

    /// <summary>
    /// Helpertext for the Appointment's URL
    /// </summary>
    /// <remarks>
    /// If URL is invalid, returns the first Error message, 
    /// otherwise it returns the designated Helpertext
    /// </remarks>
    public string URLText => PropHasErrors(nameof(URL)) ?
      GetErrors(nameof(URL)).OfType<string>().First() : "Input Appointment's URL";

    /// <summary>
    /// Bool to determine if Appointment's URL is Valid or not
    /// </summary>
    public bool URLValid => !PropHasErrors(nameof(URL));

    private DateTime? start;
    /// <summary>
    /// Start Time of the Appointment
    /// </summary>
    [StartDate(nameof(Start), nameof(End))]
    public DateTime? Start
    {
      get => start;
      set
      {
        ValidateProp(value);
        start = value;
        OnPropChanged(nameof(Start));
        OnPropChanged(nameof(StartText));
        OnPropChanged(nameof(StartValid));

        ValidateProp(End, nameof(End));
        OnPropChanged(nameof(End));
        OnPropChanged(nameof(EndText));
        OnPropChanged(nameof(EndValid));

        ValidateProp(value);

        if (StartValid)
        {
          if (value != null)
            Appointment.Start = value.Value.ToUniversalTime();
        }
        else
          Appointment.Start = null;
      }
    }

    /// <summary>
    /// Helpertext for the Appointment's Start
    /// </summary>
    /// <remarks>
    /// If Start is invalid, returns the first Error message, 
    /// otherwise it returns the designated Helpertext
    /// </remarks>
    public string StartText => PropHasErrors(nameof(Start)) ?
      GetErrors(nameof(Start)).OfType<string>().First() : "Input Appointment's Start Time";

    /// <summary>
    /// Bool to determine if Appointment's Start is Valid or not
    /// </summary>
    public bool StartValid => !PropHasErrors(nameof(Start));

    private DateTime? end;
    /// <summary>
    /// End Time of the Appointment
    /// </summary>
    [EndDate(nameof(End), nameof(Start))]
    public DateTime? End
    {
      get => end;
      set
      {
        ValidateProp(value);
        end = value;
        OnPropChanged(nameof(End));
        OnPropChanged(nameof(EndText));
        OnPropChanged(nameof(EndValid));

        ValidateProp(Start, nameof(Start));
        OnPropChanged(nameof(Start));
        OnPropChanged(nameof(StartText));
        OnPropChanged(nameof(StartValid));

        ValidateProp(value);

        if (EndValid)
        {
          if (value != null)
            Appointment.End = value.Value.ToUniversalTime();
        }
        else
          Appointment.End = null;
      }
    }

    /// <summary>
    /// Helpertext for the Appointment's End
    /// </summary>
    /// <remarks>
    /// If End is invalid, returns the first Error message, 
    /// otherwise it returns the designated Helpertext
    /// </remarks>
    public string EndText => PropHasErrors(nameof(End)) ?
      GetErrors(nameof(End)).OfType<string>().First() : "Input Appointment's End Time";

    /// <summary>
    /// Bool to determine if Appointment's End is Valid or not
    /// </summary>
    public bool EndValid => !PropHasErrors(nameof(End));

    private Customer customer;
    /// <summary>
    /// Customer of the Appointment
    /// </summary>
    public Customer CustomerSelected
    {
      get => customer;
      set
      {
        customer = value;

        OnPropChanged(nameof(CustomerSelected));

        if (value != null)
          Appointment.CustomerId = value.Id;
        else
          Appointment.CustomerId = null;
      }
    }

    /// <summary>
    /// The Appointment this VM is performing CRUD ops on
    /// </summary>
    public AppointmentFilter Appointment { get; set; }

    /// <summary>
    /// Filtered Customers
    /// </summary>
    public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();

    /// <summary>
    /// All Customers currently in the System
    /// </summary>
    public List<Customer> AllCustomers { get; set; } = new List<Customer>();

    public Action CloseAction { get; set; }

    public ICommand Month { get; }

    public ICommand Week { get; }

    public ICommand Filter { get; }

    public ICommand SearchCustomers { get; }

    public AppointmentFilterVM(ICustomerRepository customerRepository, Action action)
    {
      _customerRepository = customerRepository;

      CloseAction = action;

      Load().ConfigureAwait(true);

      Title = string.Empty;
      Description = string.Empty;
      Location = string.Empty;
      Contact = string.Empty;
      Type = string.Empty;
      URL = string.Empty;
      Start = DateTime.Now;
      End = DateTime.Now.AddHours(1);
      Start = DateTime.Now;
      CustomerSelected = null;

      SearchCustomers = new SearchCustomersCommand(this);
    }

    /// <summary>
    /// Loads DB Data for Dialog
    /// </summary>
    private async Task Load()
    {
      await _customerRepository.GetAll().ContinueWith(t =>
      {
        if (t.Exception == null)
        {
          AllCustomers = t.Result;
          foreach (Customer address in AllCustomers)
            Customers.Add(address);
        }
      }).ConfigureAwait(true);
    }

    public AppointmentFilter DBFilter()
    {
      if (Start.HasValue && StartValid)
        Appointment.Start = Start.Value.ToUniversalTime();
      if (End.HasValue && EndValid)
        Appointment.End = End.Value.ToUniversalTime();

      return Appointment;
    }

    public void Close()
    {
      CloseAction();
    }
  }
}
