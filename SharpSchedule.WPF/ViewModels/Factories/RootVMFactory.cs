using System;
using SharpSchedule.State.Navigators;

namespace SharpSchedule.ViewModels.Factories
{
  public class RootVMFactory : IRootVMFactory
  {
    private readonly IVMFactory<LoginVM> _loginFactory;
    private readonly IVMFactory<HomeVM> _homeFactory;
    private readonly IVMFactory<CustomersVM> _customersFactory;
    private readonly IVMFactory<AppointmentsVM> _appointmentsFactory;
    private readonly IVMFactory<AddressesVM> _addressesFactory;

    public RootVMFactory(
      IVMFactory<LoginVM> loginFactory,
      IVMFactory<HomeVM> homeFactory,
      IVMFactory<CustomersVM> customersFactory,
      IVMFactory<AppointmentsVM> appointmentsFactory,
      IVMFactory<AddressesVM> addressesFactory)
    {
      _loginFactory = loginFactory;
      _homeFactory = homeFactory;
      _customersFactory = customersFactory;
      _appointmentsFactory = appointmentsFactory;
      _addressesFactory = addressesFactory;
    }

    /// <summary>
    /// Creates the appropriate View Model for the selected View Type
    /// </summary>
    public ViewModelBase CreateVM(ViewType type)
    {
      return type switch
      {
        ViewType.Login => _loginFactory.CreateVM(),
        ViewType.Home => _homeFactory.CreateVM(),
        ViewType.Customers => _customersFactory.CreateVM(),
        ViewType.Appointments => _appointmentsFactory.CreateVM(),
        ViewType.Addresses => _addressesFactory.CreateVM(),
        _ => throw new ArgumentException("View Type doesn't match a VM"),
      };
    }
  }
}
