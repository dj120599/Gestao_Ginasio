using Refit;
using GinasioVitaFit.Shared.Models;
namespace GinasioVitaFit.Shared.Services;

public interface IAuthApi
{
    [Get("/aulas")]
    Task<List<AulaDto>> GetAulas();
    
    [Post("/aulaDto")]
    Task<ApiResponse<AulaDto>> AddAula([Body] AulaDto aulaDto);
}