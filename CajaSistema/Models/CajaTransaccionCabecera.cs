namespace CajaSistema.Models
{
    public class CajaTransaccionCabecera
    {
        public int IdTransaccionesPagadas { get; set; }
        public DateOnly FechaTransaccion { get; set; }
        public TimeOnly HoraTransaccion { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Modalidad { get; set; }
        public string? curso { get; set; }
        public string? Horario { get; set; }

    }
}
