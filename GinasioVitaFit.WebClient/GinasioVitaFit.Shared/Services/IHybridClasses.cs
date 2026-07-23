using GinasioVitaFit.Shared.Models;
using Refit;

namespace GinasioVitaFit.Shared.Services;

public interface IHybridClasses
{
    [Get("//sociosaula/{id}")]
    Task<ApiResponse<List<AulaSociosDto>>> GetAllSociosFromAula(int id);

    [Get("/aulassocio/{id}")]
    Task<ApiResponse<List<AulaSociosDto>>> GetAllAulasFromSocio(int id);

    [Get("/aulasocio/{id,socio}")]
    Task<ApiResponse<AulaSociosDto>> GetSocioFromAula(int id, int socio);

    [Post("/aulasocio")]
    Task<ApiResponse<AulaSociosDto>> AddSocioToAula([Body] AulaSociosDto? aulasocio);

    [Delete("aulasocio_softdelete/{id,socio}")]
    Task<ApiResponse<string>> DeleteSocio_Soft(int id, int socio);
    

    [Get("//modalidadesinstr/{id}")]
    Task<ApiResponse<List<InstrutorModDto>>> GetAllModalidadesFromInstrutor(int id);

    [Get("/instrutormods/{id}")]
    Task<ApiResponse<List<InstrutorModDto>>> GetAllInstrutorFromModalidade(int id);

    [Post("/instrutormod")]
    Task<ApiResponse<InstrutorModDto>> AddModalidadeToInstrutor([Body] InstrutorModDto? instrutormod);

    [Delete("instrutormod_softdelete/{id,modalidade}")]
    Task<ApiResponse<string>> DeleteModalidade_Soft(int id, int modalidade);
}