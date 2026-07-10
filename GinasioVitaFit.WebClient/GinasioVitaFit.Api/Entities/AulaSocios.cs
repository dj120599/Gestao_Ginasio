using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GinasioVitaFit.Api.Entities;


public class AulaSocios: BaseEntity2
{
    [Key]
    public int AulaId {get;set;}

    public int SocioId {get;set;}
}