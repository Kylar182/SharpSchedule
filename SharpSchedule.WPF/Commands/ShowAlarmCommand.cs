using System;
using System.Windows.Input;
using SharpSchedule.Data.DTOs;
using SharpSchedule.ViewModels.DialogViewModels;
using SharpSchedule.Views.Dialogs;

namespace SharpSchedule.Commands
{
  public class ShowAlarmCommand : ICommand
  {
    private readonly AppointmentDTO _dto;

    public ShowAlarmCommand(AppointmentDTO dto)
    {
      _dto = dto;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return _dto != null;
    }

    public void Execute(object parameter)
    {
      if (_dto != null)
      {
        AlarmDialog dialog = new AlarmDialog();

        AlarmVM VM = new AlarmVM(_dto);

        dialog.DataContext = VM;
        dialog.ShowDialog();
      }
    }
  }
}
