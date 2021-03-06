using System.Collections.Generic;

namespace SharpSchedule.State
{
  /// <summary>
  /// Generic Singleon State Manager
  /// </summary>
  /// <typeparam name="T">
  /// Item(s) you want to manage the State of
  /// </typeparam>
  public interface IStateManager<T>
  {
    /// <summary>
    /// Method that returns State Items Managed
    /// </summary>
    List<T> GetState();

    /// <summary>
    /// Setter Method for State being Managed
    /// </summary>
    /// <param name="state">
    /// New State
    /// </param>
    void SetState(List<T> state);
  }
}
