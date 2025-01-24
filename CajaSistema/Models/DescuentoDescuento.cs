using System.ComponentModel.DataAnnotations.Schema;

namespace CajaSistema.Models
{
    public class DescuentoDescuento
    {
        [Column("idDescuento")]
        public int idDescuento { get; set; }
        [Column("idPersona")]
        public string? idPersona { get; set; }
        [Column("idDescuentoConcepto")]
        public byte? idDescuentoConcepto { get; set; }
        [Column("monto")]
        public decimal? monto { get; set; }
        [Column("estado")]
        public int? estado { get; set; }
        [Column("usuarioRegistro")]
        public string? usuarioRegistro { get; set; }
    }
}
