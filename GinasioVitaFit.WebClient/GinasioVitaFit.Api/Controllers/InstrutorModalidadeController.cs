using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Shared.Models;

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
    
    [HttpGet("/Modalidadesinstr/{id}")]
    public async Task<IActionResult> GetAllModalidadesFromInstrutor(int id)
    {
        if (_context.InstrutorMods is not null)
        {
            var modalidades = await _context.InstrutorMods.
                Include(a => a.Instrutor).
                Include(a => a.Modalidade).
                Where(a => a.InstrutorId == id && a.IsDeleted.Equals(false)).
                ToListAsync();
            
            
            List<InstrutorModDto> Aulasmapped = _mapper.Map<List<InstrutorModDto>>(modalidades);
            
            if(Aulasmapped.Any())
                return Ok(Aulasmapped);
        }

        return NotFound();
    }

    [HttpPost("/Instrutormod")]
    public async Task<IResult> AddModalidadeToInstrutor([FromBody] InstrutorModDto? instrutormod)
    {
        if (instrutormod is  null)
            return Results.BadRequest();
        
        var MappedInstrutormod = _mapper.Map<InstrutorModDto,InstrutorMod>(instrutormod);
        
        var oldinstrutormod = await _context.InstrutorMods.
            FirstOrDefaultAsync(a => a.InstrutorId == MappedInstrutormod.InstrutorId
                                     && a.ModalidadeId == MappedInstrutormod.ModalidadeId
                                     && a.IsDeleted.Equals(true));
        
        var instrutoremods =  _context.InstrutorMods;
        
        if (instrutoremods is not null)
        {
            if (oldinstrutormod != null)
            {
                MappedInstrutormod.CreatedDate = DateTime.UtcNow;
                MappedInstrutormod.UpdatedDate = DateTime.UtcNow;
                MappedInstrutormod.IsDeleted = false;
                MappedInstrutormod.Adapt(oldinstrutormod);
            }
            else
            {
                MappedInstrutormod.CreatedDate = DateTime.UtcNow;
                MappedInstrutormod.UpdatedDate = DateTime.UtcNow;
                
                instrutoremods.Add(MappedInstrutormod);
            }
            
            try
            {
                await _context.SaveChangesAsync();
                return Results.Ok("ModalidadeDto adicionada ao InstrutorDto com Successo.");
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        }
        
        return Results.Empty;
    }
    
    [HttpDelete("instrutormod_softdelete/{id,modalidade}")]
    public async Task<IResult> DeleteModalidade_Soft(int id, int modalidade)
    {
        if (id == null)
            return Results.Empty;
        
        if (modalidade == null)
            return Results.Empty;
        
        if (_context.InstrutorMods is not null)
        {
            var instrutor = await _context.InstrutorMods.FirstOrDefaultAsync(t => t.InstrutorId == id && t.ModalidadeId == modalidade);

            if(instrutor is null)
                return Results.NotFound("ModalidadeDto não foi encontrado");
            
            instrutor.IsDeleted = true;
            
            
            await _context.SaveChangesAsync();
            
            return Results.Ok("ModalidadeDto apagado com Successo.");
        }
        return Results.Empty;
    }
}