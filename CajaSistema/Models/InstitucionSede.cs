using System.ComponentModel.DataAnnotations.Schema;

namespace CajaSistema.Models
{
    public class InstitucionSede
    {
        public byte IdSede { get; set; }
        public string? Sede { get; set; }
        public byte Estado { get; set; }
    }
}
