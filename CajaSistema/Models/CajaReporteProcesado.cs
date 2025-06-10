namespace CajaSistema.Models
{
    public class CajaReporteProcesado
    {
        public string? periodoTexto { get; set; }
        public int idTransaccionesPagadas { get; set; }
        public string? fechaTransaccion { get; set; }
        public string? horaTransaccion { get; set; }
        public string? tipoPago { get; set; }
        public string? dni { get; set; }
        public string? nombreCompleto { get; set; }
        public string? usuario { get; set; }
        public string? curso { get; set; }
        public decimal importe { get; set; }
        public decimal descuento { get; set; }
        public decimal pagoMinimo { get; set; }
        public string? banco { get; set; }
        public string? cuenta { get; set; }
        public string? servicio { get; set; }
        public string? numOperacion { get; set; }
    }
}
