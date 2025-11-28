using Microsoft.EntityFrameworkCore;
using RegistroDePaqueteEPS.Models;

namespace RegistroDePaqueteEPS.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Paquetes> Paquetes { get; set; }

    public DbSet<Preavisos> Preavisos { get; set; }

    public DbSet<AutorizadosEntrega> AutorizadosEntrega { get; set; }

    public DbSet<DireccionesDelivery> DireccionesDelivery { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paquetes>(entity =>
        {
            entity.HasKey(e => e.PaqueteId);

            // Cambio: printf('%08d', Id) asegura 8 dígitos
            entity.Property(e => e.NumeroRecepcion)
                  .HasComputedColumnSql("'MIO' || printf('%08d', PaqueteId)");
        });
    }
}
