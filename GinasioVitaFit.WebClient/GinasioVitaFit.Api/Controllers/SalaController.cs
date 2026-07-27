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
    [HttpDelete("Saladelete/{id}")]
    public async Task<IResult> DeleteSala_Soft(int id)
    {
        if (_context.Salas is not null)
        {
            // 1. Procura a sala principal pelo ID recebido
            var sala = await _context.Salas.FirstOrDefaultAsync(t => t.Id == id);
        
            if (sala is null)
                return Results.NotFound("Sala não foi encontrada");

            // 2. REQUISITO: Remove as aulas associadas a esta sala
            if (_context.Aulas is not null)
            {
                var aulasDaSala = await _context.Aulas
                    .Where(a => a.SalaId == id)
                    .ToListAsync();

                _context.Aulas.RemoveRange(aulasDaSala);
            }

            // 3. Soft Delete: Apenas marca a sala como eliminada
            sala.IsDeleted = true;
        
            await _context.SaveChangesAsync();
        
            return Results.Ok("Sala e as suas respetivas aulas foram eliminadas.");
        }
        return Results.Empty;
    }
}