namespace GinasioVitaFit.Api.Models;

public class AulaDto
{
    public int Id { get; set; }
    public int InstrutorId { get; set; }
    public InstrutorDto Instrutor { get; set; }
    public int ModalidadeId { get; set; }
    public ModalidadeDto Modalidades { get; set; }
    public int SalaId { get; set; }
    public SalaDto Sala { get; set; }
    public int Capacidade { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public bool IsOpen { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}