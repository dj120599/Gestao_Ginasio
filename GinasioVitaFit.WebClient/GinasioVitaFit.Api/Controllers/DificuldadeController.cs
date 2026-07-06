using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Api.Models;

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
    
    [HttpGet("/dificuldades")]
    public async Task<IActionResult> GetAllDificuldades()
    {
        if (_context.Dificuldades is not null)
        {
            var dificuldades = await _context.Dificuldades
                .ToListAsync();
            
            if(dificuldades.Any())
                return Ok(dificuldades);
        }

        return NotFound();
    }
}