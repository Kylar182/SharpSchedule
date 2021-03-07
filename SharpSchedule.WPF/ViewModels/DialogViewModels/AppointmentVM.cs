using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using SharpSchedule.Commands.AppointmentsVMCommands;
using SharpSchedule.Commands.CustomersVMCommands;
using SharpSchedule.Data.DTOs;
using SharpSchedule.Data.EntityModels;
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
  public class AppointmentVM : ValidationBase
  {
    private readonly IAppointmentRepository _repository;
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

    public string WindowLabel => cudString + " Appointment";

    private string title;
    /// <summary>
    /// Information about the Appointment
    /// </summary>
    [Required(ErrorMessage = "A Title is Required")]
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
          DTO.Title = value;
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
    [Required(ErrorMessage = "A Description is Required")]
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
          DTO.Description = value;
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
    [Required(ErrorMessage = "A Location is Required")]
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
          DTO.Location = value;
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
    [Required(ErrorMessage = "A Contact is Required")]
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
          DTO.Contact = value;
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
    [Required(ErrorMessage = "Appointment Type Required")]
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
          DTO.Type = value;
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
    [Required(ErrorMessage = "Appointment Link Required")]
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
          DTO.URL = value;
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

    private DateTime start;
    /// <summary>
    /// Start Time of the Appointment
    /// </summary>
    [MinBusinessHours(nameof(Start), nameof(End))]
    public DateTime Start
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
          DTO.Start = value.ToUniversalTime();
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

    private DateTime end;
    /// <summary>
    /// End Time of the Appointment
    /// </summary>
    [MaxBusinessHours(nameof(End), nameof(Start))]
    public DateTime End
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
          DTO.End = value.ToUniversalTime();
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
    [Required(ErrorMessage = "Customer is Required")]
    public Customer CustomerSelected
    {
      get => customer;
      set
      {
        ValidateProp(value);
        customer = value;

        OnPropChanged(nameof(CustomerSelected));

        if (!PropHasErrors(nameof(CustomerSelected)))
          DTO.CustomerId = value.Id;
      }
    }

    private User consultant;
    /// <summary>
    /// Consultant of the Appointment
    /// </summary>
    [Required(ErrorMessage = "Consultant is Required")]
    public User ConsultantSelected
    {
      get => consultant;
      set
      {
        ValidateProp(value);
        consultant = value;

        OnPropChanged(nameof(ConsultantSelected));

        if (!PropHasErrors(nameof(ConsultantSelected)))
          DTO.UserId = value.Id;
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

    /// <summary>
    /// Filtered Users
    /// </summary>
    public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

    /// <summary>
    /// All Users currently in the System
    /// </summary>
    public List<User> AllUsers { get; set; } = new List<User>();

    /// <summary>
    /// Action to Relay message to the Dialog and let it know to Close
    /// </summary>
    public Action CloseAction { get; set; }

    /// <summary>
    /// Command to Perform CRUD Operation on this Model
    /// </summary>
    public ICommand CRUDCommand { get; }
    /// <summary>
    /// Command to Search all Customers in the System by Name
    /// </summary>
    public ICommand SearchCustomers { get; }
    /// <summary>
    /// Command to Search all Users in the System by Username
    /// </summary>
    public ICommand SearchUsers { get; }

    /// <summary>
    /// DTO for the Appointment this VM is performing CRUD ops on
    /// </summary>
    public AppointmentDTO DTO { get; set; }

    public AppointmentVM(
      IAppointmentRepository repository,
      CUD cud, Action action, User user,
      List<User> allUsers,
      List<Customer> allCustomers,
      AppointmentDTO appointment = null)
    {
      _repository = repository;

      _cud = cud;
      Enabled = cud != CUD.Delete;
      CUDString = cud.ToString();
      CloseAction = action;

      AllUsers = allUsers;

      foreach (User consultantUser in AllUsers)
        Users.Add(consultantUser);

      AllCustomers = allCustomers;

      foreach (Customer customer in AllCustomers)
        Customers.Add(customer);

      if (appointment != null)
      {
        DTO = appointment;
        DTO.LastUpdatedBy = user.Username;
        Title = DTO.Title;
        Description = DTO.Description;
        Location = DTO.Location;
        Contact = DTO.Contact;
        Type = DTO.Type;
        URL = DTO.URL;
        Start = DTO.Start;
        End = DTO.End;
        Start = DTO.Start;
        CustomerSelected = AllCustomers.Where(pr => pr.Id == appointment.CustomerId).First();
        ConsultantSelected = AllUsers.Where(pr => pr.Id == appointment.UserId).First();
      }
      else
      {
        DTO = new AppointmentDTO
        {
          CreatedBy = user.Username,
          CreateDate = DateTime.UtcNow,
          LastUpdatedBy = user.Username
        };

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
        ConsultantSelected = null;
      }

      CRUDCommand = new AppointmentCRUDCommand(this);
      SearchCustomers = new SearchCustomersCommand(this);
      SearchUsers = new SearchUsersCommand(this);
    }

    public async Task DBUpdate()
    {
      DTO.Start = Start.ToUniversalTime();
      DTO.End = End.ToUniversalTime();
      DTO.LastUpdate = DateTime.UtcNow;
      DTO.Customer = null;

      switch (_cud)
      {
        case CUD.Create:
          await _repository.Create(DTO.ToAppointment()).ConfigureAwait(true);
          CloseAction?.Invoke();
          break;
        case CUD.Update:
          await _repository.Update(DTO.ToAppointment()).ConfigureAwait(true);
          CloseAction?.Invoke();
          break;
        case CUD.Delete:
          await _repository.Delete(DTO.Id).ConfigureAwait(true);
          CloseAction?.Invoke();
          break;
      };
    }
  }
}
