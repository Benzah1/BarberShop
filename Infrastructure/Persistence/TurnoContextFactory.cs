using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Infrastructure.Persistence;

namespace Infrastructure.Persistence
{
    public class TurnoContextFactory : IDesignTimeDbContextFactory<TurnoContext>
    {
        public TurnoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TurnoContext>();

            // Usa tu connection string real aquí o desde config si prefieres
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=BarberDB;Trusted_Connection=True;TrustServerCertificate=True;");

            return new TurnoContext(optionsBuilder.Options);
        }
    }
}
