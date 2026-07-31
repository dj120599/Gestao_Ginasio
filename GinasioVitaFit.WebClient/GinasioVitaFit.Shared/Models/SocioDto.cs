using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GinasioVitaFit.Shared.Models;

public class SocioDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Nome é obrigatório.")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Data de nascimento é obrigatória.")]
    [JsonPropertyName("nascimento")]
    public DateOnly Nascimento { get; set; }
    
    [Required(ErrorMessage = "Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O formato do email incorreto.")]
    [JsonPropertyName("email")]
    public string Email { get; set; } = String.Empty;

    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; } = "/images/modalidade-placeholder.png";
    
    [Required(ErrorMessage = "Contacto telefónico é obrigatório.")]
    [Phone(ErrorMessage = "Formato do contacto incorreto.")]
    [JsonPropertyName("contacto")]
    public string Contacto { get; set; } = String.Empty;
    
    [Range(1, int.MaxValue, ErrorMessage = "Escolha um plano.")]
    [JsonPropertyName("planoId")]
    public int PlanoId { get; set; }

    [JsonPropertyName("plano")]
    public PlanoDto Plano { get; set; }
    
    [JsonPropertyName("subInicio")]
    public DateTime SubInicio { get; set; }
    
    [JsonPropertyName("subFim")]
    public DateTime SubFim { get; set; }
}