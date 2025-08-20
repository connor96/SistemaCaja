namespace CajaSistema.Models
{
    public class EditarTransacciones
    {
        public int IdTransacciones { get; set; }
        public string? tipoPago { get; set; }
        public DateOnly fechaEnvio { get; set; }
        public TimeOnly horaEnvio { get; set; }
        public string? periodo { get; set; }
        public bool eliminar { get; set; }
        public decimal monto { get; set; }

    }
}
