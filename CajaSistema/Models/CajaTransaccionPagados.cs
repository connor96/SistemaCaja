namespace CajaSistema.Models
{
    public class CajaTransaccionPagados
    {
        public string? periodoTexto { get; set; }
        public int IdTransaccionesPagadas { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public TimeOnly HoraTransaccion { get; set; }
        public decimal Monto { get; set; }
        public string? NombreCompleto { get; set; }
        public string? DNI { get; set; }
        public string? Modalidad { get; set; }
        public string? curso { get; set; }
        public string? Horario { get; set; }
        public string? tipoPago { get; set; }

    }
}
