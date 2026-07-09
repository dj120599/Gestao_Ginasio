namespace GinasioVitaFit.Api.Models;

public class InstrutorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string ImageUrl { get; set; } = "/images/modalidade-placeholder.png";
    public string Contacto { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}