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
    
    //AulaSocios
    
    [HttpGet("/aulasocios/{id}")]
    public async Task<IActionResult> GetAllSociosFromAula(int id)
    {
        if (_context.AulaSocios is not null)
        {
            var socios = await _context.AulaSocios.
                Where(a => a.AulaID == id && a.IsDeleted.Equals(false)).
                ToListAsync();
            
            if(socios.Any())
                return Ok(socios);
        }

        return NotFound();
    }
    
    [HttpGet("/aulasocio/{id,socio}")]
    public async Task<IActionResult> GetSocioFromAula(int id, int socio)
    {
        if (_context.AulaSocios is not null)
        {
            var socios = await _context.AulaSocios.
                Where(a => a.AulaID == id && a.SocioID == socio && a.IsDeleted.Equals(false)).
                ToListAsync();
            
            if(socios.Any())
                return Ok(socios);
        }

        return NotFound();
    }
    
    
    [HttpPost("/aulasocio")]
    public async Task<IResult> AddSocioToAula([FromBody] AulaSociosDto? aulasocio)
    {
        if (aulasocio is  null)
            return Results.BadRequest();
        
        var mapper = _mapper.Map<Models.AulaSociosDto,Entities.AulaSocios>(aulasocio);
        mapper.CreatedDate = DateTime.UtcNow;
        mapper.UpdatedDate = DateTime.UtcNow;
        
        var aulasocios =  _context.AulaSocios;
        
        if (aulasocios is not null)
        {
            aulasocios.Add(mapper);
            
            try
            {
                await _context.SaveChangesAsync();
                return Results.Ok("Socio adicionado á aula com Successo.");
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        }
        
        return Results.Empty;
    }
    
    [HttpDelete("aulasocio_softdelete/{id,socio}")]
    public async Task<IResult> DeleteSocio_Soft(int id, int socio)
    {
        if (id == null)
            return Results.Empty;
        
        if (socio == null)
            return Results.Empty;
        
        if (_context.AulaSocios is not null)
        {
            var instrutor = await _context.AulaSocios.FirstOrDefaultAsync(t => t.AulaID == id && t.SocioID == socio);

            if(instrutor is null)
                return Results.NotFound("Socio não foi encontrado");
            
            instrutor.IsDeleted = true;
            
            
            await _context.SaveChangesAsync();
            
            return Results.Ok("Socio apagado com Successo.");
        }
        return Results.Empty;
    }
}