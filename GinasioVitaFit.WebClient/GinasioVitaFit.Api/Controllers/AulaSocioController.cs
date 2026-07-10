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
                Where(a => a.AulaId == id && a.IsDeleted.Equals(false)).
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
                Where(a => a.SocioId == id && a.IsDeleted.Equals(false)).
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
                a => a.AulaId == aulasocio.AulaID && a.SocioId == aulasocio.SocioID && a.IsDeleted.Equals(false));
            
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
    
    [HttpPut("aulasocio_softdelete")]
    public async Task<IResult> DeleteSocio_Soft([FromBody] AulaSociosDto? aulasocio)
    {
        if (aulasocio.AulaID == null)
            return Results.Empty;
        
        if (aulasocio.SocioID == null)
            return Results.Empty;
        
        if (_context.AulaSocios is not null)
        {
            var oldAulaSocios = await _context.AulaSocios.FirstOrDefaultAsync(t => t.AulaId == aulasocio.AulaID && t.SocioId == aulasocio.SocioID);
            
            if(oldAulaSocios is null)
                return Results.NotFound("Aula Agendada não foi encontrado");
            
            aulasocio.UpdatedDate = DateTime.UtcNow;
            aulasocio.IsDeleted = true;
            aulasocio.Adapt(oldAulaSocios);
            
            await _context.SaveChangesAsync();
            
            return Results.Ok("Socio apagado com Successo.");
        }
        return Results.Empty;
    }
}