using Microsoft.AspNetCore.Identity;

namespace RegistroDePaqueteEPS.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Provincia { get; set; } = "Hermanas Mirabal";
        public string Municipio { get; set; } = "Salcedo";
        public string? Identificacion { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Sexo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
    }

}
