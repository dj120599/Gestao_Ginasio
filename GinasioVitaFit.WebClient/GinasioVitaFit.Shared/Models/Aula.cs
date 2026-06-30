namespace GinasioVitaFit.Shared.Models;

public class Aula
{

    public int AulaID { get; set; }

    public string Instrutor { get; set; }

    public string Modalidade { get; set; }

    public string Sala { get; set; }

    public int Capacidade { get; set; }

    public DateTime Inicio { get; set; }

    public DateTime Fim { get; set; }

    public bool Delete { get; set; } = false;
}