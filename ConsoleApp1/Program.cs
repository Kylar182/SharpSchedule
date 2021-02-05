using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.Data.Repositories;
using SharpSchedule.Persistence;
using SharpSchedule.Persistence.Repositories;

namespace TestConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      IRepository<Country> Repo = new Repository<Country>(new DbContextFactory());
      List<Country> Countries = new List<Country>();
      Repo.GetAll().ContinueWith(c =>
      {
        if (c.Exception == null)
          Countries = c.Result;
      });
      CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
      List<string> CultureList = Countries.Select(c => c.Name).ToList();

      foreach (CultureInfo getCulture in getCultureInfo)
      {
        RegionInfo GetRegionInfo = new RegionInfo(getCulture.LCID);
        if (!(CultureList.Contains(GetRegionInfo.EnglishName)))
        {
          CultureList.Add(GetRegionInfo.EnglishName);

          Repo.Create(new Country 
          {
            Name = GetRegionInfo.EnglishName,
            CreatedBy = "test",
            CreateDate = DateTime.UtcNow,
            LastUpdatedBy = "test",
            LastUpdate = DateTime.UtcNow
          }).Wait();
        }
      }
    }
  }
}
