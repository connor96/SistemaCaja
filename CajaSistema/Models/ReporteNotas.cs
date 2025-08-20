namespace CajaSistema.Models
{
    public class ReporteNotas
    {
        public string? Periodo { get; set; }
        public string? IdCurso { get; set; }
        public string? NotaFinal { get; set; }
        public string? FinalGrade { get; set; }
        public string? Docente { get; set; }
        public string? Local { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Periodos { get; set; }
        public string? CodInscripcion { get; set; }
        public string? Modalidad { get; set; }
        public bool Aprobado { get; set; }
    }
}
