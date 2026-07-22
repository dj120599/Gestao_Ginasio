using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GinasioVitaFit.Shared.Models;

public class InstrutorDto
{
    [JsonPropertyName("Id")] public int Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório.")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Formato do email incorreto.")]
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("modalidades")] public List<ModalidadeDto> Modalidades { get; set; }

    [JsonPropertyName("imageUrl")] public string ImageUrl { get; set; } = "/images/modalidade-placeholder.png";

    [Required(ErrorMessage = "Contacto telefónico é obrigatório.")]
    [Phone(ErrorMessage = "Formato do contacto incorreto.")]
    [JsonPropertyName("contacto")]
    public string Contacto { get; set; }
}