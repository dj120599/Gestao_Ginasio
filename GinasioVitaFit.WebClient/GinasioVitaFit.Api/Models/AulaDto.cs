namespace GinasioVitaFit.Api.Models;

public class AulaDto
{
    public int Id { get; set; }
    public InstrutorDto Instrutores { get; set; }
    public ModalidadeDto Modalidades { get; set; }
    public SalaDto Sala { get; set; }
    public int Capacidade { get; set; }
    public DateTime AulaInicio { get; set; }
    public DateTime AulaFim { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}