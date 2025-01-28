using System.ComponentModel.DataAnnotations.Schema;

namespace CajaSistema.Models
{
    public class CajeroCajeroActivo
    {
        [Column("idCajeroActivo")]
        public int idCajeroActivo { get; set; }
        [Column("usuarioCajero")]
        public string? usuarioCajero { get; set; }
        [Column("usuarioRegistro")]
        public string? usuarioRegistro { get; set; }
        [Column("fechaRegistro")]
        public DateTime? fechaRegistro { get; set; }


    }
}
