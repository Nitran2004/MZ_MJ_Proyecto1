using System.ComponentModel.DataAnnotations;

namespace Proyecto1_MZ_MJ.Models
{
    public class Evento
    {
        public int EventoId { get; set; }

        [Required(ErrorMessage = "El título del evento es obligatorio.")]
        [Display(Name = "Título del Evento")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha del evento es obligatoria.")]
        [Display(Name = "Fecha del Evento")]
        [DataType(DataType.DateTime)]
        public DateTime FechaEvento { get; set; }

        [Required(ErrorMessage = "La ubicación es obligatoria.")]
        [Display(Name = "Ubicación")]
        public string? Ubicacion { get; set; }

        [Display(Name = "Organizador")]
        public string? Organizador { get; set; }

        [Display(Name = "Tipo de Evento")]
        public string? TipoEvento { get; set; } // Conferencia, Taller, Reunión, Celebración, etc.

        [Display(Name = "Capacidad Máxima")]
        [Range(1, 10000, ErrorMessage = "La capacidad debe ser entre 1 y 10000")]
        public int? CapacidadMaxima { get; set; }

        [Display(Name = "Imagen del Evento")]
        public string? ImagenEvento { get; set; }

        [Display(Name = "Estado")]
        public bool Activo { get; set; } = true;

        [Display(Name = "Requiere Confirmación")]
        public bool RequiereConfirmacion { get; set; } = true;

        [Display(Name = "Notas Adicionales")]
        public string? NotasAdicionales { get; set; }
    }
}