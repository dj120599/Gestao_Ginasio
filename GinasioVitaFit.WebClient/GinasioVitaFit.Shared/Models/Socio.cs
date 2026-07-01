namespace GinasioVitaFit.Shared.Models;

public class Socio
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Nascimento { get; set; }
    public string Email { get; set; }

    public string ImageUrl { get; set; } = "https://plus.unsplash.com/premium_photo-1746421978363-6e3ea7668a73?q=80&w=1934&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";
    public string Contacto { get; set; }
    public Plano Plano { get; set; }
    public DateTime SubInicio { get; set; }
    public DateTime SubFim { get; set; }
    public bool IsDeleted { get; set; }
    
}