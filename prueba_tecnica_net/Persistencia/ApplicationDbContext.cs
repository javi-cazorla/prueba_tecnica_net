using Microsoft.EntityFrameworkCore;
using prueba_tecnica_net.Entidades;

namespace prueba_tecnica_net.Persistencia
{
    internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        internal DbSet<Bank> Banks { get; set; } 
    }
}

