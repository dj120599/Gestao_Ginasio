using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Api.Models;

namespace GinasioVitaFit.Api.Controllers;

public class PlanoController: Controller
{
    private readonly IVitaFitDbContext _context;
    private readonly IMapper _mapper;

    public PlanoController(IVitaFitDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    }
    
    [HttpGet("/plano")]
    public async Task<IActionResult> GetPlanos()
    {
        if (_context.Planos is not null)
        {
            var categories = await _context.Planos
                .ToListAsync();
            
            if(categories.Any())
                return Ok(categories);
        }

        return NotFound();
    }
    
    
    [HttpDelete("plano_softdelete/{id}")]
    public async Task<IResult> DeletePlano_Soft(int id)
    {
        if (id == null)
            return Results.Empty;
        
        if (_context.Planos is not null)
        {
            var plano = await _context.Planos.FirstOrDefaultAsync(t => t.Id == id);

            if(plano is null)
                return Results.NotFound("Plano não foi encontrado");
            
            plano.IsDeleted = true;
            
            
            await _context.SaveChangesAsync();
            
            return Results.Ok("Plano apagado com Successo.");
        }
        return Results.Empty;
    }
}