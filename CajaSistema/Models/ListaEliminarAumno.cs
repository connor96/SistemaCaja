namespace CajaSistema.Models
{
    public class ListaEliminarAumno
    {
        public int IdTransacciones { get; set; }
        public int IdTransaccionesTransacciones { get; set; }
        public string? tipoPago { get; set; }
        public DateOnly fechaEnvio { get; set; }
        public TimeOnly horaEnvio { get; set; }
        public string? periodo { get; set; }
        public bool estado { get; set; }
        public string? cajeroDatos { get; set; }
        public bool eliminado { get; set; }
        public string? cajaPagos { get; set; }
        public string? curso { get; set; }
        public bool eliminar { get; set; }
    }
}
