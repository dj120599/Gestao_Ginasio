using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GinasioVitaFit.Shared.Models;

public class Modalidade
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O nome da modalidade é obrigatório.")]
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "A descrição da modalidade é obrigatória.")]
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Selecione uma dificuldade.")]
    [JsonPropertyName("dificuldadeId")]
    public int DificuldadeId { get; set; }
    
    [JsonPropertyName("dificuldade")]
    public Dificuldade Dificuldade { get; set; }
    
    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; } = "/images/modalidade-placeholder.png";
    
    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }
    
    [JsonPropertyName("updatedDate")]
    public DateTime UpdatedDate { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set;  }
    
}