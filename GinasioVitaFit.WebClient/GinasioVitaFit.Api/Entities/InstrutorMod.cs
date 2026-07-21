using Microsoft.EntityFrameworkCore;

namespace GinasioVitaFit.Api.Entities;

public class InstrutorMod
{
    public int InstrutorId { get; set; }
    public Instrutor Instrutor { get; set; }
    public int ModalidadeId { get; set; }
    public Modalidade Modalidade { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}