namespace GinasioVitaFit.Api.Models;

public class AulaSociosDto
{
    public int Id { get; set; }
    public int AulaID {get;set;}
    public int SocioID {get;set;}
    public bool IsDeleted { get; set; }
}