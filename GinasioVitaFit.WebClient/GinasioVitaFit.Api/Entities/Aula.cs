namespace GinasioVitaFit.Api.Entities;

public class Aula : BaseEntity
{
    public int InstrutorId { get; set; }
    public Instrutor Instrutor { get; set; }
    public int ModalidadeId { get; set; }
    public Modalidade Modalidade { get; set; }
    public int SalaId { get; set; }
    public Sala Sala { get; set; }
    public int Capacidade { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public bool IsOpen { get; set; }
}