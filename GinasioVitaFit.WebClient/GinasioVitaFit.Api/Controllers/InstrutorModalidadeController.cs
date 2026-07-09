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
    
    [HttpGet("/instrutormods/{id}")]
    public async Task<IActionResult> GetAllInstrutorFromModalidade(int id)
    {
        if (_context.InstrutorMods is not null)
        {
            var modalidades = await _context.InstrutorMods.
                Where(a => a.ModalidadeId == id && a.IsDeleted.Equals(false)).
                ToListAsync();
            
            if(modalidades.Any())
                return Ok(modalidades);
        }

        return NotFound();
    }

    [HttpPost("/instrutormod")]
    public async Task<IResult> AddModalidadeToInstrutor([FromBody] InstrutorModDto? instrutormod)
    {
        if (instrutormod is  null)
            return Results.BadRequest();
        
        var mapper = _mapper.Map<Models.InstrutorModDto,Entities.InstrutorMod>(instrutormod);
        mapper.CreatedDate = DateTime.UtcNow;
        mapper.UpdatedDate = DateTime.UtcNow;
        
        var instrutoremods =  _context.InstrutorMods;
        
        if (instrutoremods is not null)
        {
            instrutoremods.Add(mapper);
            
            try
            {
                await _context.SaveChangesAsync();
                return Results.Ok("Modalidade adicionada a aula com Successo.");
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        }
        
        return Results.Empty;
    }
    
    [HttpDelete("instrutormod_softdelete")]
    public async Task<IResult> DeleteModalidade_Soft([FromBody] InstrutorModDto? instrutormod)
    {
        if (instrutormod.InstrutorId == null)
            return Results.Empty;
        
        if (instrutormod.ModalidadeId == null)
            return Results.Empty;
        
        if (_context.InstrutorMods is not null)
        {
            var instrutor = await _context.InstrutorMods.FirstOrDefaultAsync(t => t.InstrutorId == instrutormod.InstrutorId && t.ModalidadeId == instrutormod.ModalidadeId);

            if(instrutor is null)
                return Results.NotFound("Modalidade não foi encontrado");
            
            instrutor.IsDeleted = true;
            
            
            await _context.SaveChangesAsync();
            
            return Results.Ok("Modalidade apagado com Successo.");
        }
        return Results.Empty;
    }
}