using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GinasioVitaFit.Shared.Models;

public class Socio
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Nome é obrigatório.")]
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [Required(ErrorMessage = "Data de nascimento é obrigatória.")]
    [JsonPropertyName("nascimento")]
    public DateOnly Nascimento { get; set; }
    
    [Required(ErrorMessage = "Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O formato do email incorreto.")]
    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; } = "/images/modalidade-placeholder.png";
    
    [Required(ErrorMessage = "Contacto telefónico é obrigatório.")]
    [Phone(ErrorMessage = "Formato do contacto incorreto.")]
    [JsonPropertyName("contacto")]
    public required string Contacto { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Escolha um plano.")]
    [JsonPropertyName("planoId")]
    public int PlanoId { get; set; }

    [JsonPropertyName("plano")]
    public Plano Plano { get; set; }
    
    [JsonPropertyName("subInicio")]
    public DateTime SubInicio { get; set; }
    
    [JsonPropertyName("subFim")]
    public DateTime SubFim { get; set; }
    
    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }
    
    [JsonPropertyName("updatedDate")]
    public DateTime UpdatedDate { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}