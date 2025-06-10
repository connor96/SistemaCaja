namespace CajaSistema.Models
{
    public class PostergacionesCursosLista
    {
        public int idPostergacion { get; set; }
        public string? idMatricula { get; set; }
        public string? idPersona { get; set; }
        public string? descripcion { get; set; }
        public int estado { get; set; }
        public int idSede { get; set; }
        public string? periodo { get; set; }
        public string? codCurso { get; set; }
        public int idTransaccion { get; set; }
        public string? usuarioRegistro { get; set; }
        public DateTime fechaRegistro { get; set; }
    }
}
