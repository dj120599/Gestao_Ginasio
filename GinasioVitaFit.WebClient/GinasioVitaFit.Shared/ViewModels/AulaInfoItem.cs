namespace GinasioVitaFit.Shared.ViewModels;

public class AulaInfoItem
{
    public string Titulo { get; set; } = string.Empty;
    public string Valor { get; set; } = string.Empty;
    public string Icone { get; set; } = string.Empty;
    public bool IsBadge { get; set; }
    public string? BadgeColor { get; set; }
    public bool CirclePingColor { get; set; }
}