using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Shared.Models;

namespace GinasioVitaFit.Api.Controllers;

public class SalaController: Controller
{
    private readonly IVitaFitDbContext _context;
    private readonly IMapper _mapper;

    public SalaController(IVitaFitDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    }
    
    [HttpGet("/salas")]
    public async Task<IActionResult> GetSalas()
    {
        if (_context.Planos is not null)
        {
            var salas = await _context.Salas
                .ToListAsync();
            
            List<SalaDto> SalasMapped = _mapper.Map<List<SalaDto>>(salas);
            
            if(SalasMapped.Any())
                return Ok(SalasMapped);
        }

        return NotFound();
    }
    
    
    [HttpDelete("sala_softdelete/{id}")]
    public async Task<IResult> DeleteSala_Soft(int id)
    {
        if (id == null)
            return Results.Empty;
        
        if (_context.Socios is not null)
        {
            var sala = await _context.Salas.FirstOrDefaultAsync(t => t.Id == id);

            if(sala is null)
                return Results.NotFound("SalaDto não foi encontrado");
            
            sala.IsDeleted = true;
            
            
            await _context.SaveChangesAsync();
            
            return Results.Ok("SalaDto apagado com Successo.");
        }
        return Results.Empty;
    }
}