namespace GinasioVitaFit.Api.Models;

public class InstrutorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string ImageUrl { get; set; }
    public string Contacto { get; set; }
    public bool IsDeleted { get; set; }
}