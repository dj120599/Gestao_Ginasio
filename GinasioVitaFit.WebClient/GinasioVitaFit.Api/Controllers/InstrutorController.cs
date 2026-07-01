using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Api.Models;

namespace GinasioVitaFit.Api.Controllers;

public class InstrutorController: Controller
{
    private readonly IVitaFitDbContext _context;
    private readonly IMapper _mapper;

    public InstrutorController(IVitaFitDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    }
    
     [HttpGet("/instrutores")]
    public async Task<IActionResult> GetInstrutores()
    {
        if (_context.Instrutores is not null)
        {
            var instrutores = await _context.Instrutores.
                Where(a => a.IsDeleted.Equals(false)).
                ToListAsync();
            
            if(instrutores.Any())
                return Ok(instrutores);
        }

        return NotFound();
    }
    
    [HttpGet("/instrutor/{id}")]
    public async Task<IActionResult> GetInstrutor(int id)
    {
        if (_context.Instrutores is not null)
        {
            var instrutor = await _context.Instrutores.
                FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted.Equals(false));
            
            if(instrutor is not null)
                return Ok(instrutor);
        }

        return NotFound();
    }
    
    //Metodo POSt com mapeamento automatico
    [HttpPost("/instrutor")]
    public async Task<IResult> AddInstrutor([FromBody] InstrutorDto? instrutor)
    {
        if (instrutor is  null)
            return Results.BadRequest();
        
        var mapper = _mapper.Map<Models.InstrutorDto,Entities.Instrutor>(instrutor);
        mapper.CreatedDate = DateTime.UtcNow;
        mapper.UpdatedDate = DateTime.UtcNow;
        
        var instrutores =  _context.Instrutores;
        
        if (instrutores is not null)
        {
            instrutores.Add(mapper);
            
            try
            {
                await _context.SaveChangesAsync();
                return Results.Ok("Instrutor adicionado com Successo.");
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        }
        
        return Results.Empty;
    }
    
    [HttpPut("/instrutor")]
    public async Task<IActionResult> UpdateInstrutor([FromBody] InstrutorDto? instrutor)
    {
        if (instrutor is null)
            return Empty;
        
        if(_context.Instrutores is null)
            return Empty;

        var oldinstrutor = await _context.Aulas.FirstOrDefaultAsync(a => a.Id == instrutor.Id);

        if(oldinstrutor is null)
            return NotFound("O Instrutor não foi encontrado");
        
        instrutor.Adapt(oldinstrutor);
        
        var result = await _context.SaveChangesAsync();
        
        try
        {
            if (result <= 0)
                return NotFound("Não foi possivel guardar os dados.");
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
        
        return Ok("Instrutor actualizada com sucesso.");
    }
    
    [HttpDelete("instrutor_softdelete/{id}")]
    public async Task<IResult> DeleteInstrutor_Soft(int id)
    {
        if (id == null)
            return Results.Empty;
        
        if (_context.Instrutores is not null)
        {
            var instrutor = await _context.Instrutores.FirstOrDefaultAsync(t => t.Id == id);

            if(instrutor is null)
                return Results.NotFound("Instrutor não foi encontrado");
            
            instrutor.IsDeleted = true;
            
            
            await _context.SaveChangesAsync();
            
            return Results.Ok("Instrutor apagado com Successo.");
        }
        return Results.Empty;
    }
    
    //InstrutorMod
    [HttpGet("/instrutormods/{id}")]
    public async Task<IActionResult> GetAllModalidadesFromInstrutor(int id)
    {
        if (_context.InstrutorMods is not null)
        {
            var modalidades = await _context.InstrutorMods.
                Where(a => a.InstrutorID == id && a.IsDeleted.Equals(false)).
                ToListAsync();
            
            if(modalidades.Any())
                return Ok(modalidades);
        }

        return NotFound();
    }

    [HttpPost("/instrutormod")]
    public async Task<IResult> AddModalidadeToInstrutor([FromBody] InstrutorModDto? instrutormod)
    {
        if (instrutormod is  null)
            return Results.BadRequest();
        
        var mapper = _mapper.Map<Models.InstrutorModDto,Entities.InstrutorMod>(instrutormod);
        mapper.CreatedDate = DateTime.UtcNow;
        mapper.UpdatedDate = DateTime.UtcNow;
        
        var instrutoremods =  _context.InstrutorMods;
        
        if (instrutoremods is not null)
        {
            instrutoremods.Add(mapper);
            
            try
            {
                await _context.SaveChangesAsync();
                return Results.Ok("Modalidade adicionada a aula com Successo.");
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        }
        
        return Results.Empty;
    }
    
    [HttpDelete("instrutormod_softdelete/{id,modalidade}")]
    public async Task<IResult> DeleteModalidade_Soft(int id, int modalidade)
    {
        if (id == null)
            return Results.Empty;
        
        if (modalidade == null)
            return Results.Empty;
        
        if (_context.InstrutorMods is not null)
        {
            var instrutor = await _context.InstrutorMods.FirstOrDefaultAsync(t => t.InstrutorID == id && t.ModalidadeID == modalidade);

            if(instrutor is null)
                return Results.NotFound("Modalidade não foi encontrado");
            
            instrutor.IsDeleted = true;
            
            
            await _context.SaveChangesAsync();
            
            return Results.Ok("Modalidade apagado com Successo.");
        }
        return Results.Empty;
    }
}