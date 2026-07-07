using GinasioVitaFit.Shared.Models;
using Refit;

namespace GinasioVitaFit.Shared.Services;

public interface IHybridClasses
{
    [Get("//sociosaula/{id}")]
    Task<ApiResponse<List<AulaSocios>>> GetAllSociosFromAula(int id);

    [Get("/aulassocio/{id}")]
    Task<ApiResponse<List<AulaSocios>>> GetAllAulasFromSocio(int id);

    [Get("/aulasocio/{id,socio}")]
    Task<ApiResponse<AulaSocios>> GetSocioFromAula(int id, int socio);

    [Post("/aulasocio")]
    Task<ApiResponse<AulaSocios>> AddSocioToAula([Body] AulaSocios? aulasocio);

    [Delete("aulasocio_softdelete/{id,socio}")]
    Task<ApiResponse<string>> DeleteSocio_Soft(int id, int socio);
    

    [Get("//modalidadesinstr/{id}")]
    Task<ApiResponse<List<InstrutorMod>>> GetAllModalidadesFromInstrutor(int id);

    [Get("/instrutormods/{id}")]
    Task<ApiResponse<List<InstrutorMod>>> GetAllInstrutorFromModalidade(int id);

    [Post("/instrutormod")]
    Task<ApiResponse<InstrutorMod>> AddModalidadeToInstrutor([Body] InstrutorMod? instrutormod);

    [Delete("instrutormod_softdelete/{id,modalidade}")]
    Task<ApiResponse<string>> DeleteModalidade_Soft(int id, int modalidade);
}