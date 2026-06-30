namespace GinasioVitaFit.Api.Entities;

public class Modalidade : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Dificuldade Dificuldade { get; set; }
    public string ImageUrl { get; set; }
}