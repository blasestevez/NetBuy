using System.ComponentModel.DataAnnotations;

namespace LaChozaComercial.Models.DTOs
{
    public class CreatePublicacionRequestDTO
    {
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public string usuarioId { get; set; }
    }
}
