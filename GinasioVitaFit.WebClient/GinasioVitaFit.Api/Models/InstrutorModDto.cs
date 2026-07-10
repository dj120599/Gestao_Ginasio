namespace GinasioVitaFit.Api.Models;

public class InstrutorModDto
{
    public int InstrutorId { get; set; }
    public int ModalidadeId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}