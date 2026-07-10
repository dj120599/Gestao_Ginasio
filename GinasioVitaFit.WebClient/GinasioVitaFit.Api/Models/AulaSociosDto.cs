namespace GinasioVitaFit.Api.Models;

public class AulaSociosDto
{
    public int AulaID {get;set;}
    public int SocioID {get;set;}
    public DateTime UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}