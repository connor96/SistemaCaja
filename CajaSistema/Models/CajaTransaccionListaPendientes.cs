namespace CajaSistema.Models
{
    public class CajaTransaccionListaPendientes
    {
        public int idTransacciones { get; set; }
        public string? periodoTexto { get; set; }
        public int IdTransaccionesPagadas { get; set; }
        public DateOnly FechaTransaccion { get; set; }
        public TimeOnly HoraTransaccion { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Modalidad { get; set; }
        public string? curso { get; set; }
        public string? Horario { get; set; }
    }
}
