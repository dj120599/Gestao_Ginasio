namespace GinasioVitaFit.Api.Models;

public class ModalidadeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DificuldadeDto Dificuldade { get; set; }
    public string ImageUrl { get; set; }
    public bool IsDeleted { get; set; }
}