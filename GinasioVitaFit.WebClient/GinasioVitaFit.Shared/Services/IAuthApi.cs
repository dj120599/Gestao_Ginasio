using Refit;
using GinasioVitaFit.Shared.Models;
namespace GinasioVitaFit.Shared.Services;

public interface IAuthApi
{
    [Get("/aulas")]
    Task<List<Aula>> GetAulas();
    
    [Post("/aula")]
    Task<ApiResponse<Aula>> AddAula([Body] Aula aula);
}