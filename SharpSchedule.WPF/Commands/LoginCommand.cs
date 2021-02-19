using System;
using System.Windows.Input;
using SharpSchedule.Data.DTOs;
using SharpSchedule.Services.Interfaces;
using SharpSchedule.ViewModels;

namespace SharpSchedule.Commands
{
  public class LoginCommand : ICommand
  {
    private readonly LoginVM _loginVM;
    private readonly IAuthService _authService;

    public LoginCommand(
      LoginVM loginVM,
      IAuthService authService)
    {
      _loginVM = loginVM;
      _authService = authService; ;
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
            break;
        }
      }
    }
  }
}
