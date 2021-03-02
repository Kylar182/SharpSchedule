using System;
using System.ComponentModel;
using System.Windows.Input;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.Repositories.Scheduling;
using SharpSchedule.Models;
using SharpSchedule.ViewModels;
using SharpSchedule.ViewModels.DialogViewModels;
using SharpSchedule.Views.Dialogs;

namespace SharpSchedule.Commands.CustomersVMCommands
{
  public class UpdateAppointmentCommand : ICommand
  {
    private readonly AppointmentsVM _appointmentsVM;
    private readonly IAppointmentRepository _repository;
    private readonly ICustomerRepository _customerRepository;
    private readonly User _user;

    public UpdateAppointmentCommand(AppointmentsVM appointmentsVM,
      IAppointmentRepository repository,
      ICustomerRepository customerRepository,
      User user)
    {
      _appointmentsVM = appointmentsVM;
      _repository = repository;
      _customerRepository = customerRepository;
      _user = user;

      _appointmentsVM.PropertyChanged += AppointmentChanged;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return _appointmentsVM.AppointmentSelected != null;
    }

    public void Execute(object parameter)
    {
      if (_appointmentsVM.AppointmentSelected != null)
      {
        AppointmentDialog dialog = new AppointmentDialog();
        AppointmentVM VM = new AppointmentVM(_repository, _customerRepository, CUD.Update,
                                new Action(() => dialog.Close()), _user, _appointmentsVM.AppointmentSelected);
        dialog.DataContext = VM;
        bool? result = dialog.ShowDialog();

        if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
          _appointmentsVM.Load().ConfigureAwait(true);

        _appointmentsVM.SearchAppointments.Execute(string.Empty);
      }
    }

    private void AppointmentChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(_appointmentsVM.AppointmentSelected))
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
  }
}
