namespace GinasioVitaFit.Api.Entities;

public class Instrutor : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string ImageUrl { get; set; } = "/images/modalidade-placeholder.png";
    public string Contacto { get; set; }
}