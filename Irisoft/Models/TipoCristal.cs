using System.ComponentModel.DataAnnotations;

namespace Irisoft.Models
{
    public class TipoCristal
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Nombre del Tipo de Cristal")]
        [StringLength(maximumLength:35, MinimumLength =3, ErrorMessage ="La longitud del nombre debe estar entre los {2} y los {1} caracteres")]
        public string Nombre { get; set; }
        public string Descripcion { get; set;}
    }
}
