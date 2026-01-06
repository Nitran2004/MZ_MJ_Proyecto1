using System.ComponentModel.DataAnnotations;

namespace Proyecto1_MZ_MJ.Models
{
    public class Queja
    {
        public int QuejaId { get; set; }
        [Display(Name = "Tu nombre")]
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string? Nombre { get; set; }
        [Display(Name = "Queja")]


        [Required(ErrorMessage = "El campo Queja es obligatorio.")]
        public string? QuejaTexto { get; set; }

        [Display(Name = "Tipo de Prioridad")]
        public string? Prioridad { get; set; }

        [Required(ErrorMessage = "El campo Sector es obligatorio.")]

        public string? Sector { get; set; }
        [Display(Name = "Evidencia")]

        [Required(ErrorMessage = "El campo Evidencia es obligatorio.")]
        public string Foto { get; set; }
    }
}