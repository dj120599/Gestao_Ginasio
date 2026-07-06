namespace GinasioVitaFit.Api.Entities;

public class Aula : BaseEntity
{
    public Instrutor Instrutor { get; set; }
    public string ImagemUrl { get; set; }
    public Modalidade Modalidade { get; set; }
    public Sala Sala { get; set; }
    public int Capacidade { get; set; }
    public DateTime AulaInicio { get; set; }
    public DateTime AulaFim { get; set; }
}