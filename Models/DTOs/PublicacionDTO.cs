using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LaChozaComercial.Models.DTOs
{
    public class PublicacionDTO
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string vendedorNombre { get; set; }
    }
}
