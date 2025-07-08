namespace CajaSistema.Models
{
    public class CajaTransaccionDetalleCabecera
    {
        public int IdTransaccionesPagadas { get; set; }
        public string? IdPersona { get; set; }
        public string? DNI { get; set; }
        public string? Alumno { get; set; }
        public DateOnly FechaTransaccion { get; set; }
        public TimeOnly HoraTransaccion { get; set; }
        public string? CodigoBanco { get; set; }
        public string? CanalPago { get; set; }
        public string? FormaPago { get; set; }
        public string? NumOperacionBanco { get; set; }
        public string? Cuenta { get; set; }
        public decimal Monto { get; set; }
        public int IdSede { get; set; }
        public string? DetalleSede { get; set; }
        public List<CajaTransaccionDetalleCuerpo> DetallePago { get; set; }
        public string? Email { get;  set; }
        public string? Telefono { get;  set; }
        public string? exaUbicacion { get; set; }
        public string? observacion { get; set; }
    }
}
