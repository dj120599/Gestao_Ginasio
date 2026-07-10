using Microsoft.EntityFrameworkCore;

namespace GinasioVitaFit.Api.Entities;

[Keyless]
public class InstrutorMod: BaseEntity2
{
    public int InstrutorID {get;set;}
    public int ModalidadeID {get;set;}
}