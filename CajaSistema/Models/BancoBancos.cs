namespace CajaSistema.Models
{
    public class BancoBancos
    {
        public int idBanco { get; set; }
        public string? codigoBanco { get; set; }
        public string? descripcionBanco { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string? usuarioRegistro { get; set; }
    }
}
