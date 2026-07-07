using GinasioVitaFit.Api.Models;
using GinasioVitaFit.Shared.Models;
using Refit;

namespace GinasioVitaFit.Shared.Services;

public interface IGinasioVitaFitService
{
    [Get("/aulas")]
    Task<List<Aula>> GetAulas();
    
    [Post("/aula")]
    Task<ApiResponse<Aula>> AddAula([Body] Aula aula);
    
    [Put("/aula")]
    Task<ApiResponse<Aula>> UpdateAula([Body] Aula aula);
    
    [Get("/aula/{id}")]
    Task<Aula> GetAula(int id);
    
    [Delete("/aula/{id}")]
    Task<ApiResponse<string>> DeleteAula(int id);
    
    [Post("/Instrutor")]
    Task<ApiResponse<string>> AddInstrutor([Body] GinasioVitaFit.Shared.Models.Instrutor instrutor);
}