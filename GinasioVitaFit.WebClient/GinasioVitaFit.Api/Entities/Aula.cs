namespace GinasioVitaFit.Api.Entities;

public class Aula : BaseEntity
{
    public Instrutor Instrutor { get; set; }
    public Modalidade Modalidade { get; set; }
    public string Sala { get; set; }
    public int Capacidade { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
}