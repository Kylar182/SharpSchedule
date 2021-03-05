using System;
using System.Windows.Input;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.Repositories.Scheduling;
using SharpSchedule.Models;
using SharpSchedule.ViewModels;
using SharpSchedule.ViewModels.DialogViewModels;
using SharpSchedule.Views.Dialogs;

namespace SharpSchedule.Commands.AppointmentsVMCommands
{
  public class NewAppointmentCommand : ICommand
  {
    private readonly AppointmentsVM _appointmentsVM;
    private readonly IAppointmentRepository _repository;
    private readonly ICustomerRepository _customerRepository;
    private readonly User _user;

    public NewAppointmentCommand(AppointmentsVM appointmentsVM,
      IAppointmentRepository repository,
      ICustomerRepository customerRepository,
      User user)
    {
      _appointmentsVM = appointmentsVM;
      _repository = repository;
      _customerRepository = customerRepository;
      _user = user;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      AppointmentDialog dialog = new AppointmentDialog();
      AppointmentVM VM = new AppointmentVM(_repository, _customerRepository, CUD.Create,
                              new Action(() => dialog.Close()), _user);
      dialog.DataContext = VM;
      bool? result = dialog.ShowDialog();

      if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
        _appointmentsVM.Load().ConfigureAwait(true);

      _appointmentsVM.Refresh();
    }
  }
}
