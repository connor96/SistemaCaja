using System.ComponentModel.DataAnnotations.Schema;

namespace CajaSistema.Models
{
    public class DescuentoListaDescuentos
    {
        [Column("IdDescuento")]
        public byte? IdDescuento { get; set; }

        [Column("DesDescuento")]
        public string? DesDescuento { get; set; }

        [Column("Estado")]
        public byte? Estado { get; set; }
    }
}
