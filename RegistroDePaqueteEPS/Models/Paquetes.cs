using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RegistroDePaqueteEPS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroDePaqueteEPS.Models;

public class Paquetes
{
    [Key]
    public int PaqueteId { get; set; }
    public string ClienteId { get; set; }
    public int? PreavisoId { get; set; }
    public string NumeroRecepcion { get; set; }

    [Required(ErrorMessage = "El numero de tracking es requerido")]
    public string NumeroTracking { get; set; }

    [Required(ErrorMessage = "El numero de EPS es requerido")]
    public string NumeroEPS { get; set; }

    [Required(ErrorMessage = "El suplidor es requerido")]
    public string Suplidor { get; set; }

    [Required(ErrorMessage = "El contenido es requerido")]
    public string Contenido {  get; set; }

    [Required(ErrorMessage = "El peso es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El peso debe ser mayor que cero")]
    public double Peso {  get; set; }

    public double Total { get; set; } = 0;

    public bool CondicionEspecial { get; set; }

    [Required(ErrorMessage = "La categoria es requerido")]
    public string Categoria { get; set; }
    public bool Retenido { get; set; }



    [ForeignKey(nameof(ClienteId))]
    public virtual ApplicationUser Cliente { get; set; }

    [ForeignKey(nameof(PreavisoId))]
    public virtual Preavisos Preaviso { get; set; }

    [ForeignKey(nameof(PaqueteId))]
    public virtual ICollection<EstatusPaqueteDetalles> EstatusPaquete { get; set; } = new List<EstatusPaqueteDetalles>();
}
