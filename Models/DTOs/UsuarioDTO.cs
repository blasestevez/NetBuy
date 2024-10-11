using System.Net.Mail;

namespace LaChozaComercial.Models.DTOs
{
    public class UsuarioDTO
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public bool userType { get; set; }
    }
}
