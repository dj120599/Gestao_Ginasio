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
    public string ImageUrl { get; set; } = "https://plus.unsplash.com/premium_photo-1746421978363-6e3ea7668a73?q=80&w=1934&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";
    
    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }
    
    [JsonPropertyName("UpdatedDate")]
    public DateTime UpdatedDate { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set;  }
    
}