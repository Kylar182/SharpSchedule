using System;
using System.Linq;
using System.Windows.Input;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.ViewModels.DialogViewModels;

namespace SharpSchedule.Commands.AppointmentsVMCommands
{
  public class SearchUsersCommand : ICommand
  {
    private readonly AppointmentVM _appointmentVM;
    private readonly AppointmentFilterVM _filterVM;

    public SearchUsersCommand(AppointmentVM appointmentVM)
    {
      _appointmentVM = appointmentVM;
    }

    public SearchUsersCommand(AppointmentFilterVM filterVM)
    {
      _filterVM = filterVM;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter)
    {
      if (parameter is string username)
      {
        if (_appointmentVM != null)
        {
          _appointmentVM.Users.Clear();

          foreach (User user in _appointmentVM.AllUsers.Where(pr => pr.Username.Contains(username)))
            _appointmentVM.Users.Add(user);
        }
        else
        {
          _filterVM.Users.Clear();

          foreach (User user in _filterVM.AllUsers.Where(pr => pr.Username.Contains(username)))
            _filterVM.Users.Add(user);
        }
      }
    }
  }
}
