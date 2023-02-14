using System.ComponentModel.DataAnnotations;

namespace Irisoft.Models
{
    public class Empleado
    { public int Id { get; set; }
        [Required(ErrorMessage ="El {0} es requerido.")]
        [Display(Name = "Nombre del Empleado")]
        public string EmpleadoNombre { get; set; }
    }
}
