using RegistroDePaqueteEPS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroDePaqueteEPS.Models;

public class Devoluciones
{
    [Key]
    public int DevolucionId{ get; set; }
    public string ClienteId { get; set; }
    public int PaqueteId { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "La razon es requerida")]
    public string Razon { get; set; }


    [ForeignKey(nameof(ClienteId))]
    public virtual ApplicationUser Cliente { get; set; }

    [ForeignKey(nameof(PaqueteId))]
    public virtual Paquetes Paquete { get; set; }
}
