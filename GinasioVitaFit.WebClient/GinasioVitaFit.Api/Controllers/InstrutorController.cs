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
    [HttpPost("/Instrutor")]
    public async Task<IActionResult> AddInstrutor([FromBody] Instrutor? instrutor)
    {
        if (instrutor == null)
        {
            return BadRequest();
        }

        // 1. Força a data no modelo para evitar campos nulos no SQL
        instrutor.CreatedDate = DateTime.Now;

        // 2. Mapeia o modelo recebido para a entidade do EF Core
        var novoInstrutor = _mapper.Map<Entities.Instrutor>(instrutor);

        // 3. Garante que a entidade também recebe a data
        novoInstrutor.CreatedDate = DateTime.Now;

        if (_context.Instrutores != null)
        {
            _context.Instrutores.Add(novoInstrutor);

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Instrutor adicionado com Sucesso.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro no SQL: {e.Message} -> {e.InnerException?.Message}");
            }
        }

        return NotFound();
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
}