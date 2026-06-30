using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Api.Models;

namespace GinasioVitaFit.Api.Controllers;

public class DificudadeController: Controller
{
    private readonly IVitaFitDbContext _context;
    private readonly IMapper _mapper;

    public DificudadeController(IVitaFitDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    } 
    
    [HttpGet("/dificuldade")]
    public async Task<IActionResult> GetCategories()
    {
        if (_context.Dificuldades is not null)
        {
            var categories = await _context.Dificuldades
                .ToListAsync();
            
            if(categories.Any())
                return Ok(categories);
        }

        return NotFound();
    }
}