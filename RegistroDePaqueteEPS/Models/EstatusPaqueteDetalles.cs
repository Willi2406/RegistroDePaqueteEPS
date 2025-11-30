using System.ComponentModel.DataAnnotations;

namespace RegistroDePaqueteEPS.Models;

public class EstatusPaqueteDetalles
{
    [Key]
    public int EstatusPaqueteDetalleId { get; set; }
    public int PaqueteId { get; set; }
    public int EstatusPaqueteId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
}
