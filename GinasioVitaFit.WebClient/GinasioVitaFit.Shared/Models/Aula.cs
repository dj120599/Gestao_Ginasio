<<<<<<< HEAD:GinasioVitaFit.WebClient/GinasioVitaFit.WebClient/Models/Aula.cs
namespace GinasioVitaFit.WebClient.Models;
=======
using System.ComponentModel.DataAnnotations;

namespace GinasioVitaFit.Shared.Models;
>>>>>>> 83a36622a588474e7e7ffb93320cf9562ce2369d:GinasioVitaFit.WebClient/GinasioVitaFit.Shared/Models/Aula.cs

public class Aula
{
    
    public int AulaID { get; set; }
    
    public string Instrutor { get; set; }
    
    public string Modalidade { get; set; }

    public string Sala { get; set; }

    public int Capacidade { get; set; }

    public DateTime Inicio { get; set; }

    public DateTime Fim { get; set; }
    
    public bool Delete { get; set; } = false;
}