using System.ComponentModel;

namespace SharpSchedule.Models
{
  public class ObservableObject : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropChanged(string propname)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
    }
  }
}
