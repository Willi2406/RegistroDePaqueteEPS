using RegistroDePaqueteEPS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroDePaqueteEPS.Models;

public class PreavisosExpress
{
    [Key]
    public int PreavisoId { get; set; }
    public string ClienteId { get; set; }
    public int? PaqueteId { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El numero de tracking es requerido")]
    public string NumeroTracking { get; set; }

    public bool Recibido { get; set; } = false;

    [ForeignKey(nameof(ClienteId))]
    public virtual ApplicationUser Cliente { get; set; }

    [ForeignKey(nameof(PaqueteId))]
    public virtual Paquetes Paquete { get; set; }
}
