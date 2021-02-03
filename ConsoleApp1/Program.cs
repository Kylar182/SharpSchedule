using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.Repositories;
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
        CreatedBy = "test",
        LastUpdatedBy = "test",
        Password = "test",
        Username = "test"
      }).Wait();
    }
  }
}
