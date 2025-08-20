namespace CajaSistema.Models
{
    public class CertificadosLista
    {
        public int idCertificado { get; set; }
        public int idTransaccion { get; set; }
        public int idSede { get; set; }
        public string? idPersona { get; set; }
        public string? nombres { get; set; }
        public string? numeroContacto { get; set; }
        public string? correo { get; set; }
        public string? personaContacto { get; set; }
        public decimal monto { get; set; }
        public int estado { get; set; }
        public string? usuarioRegistro { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string? periodo { get; set; }
        public string? certificados { get; set; }

    }
}
