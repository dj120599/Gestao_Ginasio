using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GinasioVitaFit.Shared.Models;

public class AulaDto
{
    [JsonPropertyName("id")] 
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Escolha um instrutorDto.")]
    [JsonPropertyName("instrutorId")] 
    public int InstrutorId { get; set; }

    [JsonPropertyName("instrutor")] 
    public InstrutorDto Instrutor { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Escolha uma modalidade.")]
    [JsonPropertyName("modalidadeId")] 
    public int ModalidadeId { get; set; }

    [JsonPropertyName("modalidade")] 
    public ModalidadeDto Modalidade { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Escolha uma sala.")]
    [JsonPropertyName("salaId")] 
    public int SalaId { get; set; }

    [JsonPropertyName("sala")] 
    public SalaDto Sala { get; set; }

    [Range(1, 100, ErrorMessage = "Capacidade da aula deve ser entre 1 e 100 alunos.")]
    [JsonPropertyName("capacidade")] 
    public int Capacidade { get; set; }

    [Required(ErrorMessage = "Horário início é obrigatório.")]
    [JsonPropertyName("inicio")] 
    public DateTime Inicio { get; set; }

    [Required(ErrorMessage = "Horário final é obrigatório.")]
    [JsonPropertyName("fim")] 
    public DateTime Fim { get; set; }
    
    [JsonPropertyName("isOpen")] 
    public bool IsOpen { get; set; }
}