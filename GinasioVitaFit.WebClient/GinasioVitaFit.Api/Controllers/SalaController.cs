using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Api.Models;

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
    
    [HttpGet("/sala")]
    public async Task<IActionResult> GetSalas()
    {
        if (_context.Planos is not null)
        {
            var salas = await _context.Salas
                .ToListAsync();
            
            if(salas.Any())
                return Ok(salas);
        }

        return NotFound();
    }
}