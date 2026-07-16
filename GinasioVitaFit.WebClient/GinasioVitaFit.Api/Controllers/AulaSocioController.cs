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
                a => a.AulaId == aulasocio.AulaId && a.SocioId == aulasocio.SocioId && a.IsDeleted.Equals(false));
            
            if(socios != null)
                return Ok(socios);
        }

        return NotFound();
    }


    [HttpPost("/aulasocio")]
    public async Task<IResult> AddSocioToAula([FromBody] AulaSociosDto? aulasocio)
    {
        if (aulasocio is null)
            return Results.BadRequest();

        var aulasocios = _context.AulaSocios;

        if (aulasocios is not null)
        {
            var mapper = _mapper.Map<Models.AulaSociosDto, Entities.AulaSocios>(aulasocio);
            mapper.UpdatedDate = DateTime.UtcNow;

            var oldsocios = await _context.AulaSocios.FirstOrDefaultAsync(a => a.AulaId == mapper.AulaId &&
                                                                               a.SocioId == mapper.SocioId
                                                                               && a.IsDeleted.Equals(true));

            bool isopen = AulaIsOpen(mapper).Result;
            
            if (isopen)
            {
                if (oldsocios != null)
                {
                    aulasocio.UpdatedDate = DateTime.UtcNow;
                    aulasocio.IsDeleted = false;
                    aulasocio.Adapt(oldsocios);

                    await _context.SaveChangesAsync();
                    
                    isopen = AulaIsOpen(mapper).Result;

                    if (isopen == false)
                    {
                        var aula = await _context.Aulas.FirstOrDefaultAsync(
                            a => a.Id == aulasocio.AulaId
                            && a.IsDeleted.Equals(false));
                        
                        aula.IsOpen = false;
                        await _context.SaveChangesAsync();
                    }
                        
                    
                    return Results.Ok("Socio adicionado á Aula com Successo.");
                
                }
                else
                {
                    mapper.CreatedDate = DateTime.UtcNow;

                    aulasocios.Add(mapper);

                    try
                    {
                        await _context.SaveChangesAsync();
                    
                        isopen = AulaIsOpen(mapper).Result;

                        if (isopen == false)
                        {
                            var aula = await _context.Aulas.FirstOrDefaultAsync(
                                a => a.Id == mapper.AulaId
                                     && a.IsDeleted.Equals(true));
                        
                            aula.IsOpen = false;
                            await _context.SaveChangesAsync();
                        }
                        
                        return Results.Ok("Socio adicionado á Aula com Successo.");
                    }
                    catch (Exception e)
                    {
                        return Results.NotFound(e.Message);
                    }
                }
            }
        }
        return Results.Empty;
    }
    
    [HttpPut("aulasocio_softdelete")]
    public async Task<IResult> DeleteSocio_Soft([FromBody] AulaSociosDto? aulasocio)
    {
        if (aulasocio.AulaId == null)
            return Results.Empty;
        
        if (aulasocio.SocioId == null)
            return Results.Empty;
        
        if (_context.AulaSocios is not null)
        {
            var oldAulaSocios = await _context.AulaSocios.FirstOrDefaultAsync(t => t.AulaId == aulasocio.AulaId && t.SocioId == aulasocio.SocioId);
            
            if(oldAulaSocios is null)
                return Results.NotFound("Aula Agendada não foi encontrado");
            
            aulasocio.UpdatedDate = DateTime.UtcNow;
            aulasocio.IsDeleted = true;
            aulasocio.Adapt(oldAulaSocios);
            
            await _context.SaveChangesAsync();
            
            return Results.Ok("Aula desistida com Successo.");
        }
        return Results.Empty;
    }

    private async Task<bool> AulaIsOpen(AulaSocios aulasocio)
    {
        bool result = false;
        
        var aula = await _context.Aulas.FirstOrDefaultAsync(
            a => a.Id == aulasocio.AulaId && a.IsDeleted.Equals(false));

        int capacidade = aula.Capacidade;
            
        var listasocios = await  _context.AulaSocios.Where(
                a => a.AulaId == aulasocio.AulaId && a.IsDeleted.Equals(false)).
            ToListAsync();
            
        int inscritos = listasocios.Count();

        if (capacidade > inscritos)
            result = true;
        else if (capacidade == inscritos)
            result = false;

        return result;
    }
}