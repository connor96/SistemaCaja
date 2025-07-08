namespace CajaSistema.Models
{
    public class BecadosAlumnoBecado
    {
        public int idBecado { get; set; }
        public string? idPersona { get; set; }
        public bool? estado { get; set; }
        public string? usuarioRegistro { get; set; }
        public DateTime? fechaRegistro { get; set; }
        public int idSede { get; set; }
    }
}
