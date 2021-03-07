using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharpSchedule.Data.DTOs;
using SharpSchedule.Data.EntityModels.Scheduling;
using SharpSchedule.Data.Repositories.Scheduling;
using SharpSchedule.State;

namespace SharpSchedule.ViewModels
{
  /// <summary>
  /// View Model for the Home View and 
  /// displaying Reports after Login
  /// </summary>
  public class HomeVM : ViewModelBase
  {
    //private readonly IAppointmentRepository _repository;
    private readonly IStateManager<AppointmentDTO> _state;

    public HomeVM(
      //IAppointmentRepository repository,
      IStateManager<AppointmentDTO> state)
    {
      //_repository = repository;
      _state = state;

      //LoadState().ConfigureAwait(true);
    }

    //private async Task LoadState()
    //{
    //  List<AppointmentDTO> dtos = _state.GetState();
    //  if (dtos == null || !dtos.Any())
    //  {
    //    List<Appointment> state = await _repository.Current();

    //    foreach (Appointment appointment in state)
    //    {
    //      AppointmentDTO dto = new AppointmentDTO(appointment);
    //      dto.Start = dto.Start.ToLocalTime();
    //      dto.End = dto.End.ToLocalTime();
    //      dtos.Add(dto);
    //    }

    //    _state.SetState(dtos);
    //  }
    //}
  }
}
