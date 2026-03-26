
using System.ComponentModel.DataAnnotations;

namespace CajaSistema.Models
{
    public class PeriodoGraduacion
    {
        public int idPeriodoGraduacion { get; set; }
        [Required(ErrorMessage ="La descripcion es obligatoria")]
        public string? periodoDescripcion { get; set; }
        [Required(ErrorMessage ="El campo es obligatorio")]
        public bool estado { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string? usuarioRegistro { get; set; }

    }
}
