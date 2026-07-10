using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Api.Models;

namespace GinasioVitaFit.Api.Controllers;

public class InstrutorModalidadeController : Controller
{
    private readonly IVitaFitDbContext _context;
    private readonly IMapper _mapper;

    public InstrutorModalidadeController(IVitaFitDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    }
    
    [HttpGet("/modalidadesinstr/{id}")]
    public async Task<IActionResult> GetAllModalidadesFromInstrutor(int id)
    {
        if (_context.InstrutorMods is not null)
        {
            var modalidades = await _context.InstrutorMods.
                Where(a => a.InstrutorId == id && a.IsDeleted.Equals(false)).
                ToListAsync();
            
            if(modalidades.Any())
                return Ok(modalidades);
        }

        return NotFound();
    }
}