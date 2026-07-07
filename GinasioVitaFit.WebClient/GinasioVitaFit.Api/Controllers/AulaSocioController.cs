using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Api.Models;

namespace GinasioVitaFit.Api.Controllers;

public class AulaSocioController : Controller
{
    private readonly IVitaFitDbContext _context;
    private readonly IMapper _mapper;

    public AulaSocioController(IVitaFitDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    }
    
    
    [HttpGet("/sociosaula/{id}")]
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
    
    [HttpGet("/aulassocio/{id}")]
    public async Task<IActionResult> GetAllAulasFromSocio(int id)
    {
        if (_context.AulaSocios is not null)
        {
            var socios = await _context.AulaSocios.
                Where(a => a.SocioID == id && a.IsDeleted.Equals(false)).
                ToListAsync();
            
            if(socios.Any())
                return Ok(socios);
        }

        return NotFound();
    }
    
    [HttpGet("/aulasocio/{id,socio}")]
    public async Task<IActionResult> GetSocioFromAula([FromBody] AulaSociosDto? aulasocio)
    {
        if (_context.AulaSocios is not null)
        {
            var socios = await _context.AulaSocios.FirstOrDefaultAsync(
                a => a.AulaID == aulasocio.AulaID && a.SocioID == aulasocio.SocioID && a.IsDeleted.Equals(false));
            
            if(socios != null)
                return Ok(socios);
        }

        return NotFound();
    }
    
    
    [HttpPost("/aulasocio")]
    public async Task<IResult> AddSocioToAula([FromBody] AulaSociosDto? aulasocio)
    {
        if (aulasocio is  null)
            return Results.BadRequest();
        
        var aulaexist = await _context.AulaSocios.FirstOrDefaultAsync(t => t.AulaID == aulasocio.AulaID && t.SocioID == aulasocio.SocioID);
        
        if (aulaexist != null)
        {
            var newaula = aulaexist;
            newaula.UpdatedDate = DateTime.UtcNow;  
            newaula.IsDeleted = false;
            
            newaula.Adapt(aulaexist);
            
            var result = await _context.SaveChangesAsync();
        
            try
            {
                if (result <= 0)
                    return Results.NotFound("Não foi possivel guardar os dados.");
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        
            return Results.Ok("Product Updated with Success.");
        }
        else
        {
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
        }
        
        return Results.Empty;
    }
    
    [HttpDelete("aulasocio_softdelete")]
    public async Task<IResult> DeleteSocio_Soft([FromBody] AulaSociosDto? aulasocio)
    {
        if (aulasocio.AulaID == null)
            return Results.Empty;
        
        if (aulasocio.SocioID == null)
            return Results.Empty;
        
        if (_context.AulaSocios is not null)
        {
            var instrutor = await _context.AulaSocios.FirstOrDefaultAsync(t => t.AulaID == aulasocio.AulaID && t.SocioID == aulasocio.SocioID);

            if(instrutor is null)
                return Results.NotFound("Socio não foi encontrado");
            
            instrutor.IsDeleted = true;
            
            
            await _context.SaveChangesAsync();
            
            return Results.Ok("Socio apagado com Successo.");
        }
        return Results.Empty;
    }
}