using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Api.Models;

namespace GinasioVitaFit.Api.Controllers;

public class SocioController: Controller
{
    private readonly IVitaFitDbContext _context;
    private readonly IMapper _mapper;

    public SocioController(IVitaFitDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    }
    
    [HttpGet("/socios")]
    public async Task<IActionResult> GetSocios()
    {
        if (_context.Aulas is not null)
        {
            var socios = await _context.Socios.
                Where(a => a.IsDeleted.Equals(false)).
                Include(s => s.Plano).
                ToListAsync();
            
            if(socios.Any())
                return Ok(socios);
        }

        return NotFound();
    }
    
    [HttpGet("/socio/{id}")]
    public async Task<IActionResult> GetSocio(int id)
    {
        if (_context.Socios is not null)
        {
            var socio = await _context.Socios.
                FirstOrDefaultAsync(s => s.Id == id && s.IsDeleted.Equals(false));
            
            if(socio is not null)
                return Ok(socio);
        }

        return NotFound();
    }
//Metodo POSt com mapeamento automatico
    [HttpPost("/socio")]
    public async Task<IResult> AddSocio([FromBody] SocioDto? socio)
    {
        if (socio is  null)
            return Results.BadRequest();
        
        var mapper = _mapper.Map<Models.SocioDto,Entities.Socio>(socio);
        mapper.CreatedDate = DateTime.UtcNow;
        mapper.UpdatedDate = DateTime.UtcNow;
        
        var products =  _context.Socios;
        
        if (products is not null)
        {
            products.Add(mapper);
            
            try
            {
                await _context.SaveChangesAsync();
                return Results.Ok("Socio adicionado com Successo.");
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        }
        
        return Results.Empty;
    }
    
    [HttpPut("/socio")]
    public async Task<IActionResult> UpdateSocio([FromBody] SocioDto? socio)
    {
        if (socio is null)
            return Empty;
        
        if(_context.Socios is null)
            return Empty;

        var oldproduct = await _context.Socios.FirstOrDefaultAsync(a => a.Id == socio.Id);

        if(oldproduct is null)
            return NotFound("A Aula não foi encontrado");
        
        socio.Adapt(oldproduct);
        
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
        
        return Ok("Sócio actualizada com sucesso.");
    }
    
    [HttpDelete("socio_softdelete/{id}")]
    public async Task<IResult> DeleteAula_Soft(int id)
    {
        if (id == null)
            return Results.Empty;
        
        if (_context.Socios is not null)
        {
            var socio = await _context.Socios.FirstOrDefaultAsync(t => t.Id == id);

            if(socio is null)
                return Results.NotFound("Sócio não foi encontrado");
            
            socio.IsDeleted = true;
            
            
            await _context.SaveChangesAsync();
            
            return Results.Ok("Sócio apagado com Successo.");
        }
        return Results.Empty;
    }
}