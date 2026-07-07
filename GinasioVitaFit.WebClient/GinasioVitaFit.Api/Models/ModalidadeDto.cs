namespace GinasioVitaFit.Api.Models;

public class ModalidadeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int DificuldadeId { get; set; }
    public DificuldadeDto Dificuldade { get; set; }
    public string ImageUrl { get; set; } = "/images/modalidade-placeholder.png";
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}