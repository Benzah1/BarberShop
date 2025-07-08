using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class TurnoContext :DbContext
    {

        public TurnoContext(DbContextOptions<TurnoContext> options) : base(options)
        {

        }

        public DbSet<Turno> Turnos => Set<Turno>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Barber> Barbers => Set<Barber>();
        public DbSet<Service> Services => Set<Service>();

    }
}
