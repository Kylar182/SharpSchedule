﻿using System;
using System.Linq;
using System.Windows.Input;
using SharpSchedule.Data.EntityModels.Scheduling;
using SharpSchedule.ViewModels;
using SharpSchedule.ViewModels.DialogViewModels;

namespace SharpSchedule.Commands.CustomersVMCommands
{
  public class SearchCustomersCommand : ICommand
  {
    private readonly CustomersVM _customerVM;
    private readonly AppointmentVM _appointmentVM;
    private readonly AppointmentFilterVM _filterVM;

    public SearchCustomersCommand(CustomersVM customerVM)
    {
      _customerVM = customerVM;
    }

    public SearchCustomersCommand(AppointmentVM appointmentVM)
    {
      _appointmentVM = appointmentVM;
    }

    public SearchCustomersCommand(AppointmentFilterVM filterVM)
    {
      _filterVM = filterVM;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter)
    {
      if (parameter is string name)
      {
        if (_customerVM != null)
        {
          _customerVM.Customers.Clear();

          foreach (Customer customer in _customerVM.AllCustomers.Where(pr => pr.Name.Contains(name)))
            _customerVM.Customers.Add(customer);
        }
        else if (_appointmentVM != null)
        {
          _appointmentVM.Customers.Clear();

          foreach (Customer customer in _appointmentVM.AllCustomers.Where(pr => pr.Name.Contains(name)))
            _appointmentVM.Customers.Add(customer);
        }
        else
        {
          _filterVM.Customers.Clear();

          foreach (Customer customer in _filterVM.AllCustomers.Where(pr => pr.Name.Contains(name)))
            _filterVM.Customers.Add(customer);
        }
      }
    }
  }
}
