using GinasioVitaFit.Shared.Models;
using Refit;

namespace GinasioVitaFit.Shared.Services;

public interface IGinasioVitaFitService
{
    //Aulas
    [Get("/Aulas")]
    Task<ApiResponse<List<AulaDto>>> GetAulas();
    
    [Post("/Aula")]
    Task<HttpResponseMessage> AddAula([Body] AulaDto aulaDto);
    
    [Put("/Aula")]
    Task<HttpResponseMessage> UpdateAula([Body] AulaDto aulaDto);
    
    [Get("/Aula/{id}")]
    Task<ApiResponse<AulaDto>> GetAula(int id);
    
    [Delete("/Aulasoftdelete/{id}")]
    Task<ApiResponse<string>> DeleteAula(int id);
    
    //InstrutorDto
    [Post("/Instrutor")]
    Task<HttpResponseMessage> AddInstrutor([Body] InstrutorDto novoInstrutorDto);
    
    [Get("/Instrutores")]
    Task<ApiResponse<List<InstrutorDto>>> GetAllInstrutores();

    [Put("/Instrutor")]
    Task<HttpResponseMessage> UpdateInstrutor([Body] InstrutorDto instrutorDto);
    
    [Delete("/instrutorsoftdelete/{id}")]
    Task<HttpResponseMessage> DeleteInstrutorSoft(int id);

    
    // ModalidadeDto

    [Post("/Modalidade")]
    Task<ApiResponse<string>> AddModalidade([Body] ModalidadeDto modalidadeDto);
    
    [Get("/Modalidades")]
    Task<ApiResponse<List<ModalidadeDto>>> GetAllModalidades();

    [Get("/Modalidade/{id}")]
    Task<ApiResponse<ModalidadeDto>> GetModalidade(int id);

    [Put("/Modalidade")]
    Task<ApiResponse<ModalidadeDto>> UpdateModalidade([Body] ModalidadeDto modalidadeDto);
    
    [Put("/Modalidadesoftdelete/{id}")]
    Task<ApiResponse<string>> DeleteModalidade_Soft(int id);
    
    // DificuldadeDto
    
    [Get("/Dificuldades")]
    Task<ApiResponse<List<DificuldadeDto>>> GetAllDificuldades();
    
    //Aulasocio
    [Get("/Sociosaula/{id}")]
    Task<ApiResponse<List<SocioDto>>> GetAllSociosFromAula(int id);

    [Get("/Aulassocio/{id}")]
    Task<ApiResponse<List<AulaSociosDto>>> GetAllAulasFromSocio(int id);

    [Get("/Aulasocio")]
    Task<ApiResponse<AulaSociosDto>> GetSocioFromAula(int id, int socio);

    [Post("/Aulasocio")]
    Task<ApiResponse<string>> AddSocioToAula([Body] AulaSociosDto aulasocio);
    
    [Put("/Aulasociosoftdelete")]
    Task<ApiResponse<string>> DeleteSocio_Soft([Body] AulaSociosDto aulasocio);
    
    //SalaDto
    [Get("/salas")]
    Task<ApiResponse<List<SalaDto>>> GetAllSalas();
    
    //Socios
    
    [Post("/Socio")] 
    Task<HttpResponseMessage> AddSocio([Body] SocioDto socioDto);
    
    [Get("/Socios")]
    Task<ApiResponse<List<SocioDto>>> GetSocios();
    
    [Get("/Socio/{id}")]
    Task<ApiResponse<SocioDto>> GetSocio(int id);

    [Put("/Socio")]
    Task<HttpResponseMessage> UpdateSocio([Body] SocioDto socioDto);

    [Put("/Sociosoftdelete/{id}")]
    Task<ApiResponse<string>> DeleteSocio_Soft(int id);
    
    //PlanoDto

    [Get("/Plano")]
    Task<ApiResponse<List<PlanoDto>>> GetPlanos();
}