namespace CajaSistema.Models
{
    public class BancoFormasPago
    {
        public int idFormaPago { get; set; }
        public string? codigoFormaPago { get; set; }
        public string? descripcionFormaPago { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string? usuarioRegistro { get; set; }
    }
}
