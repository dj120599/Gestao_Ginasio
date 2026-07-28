using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Shared.Models;

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
    
     [HttpGet("/Aulas")]
    public async Task<IActionResult> GetAulas()
    {
        if (_context.Aulas is not null)
        {
            
            var aulas = await _context.Aulas.
                Where(a => a.IsDeleted.Equals(false)).
                Include(a => a.Instrutor).
                Include(a => a.Modalidade).
                Include(a => a.Sala).
                ToListAsync();
            
            List<AulaDto> Aulasmapped = _mapper.Map<List<AulaDto>>(aulas);
            
            if(Aulasmapped.Any())
                return Ok(Aulasmapped);
        }

        return NotFound();
    }
    
    [HttpGet("/Aula/{id}")]
    public async Task<IActionResult> GetAula(int id)
    {
        if (_context.Aulas is not null)
        {
            var aula = await _context.Aulas.
                Include(a => a.Instrutor).
                Include(a => a.Modalidade).
                Include(a => a.Sala).
                FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted.Equals(false));
            
            var aulamapped = _mapper.Map<Aula,AulaDto>(aula);
            
            if(aulamapped is not null)
                return Ok(aulamapped);
        }

        return NotFound();
    }
    
    //Metodo POSt com mapeamento automatico
    [HttpPost("/Aula")]
    public async Task<IResult> AddAula([FromBody] AulaDto? aula)
    {
        if (aula is  null)
            return Results.BadRequest();
        
        var aulamapped = _mapper.Map<AulaDto,Entities.Aula>(aula);
        aulamapped.CreatedDate = DateTime.UtcNow;
        aulamapped.UpdatedDate = DateTime.UtcNow;
        
        var products =  _context.Aulas;
        
        if (products is not null)
        {
            products.Add(aulamapped);
            
            try
            {
                await _context.SaveChangesAsync();
                return Results.Ok("AulaDto adicionada com Successo.");
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        }
        
        return Results.Empty;
    }
    
    [HttpPut("/Aula")]
    public async Task<IActionResult> UpdateAula([FromBody] AulaDto? aula)
    {
        if (aula is null)
            return Empty;
        
        if(_context.Aulas is null)
            return Empty;

        var oldproduct = await _context.Aulas.FirstOrDefaultAsync(a => a.Id == aula.Id);

        if(oldproduct is null)
            return NotFound("A AulaDto não foi encontrado");
        
        var aulamapped = _mapper.Map<AulaDto,Entities.Aula>(aula);
        aulamapped.Adapt(oldproduct);
        
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
        
        return Ok("AulaDto actualizada com sucesso.");
    }
    
    [HttpDelete("Aulasoftdelete/{id}")]
    public async Task<IResult> DeleteAula_Soft(int id)
    {
        if (id == null)
            return Results.Empty;
        
        if (_context.Aulas is not null)
        {
            var aula = await _context.Aulas.FirstOrDefaultAsync(t => t.Id == id);

            if(aula is null)
                return Results.NotFound("AulaDto não foi encontrado");
            
            aula.IsDeleted = true;
            
            if (_context.AulaSocios is not null)
            {
                var oldAulaSocios = await _context.AulaSocios.
                    Where(a => a.AulaId == id && a.IsDeleted.Equals(false)).
                    ToListAsync();

                if (oldAulaSocios is null)
                    return Results.NotFound("AulaDto Agendada não foi encontrado");

                foreach (AulaSocios oldAulaSocio in oldAulaSocios)
                {
                    AulaSocios _aulaSocio = oldAulaSocio;
                    
                    _aulaSocio.UpdatedDate = DateTime.UtcNow;
                    _aulaSocio.IsDeleted = true;
                    _aulaSocio.Adapt(oldAulaSocio);
                }
            }

            await _context.SaveChangesAsync();
            
            return Results.Ok("AulaDto apagada com Successo.");
        }
        return Results.Empty;
    }
}