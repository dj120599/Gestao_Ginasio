using System.Text.Json.Serialization;

namespace GinasioVitaFit.Shared.Models;

public class InstrutorModDto
{
    [JsonPropertyName("instrutorId")] 
    public int InstrutorId { get; set; }
    
    [JsonPropertyName("instrutorDto")] 
    public InstrutorDto InstrutorDto { get; set; }
    
    [JsonPropertyName("modalidadeId")] 
    public int ModalidadeId { get; set; }
    
    [JsonPropertyName("modalidade")] 
    public ModalidadeDto Modalidade { get; set; }
}