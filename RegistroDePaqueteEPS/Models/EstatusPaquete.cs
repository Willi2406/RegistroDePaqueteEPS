using System.ComponentModel.DataAnnotations;

namespace RegistroDePaqueteEPS.Models;

public class EstatusPaquete
{
    [Key]
    public int EstatusPaqueteId {  get; set; }
    public string Descripcion { get; set; }
    public int Existencia { get; set; }
}
