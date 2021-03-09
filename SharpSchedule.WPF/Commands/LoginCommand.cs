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
            _loginVM.Message = _loginVM.Info == "en" ?
              "Incorrect Username or Password" : "Identifiant ou mot de passe incorrect";
            break;
          case null:
            _loginVM.Message = _loginVM.Info == "en" ?
              "Database could not be reached" : "La base de données n'a pas pu être atteinte";
            break;
          case true:
            _loginVM.Message = _loginVM.Info == "en" ? "Success" : "Succès";
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
