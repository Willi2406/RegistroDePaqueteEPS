using RegistroDePaqueteEPS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroDePaqueteEPS.Models;

public class DireccionesDelivery
{
    [Key]
    public int DireccionDeliveryId { get; set; }

    public string ClienteId { get; set; }

    public int AutorizadoEntregaId { get; set; }

    [Required]
    public string Provincia { get; set; } = "Hermanas Mirabal";

    [Required]
    public string Municipio { get; set; } = "Salcedo";

    [Required(ErrorMessage = "El sector es requerido")]
    public string Sector { get; set; }

    [Required(ErrorMessage = "La calle es requerida")]
    public string Calle { get; set; }

    public string? Domicilio { get; set; }

    [Required(ErrorMessage = "Las referencias son requeridas")]
    public string Referencias { get; set; }

    public bool Principal { get; set; }

    [Required(ErrorMessage = "El alias es requerido")]
    public string Alias { get; set; }

    [ForeignKey(nameof(ClienteId))]
    public virtual ApplicationUser Cliente { get; set; }

    [ForeignKey(nameof(AutorizadoEntregaId))]
    public virtual AutorizadosEntrega AutorizadoEntrega { get; set; }
}
