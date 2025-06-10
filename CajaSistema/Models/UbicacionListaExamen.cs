namespace CajaSistema.Models
{
    public class UbicacionListaExamen
    {
        public int IdExamenUbicacion { get; set; }
        public string? IdPersona { get; set; }
        public string? CodCurso { get; set; }
        public int Estado { get; set; }
        public string? Periodo { get; set; }
        public string? IdSede { get; set; }
        public string? UsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdTransaccion { get; set; }
    }
}
