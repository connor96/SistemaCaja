namespace CajaSistema.Models
{
    public class BancoCanalesPago
    {
        public int idCanalBanco { get; set; }
        public string? codigoCanal { get; set; }
        public string? descripcionCanal { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string? usuarioRegistro { get; set; }
    }
}
