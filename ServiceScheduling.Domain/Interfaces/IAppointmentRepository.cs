using ServiceScheduling.Domain.Entities;

namespace ServiceScheduling.Domain.Interfaces;

public interface IAppointmentRepository
{
    Task SaveAsync(Appointment appointment, CancellationToken cancellationToken = default);
    Task<List<Appointment>?> GetAllAsync(int skip = 0, int take = 10, CancellationToken cancellationToken = default);
    Task<Appointment?> GetByIdAsync(Guid appointmentId, CancellationToken cancellationToken = default);
    Task UpdateAsync(Appointment appointmentData, CancellationToken cancellationToken = default);
    Task DeleteAsync(Appointment appointmentData, CancellationToken cancellationToken = default);
}