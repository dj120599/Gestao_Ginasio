namespace GinasioVitaFit.Api.Models;

public class AulaDto
{
    public int Id { get; set; }
    public InstrutorDto Instrutores { get; set; }
    public ModalidadeDto Modalidades { get; set; }
    public string Sala { get; set; }
    public int Capacidade { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public bool IsDeleted { get; set; }
}