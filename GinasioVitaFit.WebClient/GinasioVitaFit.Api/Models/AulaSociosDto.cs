namespace GinasioVitaFit.Api.Models;

public class AulaSociosDto
{
    public int AulaId {get;set;}
    public int SocioId {get;set;}
    public DateTime UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}