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

                entity.Property(e => e.NumeroRecepcion)
                      .HasComputedColumnSql("'MIO' + RIGHT('00000000' + CAST(PaqueteId AS VARCHAR(10)), 8)");

               entity.HasOne(d => d.Preaviso)
              .WithOne(p => p.Paquete)
              .HasForeignKey<Preavisos>(p => p.PaqueteId)
              .OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.Entity<DireccionesDelivery>(entity =>
            {
                entity.HasKey(e => e.DireccionDeliveryId);

                entity.HasOne(d => d.AutorizadoEntrega)
                      .WithMany() // Autorizados no tiene una lista de direcciones en su modelo, así que vacío.
                      .HasForeignKey(d => d.AutorizadoEntregaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Preavisos>(entity =>
            {
                entity.HasKey(e => e.PreavisoId);
            });

            builder.Entity<EstatusPaqueteDetalles>(entity =>
            {
                entity.HasKey(e => e.EstatusPaqueteDetalleId);

                entity.HasOne<Paquetes>()
                      .WithMany(p => p.EstatusPaquete)
                      .HasForeignKey(d => d.PaqueteId)
                      .OnDelete(DeleteBehavior.Cascade);
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
