using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LaChozaComercial.Models
{
    public class Usuario : IdentityUser
    {
        [Required]
        public bool UserType { get; set; } // "Vendedor" o "Cliente"
    }
}
