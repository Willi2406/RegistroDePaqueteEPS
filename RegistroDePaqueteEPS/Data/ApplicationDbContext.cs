using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RegistroDePaqueteEPS.Models;

namespace RegistroDePaqueteEPS.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Paquetes> Paquetes { get; set; }

        public DbSet<Preavisos> Preavisos { get; set; }

        public DbSet<AutorizadosEntrega> AutorizadosEntrega { get; set; }

        public DbSet<DireccionesDelivery> DireccionesDelivery { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Paquetes>(entity =>
            {
                
                entity.HasKey(e => e.PaqueteId);

                // Cambio: printf('%08d', Id) asegura 8 dígitos
                entity.Property(e => e.NumeroRecepcion)
                      .HasComputedColumnSql("'MIO' || printf('%08d', PaqueteId)");
            });
        }
    }
}
