using System.Net.Mail;

namespace LaChozaComercial.Models.DTOs
{
    public class CreateUsuarioRequestDTO
    {
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool userType { get; set; }
    }
}
