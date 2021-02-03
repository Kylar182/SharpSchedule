using System;
using SharpSchedule.State.Navigators;

namespace SharpSchedule.ViewModels.Factories
{
  public class VMAbstractFactory : IVMAbstractFactory
  {
    private readonly IVMFactory<HomeVM> _homeFactory;
    private readonly IVMFactory<CustomersVM> _customersFactory;
    private readonly IVMFactory<AppointmentsVM> _appointmentsFactory;
    private readonly IVMFactory<AddressesVM> _addressesFactory;

    public VMAbstractFactory(
      IVMFactory<HomeVM> homeFactory,
      IVMFactory<CustomersVM> customersFactory,
      IVMFactory<AppointmentsVM> appointmentsFactory,
      IVMFactory<AddressesVM> addressesFactory
      )
    {
      _homeFactory = homeFactory;
      _customersFactory = customersFactory;
      _appointmentsFactory = appointmentsFactory;
      _addressesFactory = addressesFactory;
    }

    public ViewModelBase CreateVM(ViewType type)
    {
      switch (type)
      {
        case ViewType.Home:
          return _homeFactory.CreateVM();
        case ViewType.Customers:
          return _customersFactory.CreateVM();
        case ViewType.Appointments:
          return _appointmentsFactory.CreateVM();
        case ViewType.Addresses:
          return _addressesFactory.CreateVM();
        default:
          throw new ArgumentException("View Type doesn't match a VM");
      }
    }
  }
}
