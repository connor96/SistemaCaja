namespace CajaSistema.Models
{
    public class CajeroAsignacionCajero
    {
        public int idCajeroAsignacion { get; set; }
        public int idSede { get; set; }
        public string? periodo { get; set; }
        public string? usuarioCajero { get; set; }
        public bool estado { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string? usuarioRegistro { get; set; }
    }
}
