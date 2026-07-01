namespace GinasioVitaFit.Shared.Models;

public class Modalidade
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Dificuldade Dificuldade { get; set; }
    public string ImageUrl { get; set; } = "https://plus.unsplash.com/premium_photo-1746421978363-6e3ea7668a73?q=80&w=1934&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";
    public bool IsDeleted { get; set;  }
    
}