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
    
    public string ImagemUrl { get; set; } = "https://plus.unsplash.com/premium_photo-1746421978363-6e3ea7668a73?q=80&w=1934&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";

}