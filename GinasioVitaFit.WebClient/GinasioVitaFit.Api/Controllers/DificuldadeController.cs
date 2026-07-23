using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Shared.Models;

namespace GinasioVitaFit.Api.Controllers;

public class DificuldadeController: Controller
{
    private readonly IVitaFitDbContext _context;
    private readonly IMapper _mapper;

    public DificuldadeController(IVitaFitDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    } 
    
    [HttpGet("/Dificuldades")]
    public async Task<IActionResult> GetAllDificuldades()
    {
        if (_context.Dificuldades is not null)
        {
            var dificuldades = await _context.Dificuldades
                .ToListAsync();
            
            List<DificuldadeDto> dificuldadesmapped = _mapper.Map<List<DificuldadeDto>>(dificuldades);
            
            if(dificuldadesmapped.Any())
                return Ok(dificuldadesmapped);
        }
        

        return NotFound();
    }
    
    
    [HttpDelete("Dificuldadesoftdelete/{id}")]
    public async Task<IResult> DeleteDificuldade_Soft(int id)
    {
        if (id == null)
            return Results.Empty;
        
        if (_context.Dificuldades is not null)
        {
            var dificuldade = await _context.Dificuldades.FirstOrDefaultAsync(t => t.Id == id);

            if(dificuldade is null)
                return Results.NotFound("DificuldadeDto não foi encontrado");
            
            dificuldade.IsDeleted = true;
            
            
            await _context.SaveChangesAsync();
            
            return Results.Ok("DificuldadeDto apagado com Successo.");
        }
        return Results.Empty;
    }
}