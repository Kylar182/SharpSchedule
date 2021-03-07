using System;
using System.ComponentModel;
using System.Windows.Input;
using SharpSchedule.Data.DTOs;
using SharpSchedule.Services.Interfaces;
using SharpSchedule.State.Navigators;
using SharpSchedule.ViewModels;
using SharpSchedule.ViewModels.Factories;

namespace SharpSchedule.Commands
{
  public class LoginCommand : ICommand
  {
    private readonly LoginVM _loginVM;
    private readonly IAuthService _authService;
    private readonly INavigator _navigator;
    private readonly IVMFactory<AppointmentsVM> _appointmentVMFactory;

    public LoginCommand(
      LoginVM loginVM,
      IAuthService authService,
      INavigator navigator,
      IVMFactory<AppointmentsVM> appointmentVMFactory)
    {
      _loginVM = loginVM;
      _authService = authService; ;
      _navigator = navigator;
      _appointmentVMFactory = appointmentVMFactory;

      _loginVM.PropertyChanged += ErrorsChanged;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return !_loginVM.HasErrors;
    }

    public async void Execute(object parameter)
    {
      if (!_loginVM.HasErrors)
      {
        bool? login = await _authService.Login(new LoginDTO(_loginVM.Username, _loginVM.Password));

        switch (login)
        {
          case false:
            _loginVM.Message = "Incorrect Username or Password";
            break;
          case null:
            _loginVM.Message = "Database could not be reached";
            break;
          case true:
            _loginVM.Message = "Success";
            _navigator.CurrentVM = _appointmentVMFactory.CreateVM();
            break;
        }
      }
    }

    private void ErrorsChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(_loginVM.HasErrors))
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
  }
}
