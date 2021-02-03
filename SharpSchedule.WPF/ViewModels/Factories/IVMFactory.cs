namespace SharpSchedule.ViewModels.Factories
{
  /// <summary>
  /// Creates the View Model used for Abstraction 
  /// and DI into Views as the Data Context
  /// </summary>
  /// <typeparam name="T">
  /// Any VM inheriting the View Model Base
  /// </typeparam>
  public interface IVMFactory<T> where T : ViewModelBase
  {
    T CreateVM();
  }
}
