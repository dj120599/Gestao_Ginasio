using System.ComponentModel.DataAnnotations;

namespace GinasioVitaFit.WebClient.Models;

public class Aula
{
    [Key]
    public int AulaId { get; set; }

    [Required]
    public string Instrutor { get; set; }

    [Required]
    public string Modalidade { get; set; }

    [Required]
    public string Sala { get; set; } = string.Empty;

    [Required]
    public int Capacidade { get; set; }

    [Required]
    public DateTime Inicio { get; set; }

    [Required]
    public DateTime Fim { get; set; }

    public bool Delete { get; set; }
}