namespace GinasioVitaFit.Api.Entities;

public class Socio : BaseEntity
{
    public string Name { get; set; }
    public DateOnly Nascimento { get; set; }
    public string Email { get; set; }
    public string ImageUrl { get; set; } = "/images/modalidade-placeholder.png";
    public string Contacto { get; set; }
    public int PlanoId { get; set; }
    public Plano Plano { get; set; }
    public DateTime SubInicio { get; set; }
    public DateTime SubFim { get; set; }
}