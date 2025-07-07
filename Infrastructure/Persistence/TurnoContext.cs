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

    }
}
