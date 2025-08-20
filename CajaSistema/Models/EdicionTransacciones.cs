namespace CajaSistema.Models
{
    public class EdicionTransacciones
    {
        public int idTransaccion { get; set; }
        public string? tipo { get; set; }
        public string? descripcion { get; set; }
        public decimal monto { get; set; }
    }
}
