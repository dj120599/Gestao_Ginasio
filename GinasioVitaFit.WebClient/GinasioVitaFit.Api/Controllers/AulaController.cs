using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Api.Models;

namespace GinasioVitaFit.Api.Controllers;

public class AulaController: Controller
{
    private readonly IVitaFitDbContext _context;
    private readonly IMapper _mapper;

    public AulaController(IVitaFitDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    }
    
     [HttpGet("/aulas")]
    public async Task<IActionResult> GetAulas()
    {
        if (_context.Aulas is not null)
        {
            var aulas = await _context.Aulas.
                Where(a => a.IsDeleted.Equals(false)).
                Include(a => a.Instrutor).
                Include(a => a.Modalidade).
                ToListAsync();
            
            if(aulas.Any())
                return Ok(aulas);
        }

        return NotFound();
    }
    
    [HttpGet("/aula/{id}")]
    public async Task<IActionResult> GetAula(int id)
    {
        if (_context.Aulas is not null)
        {
            var aulas = await _context.Aulas.
                FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted.Equals(false));
            
            if(aulas is not null)
                return Ok(aulas);
        }

        return NotFound();
    }
    
    //Metodo POSt com mapeamento automatico
    [HttpPost("/aula")]
    public async Task<IResult> AddAula([FromBody] AulaDto? aula)
    {
        if (aula is  null)
            return Results.BadRequest();
        
        var mapper = _mapper.Map<Models.AulaDto,Entities.Aula>(aula);
        mapper.CreatedDate = DateTime.UtcNow;
        mapper.UpdatedDate = DateTime.UtcNow;
        
        var products =  _context.Aulas;
        
        if (products is not null)
        {
            products.Add(mapper);
            
            try
            {
                await _context.SaveChangesAsync();
                return Results.Ok("Products Added with Success.");
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        }
        
        return Results.Empty;
    }
    
    [HttpPut("/aula")]
    public async Task<IActionResult> UpdateAula([FromBody] AulaDto? aula)
    {
        if (aula is null)
            return Empty;
        
        if(_context.Aulas is null)
            return Empty;

        var oldproduct = await _context.Aulas.FirstOrDefaultAsync(a => a.Id == aula.Id);

        if(oldproduct is null)
            return NotFound("A Aula não foi encontrado");
        
        aula.Adapt(oldproduct);
        
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
        
        return Ok("Aula actualizada com sucesso.");
    }
    
    [HttpDelete("aula_softdelete/{id}")]
    public async Task<IResult> DeleteAula_Soft(int id)
    {
        if (id == null)
            return Results.Empty;
        
        if (_context.Aulas is not null)
        {
            var aula = await _context.Aulas.FirstOrDefaultAsync(t => t.Id == id);

            if(aula is null)
                return Results.NotFound("Aula não foi encontrado");
            
            aula.IsDeleted = true;
            
            
            await _context.SaveChangesAsync();
            
            return Results.Ok("Aula apagada com Successo.");
        }
        return Results.Empty;
    }
}