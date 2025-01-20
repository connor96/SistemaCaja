using System.ComponentModel.DataAnnotations.Schema;

namespace CajaSistema.Models
{
    public class DescuentoListaDescuentos
    {
        [Column("IdDescuento")]
        public int IdDescuento { get; set; }
        [Column("DesDescuento")]
        public string DesDescuento { get; set; }
        [Column("Estado")]
        public int Estado { get; set; }
    }
}
