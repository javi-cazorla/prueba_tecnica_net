using Microsoft.EntityFrameworkCore;
using prueba_tecnica_net.Entidades;

namespace prueba_tecnica_net.Persistencia
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Bank> Banks { get; set; } 
    }
}

