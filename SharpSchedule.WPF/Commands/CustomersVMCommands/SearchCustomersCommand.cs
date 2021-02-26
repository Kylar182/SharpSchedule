using System;
using System.Linq;
using System.Windows.Input;
using SharpSchedule.Data.EntityModels.Scheduling;
using SharpSchedule.ViewModels;

namespace SharpSchedule.Commands.CustomersVMCommands
{
  public class SearchCustomersCommand : ICommand
  {
    private readonly CustomersVM _customerVM;

    public SearchCustomersCommand(CustomersVM customerVM)
    {
      _customerVM = customerVM;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter)
    {
      if (parameter is string name)
      {
        _customerVM.Customers.Clear();

        foreach (Customer customer in _customerVM.AllCustomers.Where(pr => pr.Name.Contains(name)))
          _customerVM.Customers.Add(customer);
      }
    }
  }
}
