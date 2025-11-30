using RegistroDePaqueteEPS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroDePaqueteEPS.Models;

public class AutorizadosEntrega
{
    [Key]
    public int AutorizadoEntregaId { get; set; }

    public string ClienteId { get; set; }

    [Required(ErrorMessage = "La identificacion es requerida")]
    public string Identificacion { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    public string Nombres { get; set; }

    [Required(ErrorMessage = "El telefono es requerido")]
    public string Telefono { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    public string Correo {  get; set; }

    [ForeignKey(nameof(ClienteId))]
    public virtual ApplicationUser Cliente { get; set; }
}