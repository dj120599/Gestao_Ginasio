using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Shared.Models;

namespace GinasioVitaFit.Api.Controllers;

public class SocioController: Controller
{
    private readonly IVitaFitDbContext _context;
    private readonly IMapper _mapper;

    public SocioController(IVitaFitDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    }
    
    [HttpGet("/Socios")]
    public async Task<IActionResult> GetSocios()
    {
        if (_context.Socios is not null)
        {
            var socios = await _context.Socios.
                Where(a => a.IsDeleted.Equals(false)).
                Include(s => s.Plano).
                ToListAsync();
            
            
            List<SocioDto> SociosMapped = _mapper.Map<List<SocioDto>>(socios);
            
            if(SociosMapped.Any())
                return Ok(SociosMapped);
        }

        return NotFound();
    }
    
    [HttpGet("/Socio/{id}")]
    public async Task<IActionResult> GetSocio(int id)
    {
        if (_context.Socios is not null)
        {
            var socio = await _context.Socios.
                Include(s => s.Plano).
                FirstOrDefaultAsync(s => s.Id == id && s.IsDeleted.Equals(false));
            
            var SocioMapped = _mapper.Map<Socio,SocioDto>(socio);
            
            if(SocioMapped is not null)
                return Ok(SocioMapped);
        }

        return NotFound();
    }
//Metodo POSt com mapeamento automatico
    [HttpPost("/Socio")]
    public async Task<IResult> AddSocio([FromBody] SocioDto? socio)
    {
        if (socio is  null)
            return Results.BadRequest();
        
        var SocioMapped = _mapper.Map<SocioDto,Socio>(socio);
        
        SocioMapped.CreatedDate = DateTime.UtcNow;
        SocioMapped.UpdatedDate = DateTime.UtcNow;
        
        var products =  _context.Socios;
        
        if (products is not null)
        {
            products.Add(SocioMapped);
            
            try
            {
                await _context.SaveChangesAsync();
                return Results.Ok("SocioDto adicionado com Successo.");
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        }
        
        return Results.Empty;
    }
    
    [HttpPut("/Socio")]
    public async Task<IActionResult> UpdateSocio([FromBody] SocioDto? socio)
    {
        if (socio is null)
            return Empty;
        
        if(_context.Socios is null)
            return Empty;

        var SocioMapped = _mapper.Map<SocioDto,Socio>(socio);
        SocioMapped.UpdatedDate = DateTime.UtcNow;
        
        var oldproduct = await _context.Socios.FirstOrDefaultAsync(a => a.Id == SocioMapped.Id);

        if(oldproduct is null)
            return NotFound("O SocioDto não foi encontrado");
        
        SocioMapped.Adapt(oldproduct);
        
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
        
        return Ok("Sócio actualizada com sucesso.");
    }
    [HttpDelete("Sociosoftdelete/{id}")]
    public async Task<IResult> DeleteSocio_Soft(int id)
    {
        if (_context.Socios is not null)
        {
            // 1. Procura o sócio principal pelo ID recebido
            var socio = await _context.Socios.FirstOrDefaultAsync(t => t.Id == id);
        
            if (socio is null)
                return Results.NotFound("Sócio não foi encontrado");

            // 2. REQUISITO DO CARTÃO: Remove todas as linhas correspondentes na tabela AulaSocios
            if (_context.AulaSocios is not null)
            {
                var aulasDoIdSocio = await _context.AulaSocios
                    .Where(a => a.SocioId == id)
                    .ToListAsync();

                // Apaga em lote todas as inscrições deste sócio
                _context.AulaSocios.RemoveRange(aulasDoIdSocio);
            }

            // 3. Faz o Soft Delete do sócio principal
            socio.IsDeleted = true;
        
            // Grava todas as alterações na base de dados
            await _context.SaveChangesAsync();
        
            return Results.Ok("Sócio desativado e as suas inscrições em aulas foram removidas.");
        }
        return Results.Empty;
    }

}