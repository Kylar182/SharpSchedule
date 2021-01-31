using System.Threading.Tasks;
using SharpSchedule.Data.EntityModels;

namespace SharpSchedule.Data.Services
{
  /// <summary>
  /// Base Repo with Generic Crud Methods
  /// </summary>
  /// <typeparam name="T">
  /// Database Model inherited from the BaseModel
  /// </typeparam>
  public interface IRepository<T> where T : BaseModel
  {
    /// <summary>
    /// Gets an Item in the Database with this Id
    /// </summary>
    /// <returns>
    /// The Model matching this Id or Null Model
    /// </returns>
    Task<T> GetById(int id);

    /// <summary>
    /// Creates an Item in the Database
    /// </summary>
    /// <returns>
    /// Updated Model
    /// </returns>
    Task<T> Create(T item);

    /// <summary>
    /// Updates an Item in the Database
    /// </summary>
    /// <returns>
    /// Updated Model
    /// </returns>
    Task<T> Update(T item);

    /// <summary>
    /// Task to Delete an item from the Database by it's Database Id
    /// </summary>
    /// <returns>
    /// Boolean value of successful delete
    /// </returns>
    Task<bool> Delete(T item);
  }
}
