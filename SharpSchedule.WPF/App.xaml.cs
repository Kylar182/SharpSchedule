﻿using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.Data.EntityModels.Scheduling;
using SharpSchedule.Data.Repositories;
using SharpSchedule.Data.Repositories.Location;
using SharpSchedule.Data.Repositories.Scheduling;
using SharpSchedule.Persistence;
using SharpSchedule.Persistence.Repositories;
using SharpSchedule.Persistence.Repositories.Location;
using SharpSchedule.Persistence.Repositories.Scheduling;
using SharpSchedule.Services;
using SharpSchedule.Services.Interfaces;
using SharpSchedule.State.Navigators;
using SharpSchedule.ViewModels;
using SharpSchedule.ViewModels.Factories;
using SharpSchedule.WPF;

namespace SharpSchedule
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      IServiceProvider serviceProvider = CreateServiceProvider();

      Window window = serviceProvider.GetRequiredService<MainWindow>();
      window.Show();

      base.OnStartup(e);
    }

    private static IServiceProvider CreateServiceProvider()
    {
      IServiceCollection services = new ServiceCollection();

      services.AddSingleton<DbContextFactory>();
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IAddressRepository, AddressRepository>();
      services.AddScoped<ICityRepository, CityRepository>();
      services.AddScoped<IRepository<Country>, Repository<Country>>();
      services.AddScoped<IAppointmentRepository, AppointmentRepository>();
      services.AddScoped<ICustomerRepository, CustomerRepository>();

      services.AddSingleton<IRootVMFactory, RootVMFactory>();
      services.AddSingleton<IVMFactory<HomeVM>, HomeVMFactory>();
      services.AddSingleton<IVMFactory<CustomersVM>, CustomersVMFactory>();
      services.AddSingleton<IVMFactory<AppointmentsVM>, AppointmentsVMFactory>();
      services.AddSingleton<IVMFactory<AddressesVM>, AddressesVMFactory>();
      services.AddSingleton<IVMFactory<LoginVM>, LoginVMFactory>();

      services.AddSingleton<IAuthService, AuthService>();
      services.AddScoped<INavigator, Navigator>();
      services.AddScoped<MainVM>();

      services.AddScoped(s => new MainWindow(s.GetRequiredService<MainVM>()));

      return services.BuildServiceProvider();
    }
  }
}
