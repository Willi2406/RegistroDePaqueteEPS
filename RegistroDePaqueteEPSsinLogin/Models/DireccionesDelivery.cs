using System.ComponentModel.DataAnnotations;

namespace RegistroDePaqueteEPS.Models;

public class DireccionesDelivery
{
    [Key]
    public int DireccionDeliveryId { get; set; }

    public int ClienteId { get; set; }

    public int AutorizadoEntregaId { get; set; }

    [Required]
    public string Provincia { get; set; }

    [Required]
    public string Municipio { get; set; }

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
}
