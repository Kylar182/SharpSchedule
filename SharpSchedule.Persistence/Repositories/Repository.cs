using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.Services;

namespace SharpSchedule.Persistence.Repositories
{
  /// <summary>
  /// Base Repository Methods Implemented 
  /// as an overridable abstract class
  /// </summary>
  /// <typeparam name="T">
  /// Database Model inherited from the BaseModel
  /// </typeparam>
  public abstract class Repository<T> : IRepository<T> where T : BaseModel
  {
    protected readonly DbContextFactory _contextFactory;

    public Repository(DbContextFactory _contextFactor)
    {
      _contextFactory = _contextFactor;
    }

    public virtual async Task<T> GetById(int id)
    {
      using (SchedulingContext _context = _contextFactory.CreateDbContext())
      {
        return await _context.Set<T>().FindAsync(id);
      }
    }

    public virtual async Task<T> Create(T item)
    {
      using (SchedulingContext _context = _contextFactory.CreateDbContext())
      {
        await _context.Set<T>().AddAsync(item);
        await _context.SaveChangesAsync();
        return item;
      }
    }

    public virtual async Task<T> Update(T item)
    {
      using (SchedulingContext _context = _contextFactory.CreateDbContext())
      {
        _context.Set<T>().Update(item);
        await _context.SaveChangesAsync();
        return item;
      }
    }

    public virtual async Task<bool> Delete(T item)
    {
      using (SchedulingContext _context = _contextFactory.CreateDbContext())
      {
        _context.Set<T>().Remove(item);
        await _context.SaveChangesAsync();
        return true;
      }
    }
  }
}
