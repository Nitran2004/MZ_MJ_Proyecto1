using System.ComponentModel.DataAnnotations;

namespace Proyecto1_MZ_MJ.Models
{
    public class Queja
    {
        public int QuejaId { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El campo Queja es obligatorio.")]
        public string? QuejaTexto { get; set; }

        public string? Foto { get; set; }
    }
}