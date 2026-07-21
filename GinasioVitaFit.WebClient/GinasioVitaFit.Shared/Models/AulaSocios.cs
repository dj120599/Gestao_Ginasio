using System.Text.Json.Serialization;

namespace GinasioVitaFit.Shared.Models;

public class AulaSocios
{
    [JsonPropertyName("aulaId")]
    public int AulaId { get; set; }
    
    [JsonPropertyName("aula")]
    public Aula Aula { get; set; }
    
    [JsonPropertyName("socioId")]
    public int SocioId { get; set; }

    [JsonPropertyName("socio")]
    public Socio Socio { get; set; }
    
    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }
    
    [JsonPropertyName("updatedDate")]
    public DateTime UpdatedDate { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}