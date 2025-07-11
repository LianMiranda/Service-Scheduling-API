using Microsoft.EntityFrameworkCore;
using ServiceScheduling.Domain.Entities;
using ServiceScheduling.Domain.Interfaces;
using ServiceScheduling.Infrastructure.Data;

namespace ServiceScheduling.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _context;

    public AppointmentRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task SaveAsync(Appointment appointment, CancellationToken cancellationToken = default)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Appointment>?> GetAllAsync(int skip = 0, int take = 10,
        CancellationToken cancellationToken = default)
    {
        var appointmentList = await _context.Appointments.AsNoTracking().Include(service => service.Service)
            .Include(client => client.Client).Skip(skip).Take(take).ToListAsync();

        return appointmentList;
    }

    public Task<Appointment?> GetByIdAsync(Guid appointmentId, CancellationToken cancellationToken = default)
    {
        var appointment = _context.Appointments.AsNoTracking().Include(service => service.Service)
            .Include(client => client.Client).FirstOrDefaultAsync(appointment => appointment.Id == appointmentId, cancellationToken);
        
        return appointment;
    }

    public async Task UpdateAsync(Appointment appointmentData, CancellationToken cancellationToken = default)
    {
        _context.Appointments.Update(appointmentData);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Appointment appointmentData, CancellationToken cancellationToken = default)
    {
        _context.Appointments.Remove(appointmentData);
        await _context.SaveChangesAsync(cancellationToken);
    }
}