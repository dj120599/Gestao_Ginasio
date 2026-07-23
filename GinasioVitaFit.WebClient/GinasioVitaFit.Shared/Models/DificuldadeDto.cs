using System.Text.Json.Serialization;

namespace GinasioVitaFit.Shared.Models;

public class DificuldadeDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get ;set;}
}