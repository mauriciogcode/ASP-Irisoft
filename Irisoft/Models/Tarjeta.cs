using System.ComponentModel.DataAnnotations;

namespace Irisoft.Models
{
    public class Tarjeta
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Nombre de la Tarjeta")]
        public string Nombre { get; set; }
    }
}
