using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Shared.Models;

namespace GinasioVitaFit.Api.Controllers;

public class InstrutorController: Controller
{
    private readonly IVitaFitDbContext _context;
    private readonly IMapper _mapper;

    public InstrutorController(IVitaFitDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    }
    
     [HttpGet("/Instrutores")]
    public async Task<IActionResult> GetInstrutores()
    {
        if (_context.Instrutores is not null)
        {
            var instrutores = await _context.Instrutores.
                Where(a => a.IsDeleted.Equals(false)).
                ToListAsync();

            var mappedinstrutores = _mapper.Map<List<InstrutorDto>>(instrutores);
            
            for (int i = 0; i < mappedinstrutores.Count; i++)
            {
                mappedinstrutores[i].Modalidades = GetAllModalidades(instrutores[i].Id);
            }
            
            
            if(mappedinstrutores.Any())
                return Ok(mappedinstrutores);
        }

        return NotFound();
    }

    
    [HttpGet("/Instrutor/{id}")]
    public async Task<IActionResult> GetInstrutor(int id)
    {
        if (_context.Instrutores is not null)
        {
            var instrutor = await _context.Instrutores.
                FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted.Equals(false));
            
            var instrutormapped = _mapper.Map<Instrutor,InstrutorDto>(instrutor);
            
            instrutormapped.Modalidades = GetAllModalidades(instrutormapped.Id);;
            
            if(instrutormapped is not null)
                return Ok(instrutormapped);
        }

        return NotFound();
    }
    
    //Metodo POSt com mapeamento automatico
    [HttpPost("/Instrutor")]
       public async Task<IResult> AddInstrutor([FromBody] InstrutorDto? instrutor)
       {
           List<int> newmods = new List<int>();
           
           if (instrutor is  null)
               return Results.BadRequest();
           
           var instrutormapped = _mapper.Map<InstrutorDto,Entities.Instrutor>(instrutor);
           
           var instrutores =  _context.Instrutores;
           
           if (instrutores is not null)
           {
               foreach (var VARIABLE in instrutor.Modalidades)
               {
                   newmods.Add(VARIABLE.Id);
               }
               
               
               instrutormapped.CreatedDate = DateTime.UtcNow;
               instrutormapped.UpdatedDate = DateTime.UtcNow;
                
               instrutores.Add(instrutormapped);
           }
           
           var result = await _context.SaveChangesAsync();
           
           try
           {
               if (result <= 0)
                   return Results.NotFound("Não foi possivel guardar os dados.");
           }
           catch (Exception e)
           {
               return Results.NotFound(e.Message);
           }
               
           var lastinstrutor = _context.Instrutores.
               OrderBy(e => e.Id).
           LastOrDefault();
           
           int instrutorId = lastinstrutor.Id;

           foreach (int mod in newmods)
           {
               InstrutorMod im = new InstrutorMod
               {
                   InstrutorId = instrutorId,
                   ModalidadeId = mod
               };

               _context.InstrutorMods.Add(im);
           }
           
           await _context.SaveChangesAsync();
           return Results.Ok("InstrutorDto adicionado com Successo.");
       }
    
    [HttpPut("/Instrutor")]
    public async Task<IActionResult> UpdateInstrutor([FromBody] InstrutorDto? instrutor)
    {
        List<int> newmods = new List<int>();
        List<int> oldmods = new List<int>();
        
        
        if (instrutor is null)
            return Empty;
        
        var instrutormapped = _mapper.Map<InstrutorDto,Entities.Instrutor>(instrutor);
           
        var oldinstrutor = await _context.Instrutores.
            FirstOrDefaultAsync(a => a.Id == instrutormapped.Id
                                     && a.IsDeleted.Equals(false));
        
        List<ModalidadeDto> oldmodalidades = GetAllModalidades(oldinstrutor.Id);;
        
        
        var instrutores =  _context.Instrutores;

        if (instrutores is not null)
        {
            if (oldinstrutor != null)
            {
                
                foreach (var VARIABLE in oldmodalidades)
                {
                    oldmods.Add(VARIABLE.Id);
                }
                
                foreach (var VARIABLE in instrutor.Modalidades)
                {
                    newmods.Add(VARIABLE.Id);
                }
                
                instrutormapped.CreatedDate = oldinstrutor.CreatedDate;
                instrutormapped.UpdatedDate = DateTime.UtcNow;
                instrutormapped.IsDeleted = false;
                
                instrutormapped.Adapt(oldinstrutor);

            }
            else
            {
                return NotFound("O InstrutorDto não foi encontrado");
            }
        }
        
        List<int> ModstoDelete = oldmods.Except(newmods).ToList();
        List<int> ModstoAdd = newmods.Except(oldmods).ToList();

        foreach (var mtd in ModstoDelete)
        {
            var moddelete = await _context.InstrutorMods.
                FirstOrDefaultAsync(a => a.InstrutorId == instrutormapped.Id
                                         && a.ModalidadeId == mtd
                                         && a.IsDeleted.Equals(false));
            
            moddelete.IsDeleted = true;
        }
        
        foreach (var mta in ModstoAdd)
        {
            InstrutorMod im = new InstrutorMod
            {
                InstrutorId = instrutormapped.Id,
                ModalidadeId = mta
            };

            _context.InstrutorMods.Add(im);
        }
        
        var result = await _context.SaveChangesAsync();
        
        try
        {
            if (result <= 0)
                return NotFound("Não foi possivel guardar os dados.");
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
        
        return Ok("InstrutorDto actualizada com sucesso.");
    }
    
    [HttpDelete("/Instrutorsoftdelete/{id}")]
    public async Task<IResult> DeleteInstructor_Soft(int id)
    {
        if (id <= 0)
            return Results.BadRequest("ID inválido");
    
        if (_context.Instrutores is not null)
        {
            var instrutor = await _context.Instrutores.FirstOrDefaultAsync(t => t.Id == id);
    
            if (instrutor is null)
                return Results.NotFound("InstrutorDto não foi encontrado");
    
            // Regra em cascata para as aulas
            if (_context.Aulas is not null)
            {
                var aulasDoInstrutor = await _context.Aulas
                    .Where(a => a.InstrutorId == id && !a.IsDeleted)
                    .ToListAsync();
    
                foreach (var aula in aulasDoInstrutor)
                {
                    aula.IsDeleted = true;
                }
            }
    
            instrutor.IsDeleted = true;
            await _context.SaveChangesAsync();
    
            return Results.Ok("InstrutorDto e aulas desativados.");
        }
    
        return Results.NotFound("Erro no contexto");
    }
    
    List<ModalidadeDto> GetAllModalidades(int InstrutorId)
    {
        List<ModalidadeDto> Modalidades = new List<ModalidadeDto>();
        List<int> ModalidadesIds = new List<int>();
        
        var modalidades =  _context.InstrutorMods.
            Where(a => a.InstrutorId == InstrutorId && a.IsDeleted.Equals(false)).
            ToList();

        foreach (var modsid in modalidades)
        {
            ModalidadesIds.Add(modsid.ModalidadeId);
        }

        foreach (var ids in ModalidadesIds)
        {
            var modalidade = _context.Modalidades.
                Include(m => m.Dificuldade).
                FirstOrDefault(a => a.Id == ids);
            ModalidadeDto modalidademapped = _mapper.Map<Modalidade,ModalidadeDto>(modalidade);
            
            Modalidades.Add(modalidademapped);
        }
        
        return Modalidades;
    }
}