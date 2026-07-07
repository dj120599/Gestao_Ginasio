using GinasioVitaFit.Shared.Models;
using Refit;

namespace GinasioVitaFit.Shared.Services;

public interface IGinasioVitaFitService
{
    [Get("/aulas")]
    Task<ApiResponse<List<Aula>>> GetAulas();
    
    [Post("/aula")]
    Task<ApiResponse<Aula>> AddAula([Body] Aula aula);
    
    [Put("/aula")]
    Task<ApiResponse<Aula>> UpdateAula([Body] Aula aula);
    
    [Get("/aula/{id}")]
    Task<Aula> GetAula(int id);
    
    [Delete("/aula/{id}")]
    Task<ApiResponse<string>> DeleteAula(int id);
    
    // Modalidade

    [Post("/modalidade")]
    Task<ApiResponse<string>> AddModalidade([Body] Modalidade modalidade);
    
    [Get("/modalidades")]
    Task<ApiResponse<List<Modalidade>>> GetAllModalidades();
    
    // Dificuldade
    
    [Get("/dificuldades")]
    Task<ApiResponse<List<Dificuldade>>> GetAllDificuldades();
    
    //Aulasocio e Instrutormod
    [Get("//sociosaula/{id}")]
    Task<ApiResponse<List<AulaSocios>>> GetAllSociosFromAula(int id);

    [Get("/aulassocio/{id}")]
    Task<ApiResponse<List<AulaSocios>>> GetAllAulasFromSocio(int id);

    [Get("/aulasocio")]
    Task<ApiResponse<AulaSocios>> GetSocioFromAula(int id, int socio);

    [Post("/aulasocio")]
    Task<ApiResponse<string>> AddSocioToAula([Body] AulaSocios? aulasocio);

    [Delete("/aulasocio_softdelete")]
    Task<ApiResponse<string>> DeleteSocio_Soft([Body] AulaSocios? aulasocio);
    

    [Get("/modalidadesinstr/{id}")]
    Task<ApiResponse<List<InstrutorMod>>> GetAllModalidadesFromInstrutor(int id);

    [Get("/instrutormods/{id}")]
    Task<ApiResponse<List<InstrutorMod>>> GetAllInstrutorFromModalidade(int id);

    [Post("/instrutormod")]
    Task<ApiResponse<InstrutorMod>> AddModalidadeToInstrutor([Body] InstrutorMod? instrutormod);

    [Delete("/instrutormod_softdelete")]
    Task<ApiResponse<string>> DeleteModalidade_Soft([Body] InstrutorMod? instrutormod);
}