namespace CajaSistema.Models
{
    public class RegistroActividad
    {
        public int intIdRegistro { get; set; }
        public string? strNombre { get; set; }
        public string? strColor { get; set; }
        public DateOnly strFechaRegistro { get; set; }
        public string? strUsuarioRegistro { get; set; }
    }
}
