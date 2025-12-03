using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RegistroDePaqueteEPS.Models;
using System.ComponentModel;
using System.Reflection.Emit;

namespace RegistroDePaqueteEPS.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Paquetes> Paquetes { get; set; }

        public DbSet<Preavisos> Preavisos { get; set; }

        public DbSet<AutorizadosEntrega> AutorizadosEntrega { get; set; }

        public DbSet<DireccionesDelivery> DireccionesDelivery { get; set; }

        public DbSet<EstatusPaquete> EstatusPaquete { get; set; }

        public DbSet<EstatusPaqueteDetalles> EstatusPaqueteDetalles { get; set; }

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

            builder.Entity<EstatusPaquete>(entity =>
            {
                entity.HasData(
                    new EstatusPaquete
                    {
                        EstatusPaqueteId = 1,
                        Descripcion = "Almacen de Origen",
                        Existencia = 0
                    },
                    new EstatusPaquete
                    {
                        EstatusPaqueteId = 2,
                        Descripcion = "Embarcado",
                        Existencia = 0
                    },
                    new EstatusPaquete
                    {
                        EstatusPaqueteId = 3,
                        Descripcion = "Linea Aerea",
                        Existencia = 0
                    },
                    new EstatusPaquete
                    {
                        EstatusPaqueteId = 4,
                        Descripcion = "Aduanas",
                        Existencia = 0
                    },
                    new EstatusPaquete
                    {
                        EstatusPaqueteId = 5,
                        Descripcion = "Centro de Distribucion",
                        Existencia = 0
                    },
                    new EstatusPaquete
                    {
                        EstatusPaqueteId = 6,
                        Descripcion = "Transito a la Oficina",
                        Existencia = 0
                    },
                    new EstatusPaquete
                    {
                        EstatusPaqueteId = 7,
                        Descripcion = "Disponible",
                        Existencia = 0
                    },
                    new EstatusPaquete
                    {
                        EstatusPaqueteId = 8,
                        Descripcion = "Entregado al Cliente",
                        Existencia = 0
                    }
                );
            });
        }
    }
}
