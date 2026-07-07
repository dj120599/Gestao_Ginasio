namespace GinasioVitaFit.Shared.Models;

public class Aula
{

    public int Id { get; set; }

    public Instrutor Instrutor { get; set; }

    public Modalidade Modalidade { get; set; }

    public Sala Sala { get; set; }

    public int Capacidade { get; set; }

    public DateTime AulaInicio { get; set; }

    public DateTime AulaFim { get; set; }

    public bool IsDeleted { get; set; } = false;
}