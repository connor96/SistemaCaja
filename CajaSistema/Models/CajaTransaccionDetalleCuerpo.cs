namespace CajaSistema.Models
{
    public class CajaTransaccionDetalleCuerpo
    {
        public int IdTransaccionesDetalle { get; set; }
        public string? Descripcion { get; set; }
        public decimal Monto { get; set; }
        public string? curso { get; set; }
        public string? aula { get; set; }
        public string? horario { get; set; }
        public string? docente { get; set; }
    }
}
