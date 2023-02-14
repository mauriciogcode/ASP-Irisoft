using System.ComponentModel.DataAnnotations;

namespace Irisoft.Models
{
    public class VentaDiaria
    {

        public int Id { get; set; }
        [Required]
        public int Orden { get; set; }
        [Required(ErrorMessage ="El {0} es requerido")]
        [Display(Name ="Nombre de Cliente")]

        public string ClienteNombre { get; set; }
        [Display(Name = "Cantidad de unidades")]
        public int? Cantidad { get; set; }
        [Display(Name = "Tipo de Cristal")]
        public int? IdTipoCristal { get; set; }
        [Display(Name = "Orden de Laboratorio")]
        public int? OrdenLaboratorio { get; set; }
        [Display(Name = "Nombre de Laboratorio")]

        public int? IdLaboratorio { get; set; }
        [Display(Name = "Nombre de Armazon")]
        public string? Armazon { get; set; }
        [Display(Name = "Recibe")]
        public string? RecibeNombre { get; set; }
        [Display(Name = "Vendedor")]
        public int? IdEmpleado { get; set; }
        [Display(Name = "Nombre de Tarjeta de Credito")]
        public int? IdTarjeta { get; set; }
        [Display(Name = "Cuotas")]
        public int? Cuotas { get; set; }
        [Required(ErrorMessage ="El {0} es requerido")]
        [Range(1, 1000000, ErrorMessage ="El {0} es requerido")]
        [Display(Name ="Monto sin recibo")]
        public double? MontoSinRecibo { get; set; }
        [Required(ErrorMessage ="El {0} es requerido")]
        [Range(1, 1000000, ErrorMessage ="El {0} es requerido")]
        [Display(Name ="Monto en efectivo")]
        public double? MontoEfectivo { get; set; }
        [Required(ErrorMessage ="El {0} es requerido")]
        [Range(1, 1000000, ErrorMessage ="El {0} es requerido")]
        [Display(Name ="Monto con Tarjeta")]
        public double? MontoTarjeta { get; set; }
        [Required(ErrorMessage ="El {0} es requerido")]
        [Range(1, 1000000, ErrorMessage ="El {0} es requerido")]
        [Display(Name ="Monto con Transferencia")]
        public double? MontoTrasferencia { get; set; }
        [Required(ErrorMessage ="El {0} es requerido")]
        [Range(1, 1000000, ErrorMessage ="El {0} es requerido")]
        [Display(Name ="Monto Total")]
        public double? MontoTotal { get; set; }


    }
}
