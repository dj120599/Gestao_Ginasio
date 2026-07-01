namespace GinasioVitaFit.Shared.Models;

public class Socio
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Nascimento { get; set; }
    public string Email { get; set; }
    public string ImageUrl { get; set; }
    public string Contacto { get; set; }
    public Plano Plano { get; set; }
    public DateTime SubInicio { get; set; }
    public DateTime SubFim { get; set; }
    public bool IsDeleted { get; set; }
}