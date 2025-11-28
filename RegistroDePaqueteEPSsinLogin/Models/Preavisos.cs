using System.ComponentModel.DataAnnotations;

namespace RegistroDePaqueteEPS.Models;

public class Preavisos
{
    [Key]
    public int PreavisoId {  get; set; }

    public int ClienteId { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El numero de tracking es requerido")]
    public string NumeroTracking { get; set; }

    [Required(ErrorMessage = "El suplidor es requerido")]
    public string Tienda { get; set; }

    public string Transportista { get; set; }

    [Required(ErrorMessage = "El contenido es requerido")]
    public string Contenido { get; set; }

    [Required(ErrorMessage = "El valor es requerido")]
    public double Valor { get; set; }
}
