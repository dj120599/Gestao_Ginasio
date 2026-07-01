namespace GinasioVitaFit.Shared.Models;

public class Modalidade
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Dificuldade Dificuldade { get; set; }
    public string ImageUrl { get; set; }
    public bool IsDeleted { get; set; }
}