using Microsoft.EntityFrameworkCore;

namespace GinasioVitaFit.Api.Entities;

[Keyless]
public class AulaSocios: BaseEntity2
{
    public int AulaId {get;set;}
    public int SocioId {get;set;}
}