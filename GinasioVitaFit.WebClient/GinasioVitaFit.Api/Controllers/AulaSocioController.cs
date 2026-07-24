using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Shared.Models;

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
    
    
    [HttpGet("/Sociosaula/{id}")]
    public async Task<IActionResult> GetAllSociosFromAula(int id)
    {
        if (_context.AulaSocios is not null)
        {
            var socios = await _context.AulaSocios
                .Where(a => a.AulaId == id && !a.IsDeleted)
                .Include(a => a.Socio)
                .Select(a => a.Socio)
                .Where(s => !s.IsDeleted)
                .ToListAsync();
            
            List<AulaSociosDto> Sociosmapped = _mapper.Map<List<AulaSociosDto>>(socios);
            
            if(Sociosmapped.Any())
                return Ok(Sociosmapped);
        }

        return NotFound();
    }
    
    [HttpGet("/Aulassocio/{id}")]
    public async Task<IActionResult> GetAllAulasFromSocio(int id)
    {
        if (_context.AulaSocios is not null)
        {
            
            var socios = await _context.AulaSocios.
                Where(a => a.SocioId == id && a.IsDeleted.Equals(false)).
                ToListAsync();

            List<AulaSociosDto> Aulasmapped = _mapper.Map<List<AulaSociosDto>>(socios);
            
            if(Aulasmapped.Any())
                return Ok(Aulasmapped);
        }

        return NotFound();
    }
    
    [HttpGet("/Aulasocio")]
    public async Task<IActionResult> GetSocioFromAula([FromBody] AulaSociosDto? aulasocio)
    {
        if (_context.AulaSocios is not null)
        {
            var socios = await _context.AulaSocios.FirstOrDefaultAsync(
                a => a.AulaId == aulasocio.AulaId && a.SocioId == aulasocio.SocioId && a.IsDeleted.Equals(false));
            
            var sociomapped = _mapper.Map<AulaSocios,AulaSociosDto>(socios);
            
            if(sociomapped != null)
                return Ok(sociomapped);
        }

        return NotFound();
    }


    [HttpPost("/Aulasocio")]
    public async Task<IResult> AddSocioToAula([FromBody] AulaSociosDto? aulasocio)
    {
        if (aulasocio is null)
            return Results.BadRequest();

        var aulasocios = _context.AulaSocios;

        if (aulasocios is not null)
        {
            var sociomapped = _mapper.Map<AulaSociosDto, AulaSocios>(aulasocio);
            sociomapped.UpdatedDate = DateTime.UtcNow;

            var oldsocios = await _context.AulaSocios.
                FirstOrDefaultAsync(a => a.AulaId == sociomapped.AulaId 
                                         && a.SocioId == sociomapped.SocioId 
                                         && a.IsDeleted.Equals(true));

            bool isopen = AulaIsOpen(sociomapped).Result;
            
            if (isopen)
            {
                if (oldsocios != null)
                {
                    sociomapped.UpdatedDate = DateTime.UtcNow;
                    sociomapped.IsDeleted = false;
                    sociomapped.Adapt(oldsocios);

                    await _context.SaveChangesAsync();
                    
                    isopen = AulaIsOpen(sociomapped).Result;

                    if (isopen == false)
                    {
                        var aula = await _context.Aulas.FirstOrDefaultAsync(
                            a => a.Id == sociomapped.AulaId
                            && a.IsDeleted.Equals(false));
                        
                        aula.IsOpen = false;
                        await _context.SaveChangesAsync();
                    }
                        
                    
                    return Results.Ok("SocioDto adicionado á AulaDto com Successo.");
                
                }
                else
                {
                    sociomapped.CreatedDate = DateTime.UtcNow;

                    aulasocios.Add(sociomapped);

                    try
                    {
                        await _context.SaveChangesAsync();
                    
                        isopen = AulaIsOpen(sociomapped).Result;

                        if (isopen == false)
                        {
                            var aula = await _context.Aulas.FirstOrDefaultAsync(
                                a => a.Id == sociomapped.AulaId
                                     && a.IsDeleted.Equals(true));
                        
                            aula.IsOpen = false;
                            await _context.SaveChangesAsync();
                        }
                        
                        return Results.Ok("SocioDto adicionado á AulaDto com Successo.");
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
    
    [HttpPut("Aulasociosoftdelete")]
    public async Task<IResult> DeleteSocio_Soft([FromBody] AulaSociosDto? aulasocio)
    {
        if (aulasocio.AulaId == null)
            return Results.Empty;
        
        if (aulasocio.SocioId == null)
            return Results.Empty;
        
        var sociomapped = _mapper.Map<AulaSociosDto, AulaSocios>(aulasocio);
        
        if (_context.AulaSocios is not null)
        {
            var oldAulaSocios = await _context.AulaSocios.
                FirstOrDefaultAsync(t => t.AulaId == sociomapped.AulaId 
                                         && t.SocioId == sociomapped.SocioId);
            
            if(oldAulaSocios is null)
                return Results.NotFound("Aula Agendada não foi encontrado");
            
            sociomapped.UpdatedDate = DateTime.UtcNow;
            sociomapped.IsDeleted = true;
            sociomapped.Adapt(oldAulaSocios);
            
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