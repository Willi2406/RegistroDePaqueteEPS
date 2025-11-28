using System.ComponentModel.DataAnnotations;

namespace RegistroDePaqueteEPS.Models;

public class Paquetes
{
    [Key]
    public int PaqueteId { get; set; }
    public int ClienteId { get; set; }
    public string NumeroRecepcion { get; set; }

    [Required(ErrorMessage = "El numero de tracking es requerido")]
    public string NumeroTracking { get; set; }

    [Required(ErrorMessage = "El suplidor es requerido")]
    public string Suplidor { get; set; }

    [Required(ErrorMessage = "El contenido es requerido")]
    public string Contenido {  get; set; }

    [Required(ErrorMessage = "El peso es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El peso debe ser mayor que cero")]
    public double Peso {  get; set; }

    [Required(ErrorMessage = "El total es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El peso debe ser mayor que cero")]
    public double Total { get; set; }

    public bool CondicionEspecial { get; set; }

    [Required(ErrorMessage = "La categoria es requerido")]
    public string Categoria { get; set; }

    public bool Retenido { get; set; }
}
