namespace GinasioVitaFit.Shared.Models;

public class AulaSocios
{
    public int Id { get; set; }
    public int AulaID {get;set;}
    public int SocioID {get;set;}
    public bool IsDeleted { get; set;  }
}