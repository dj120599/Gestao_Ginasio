using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GinasioVitaFit.Api.Entities;

public class AulaSocios
{
    public int AulaId { get; set; }
    public int SocioId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}