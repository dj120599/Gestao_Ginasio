using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Api.Models;

namespace GinasioVitaFit.Api.Controllers;

public class ModalidadeController: Controller
{
    private readonly IVitaFitDbContext _context;
    private readonly IMapper _mapper;

    public ModalidadeController(IVitaFitDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    }
    
     [HttpGet("/modalidades")]
    public async Task<IActionResult> GetAllModalidades()
    {
        if (_context.Modalidades is not null)
        {
            var modalidades = await _context.Modalidades.
                Where(a => a.IsDeleted.Equals(false)).
                Include(a => a.Dificuldade).
                ToListAsync();
            
            if(modalidades.Any())
                return Ok(modalidades);
        }

        return NotFound();
    }
    
    [HttpGet("/modalidade/{id}")]
    public async Task<IActionResult> GetModalidade(int id)
    {
        if (_context.Modalidades is not null)
        {
            var modalidade = await _context.Modalidades.
                FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted.Equals(false));
            
            if(modalidade is not null)
                return Ok(modalidade);
        }

        return NotFound();
    }
    
    [HttpPost("/modalidade")]
    public async Task<IResult> AddModalidade([FromBody] ModalidadeDto? modalidade, [FromServices] IWebHostEnvironment env)
    {
        if (modalidade is  null)
            return Results.BadRequest("Dados inválidos.");
        
        var mapper = _mapper.Map<Models.ModalidadeDto,Entities.Modalidade>(modalidade);
        mapper.CreatedDate = DateTime.UtcNow;
        mapper.UpdatedDate = DateTime.UtcNow;
        
        // Verifica se o utilizador enviou uma imagem em Base64
        if (!string.IsNullOrEmpty(modalidade.ImageUrl) && modalidade.ImageUrl.StartsWith("data:image"))
        {
            try
            {
                // 1. Extrai os bytes puros do formato Base64
                var base64Data = modalidade.ImageUrl.Split(',')[1];
                var imageBytes = Convert.FromBase64String(base64Data);

                // 2. Descobre a extensão do ficheiro (.png, .jpg, etc.)
                var contentType = modalidade.ImageUrl.Split(';')[0].Split(':')[1];
                var extension = contentType == "image/png" ? ".png" : ".jpg";

                // 3. Gera um nome de ficheiro único para nunca haver duplicados
                var fileName = $"{Guid.NewGuid()}{extension}";
            
                // 4. Define o caminho físico onde o ficheiro vai ser guardado no servidor
                var uploadsFolder = Path.Combine(env.ContentRootPath, "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            
                var filePath = Path.Combine(uploadsFolder, fileName);

                // 5. Escreve os bytes no disco do servidor com o caminho absoluto completo
                await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                // 6. Guarda na Base de Dados apenas o caminho relativo para o Frontend ler
                mapper.ImageUrl = $"/uploads/{fileName}";
            }
            catch (Exception ex)
            {
                return Results.Problem($"Erro ao processar e salvar o ficheiro físico da imagem: {ex.Message}");
            }
        }
        
        var modalidades =  _context.Modalidades;
        
        if (modalidades is not null)
        {
            modalidades.Add(mapper);
            
            try
            {
                await _context.SaveChangesAsync();
                return Results.Ok("Products Added with Success.");
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        }
        
        return Results.Empty;
    }
    
    [HttpPut("/modalidade")]
    public async Task<IActionResult> UpdateModalidade([FromBody] AulaDto? modalidade)
    {
        if (modalidade is null)
            return Empty;
        
        if(_context.Modalidades is null)
            return Empty;

        var oldmodalidade = await _context.Modalidades.FirstOrDefaultAsync(a => a.Id == modalidade.Id);

        if(oldmodalidade is null)
            return NotFound("A Aula não foi encontrado");
        
        modalidade.Adapt(oldmodalidade);
        
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
        
        return Ok("Aula actualizada com sucesso.");
    }
    
    [HttpDelete("modalidade_softdelete/{id}")]
    public async Task<IResult> DeleteModalidade_Soft(int id)
    {
        if (id == null)
            return Results.Empty;
        
        if (_context.Modalidades is not null)
        {
            var modalidade = await _context.Aulas.FirstOrDefaultAsync(t => t.Id == id);

            if(modalidade is null)
                return Results.NotFound("Modalidade não foi encontrado");
            
            modalidade.IsDeleted = true;
            
            
            await _context.SaveChangesAsync();
            
            return Results.Ok("Modalidade apagada com Successo.");
        }
        return Results.Empty;
    }
}