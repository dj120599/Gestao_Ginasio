using System.Text.Json.Serialization;

namespace GinasioVitaFit.Shared.Models;

public class AulaSociosDto
{
    [JsonPropertyName("aulaId")]
    public int AulaId { get; set; }

    [JsonPropertyName("aula")]
    public AulaDto Aula { get; set; }
    
    [JsonPropertyName("socioId")]
    public int SocioId { get; set; }

    [JsonPropertyName("socio")]
    public SocioDto Socio { get; set; }
}