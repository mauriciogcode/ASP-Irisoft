using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Irisoft.Models
{
    public class Laboratorio
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El {0} es requerido")]
        [Display(Name = "Nombre del Laboratorio")]
        [Remote("VerificarExisteLaboratorio", "Laboratorios", ErrorMessage ="Error")]
        public string Nombre { get; set; }
    }
}
