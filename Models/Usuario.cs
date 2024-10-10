using System.ComponentModel.DataAnnotations;

namespace LaChozaComercial.Models
{
    public class Usuario
    {
        [Required]
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        public string Contraseña { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public bool UserType { get; set; } // "Vendedor" o "Cliente"
    }
}
