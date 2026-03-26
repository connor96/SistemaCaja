using System.ComponentModel.DataAnnotations.Schema;

namespace CajaSistema.Models
{
    public class DescuentoLista
    {
        public int idDescuento { get; set; }
        public string? idPersona { get; set; }
        public string? nombresApellidos { get; set; }
        public string? fechaRegistro { get; set; }
        public string? estado { get; set; }
        public string? idmatricula { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal monto { get; set; }
        public string? descripcionDescuento { get; set; }
        public string? periodo { get; set; }
        public string? curso { get; set; }
        public string? notafinal { get; set; }
        public bool? Aprobado { get; set; }
    }
}
