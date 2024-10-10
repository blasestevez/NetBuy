using System.ComponentModel.DataAnnotations;

namespace LaChozaComercial.Models
{
    public class Publicacion
    {
        [Required]
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public decimal Precio { get; set; }
        public string NombreVendedor { get; set; }
    }
}
