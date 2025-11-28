using Microsoft.AspNetCore.Identity;

namespace RegistroDePaqueteEPS.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? NombreCompleto { get; set; }
        public string? Provincia { get; set; }
        public string? Sucursal { get; set; }
        public string? Identificacion { get; set; }
        public string? Sexo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Telefono { get; set; }
        public string? Celular { get; set; }
    }

}
