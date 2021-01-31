using System;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.Services;
using SharpSchedule.Persistence;
using SharpSchedule.Persistence.Repositories;

namespace TestConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      IRepository<User> Repo = new Repository<User>(new DbContextFactory());
      Repo.Create(new User
      {
        Active = true,
        CreateDate = DateTime.UtcNow,
        CreatedBy = "test",
        LastUpdate = DateTime.UtcNow,
        LastUpdatedBy = "test",
        Password = "test",
        Username = "test"
      }).Wait();
    }
  }
}
