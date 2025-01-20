using System.ComponentModel.DataAnnotations.Schema;

namespace CajaSistema.Models
{
    public class DescuentoDescuento
    {
        [Column("idDescuento")]
        public int idDescuento { get; set; }
        [Column("idPersona")]
        public string idPersona { get; set; }
        [Column("idDescuentoConcepto")]
        public int idDescuentoConcepto { get; set; }
        [Column("monto")]
        public double monto { get; set; }
        [Column("estado")]
        public string estado { get; set; }
        [Column("usuarioRegistro")]
        public string usuarioRegistro { get; set; }
    }
}
