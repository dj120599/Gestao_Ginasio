using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Api.Models;

namespace GinasioVitaFit.Api.Controllers;

public class ModalidadeController : Controller
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
                Where(m => !m.IsDeleted).
                Include(m => m.Dificuldade).
                ToListAsync();

            if (modalidades.Any())
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
                FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);

            if (modalidade is not null)
                return Ok(modalidade);
        }

        return NotFound();
    }

    [HttpPost("/modalidade")]
    public async Task<IResult> AddModalidade([FromBody] ModalidadeDto? modalidade, [FromServices] IWebHostEnvironment env)
    {
        if (modalidade is null)
            return Results.BadRequest("Dados inválidos.");

        var mapper = _mapper.Map<Models.ModalidadeDto, Entities.Modalidade>(modalidade);
        mapper.CreatedDate = DateTime.UtcNow;
        mapper.UpdatedDate = DateTime.UtcNow;

        if (!string.IsNullOrEmpty(modalidade.ImageUrl) && modalidade.ImageUrl.Contains("|"))
        {
            try
            {
                // 1. Separa o Nome Original do Ficheiro dos dados do Base64
                var parts = modalidade.ImageUrl.Split('|');
                var originalFileName = parts[0]; // ex: "futebol.png"
                var base64Raw = parts[1];

                var base64Data = base64Raw.Split(',');
                var imageBytes = Convert.FromBase64String(base64Data[1]);

                // 2. Define o caminho da pasta e o caminho final do ficheiro usando o nome original
                var uploadsFolder = Path.Combine(env.ContentRootPath, "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, originalFileName);

                // 3. VERIFICAÇÃO AUTOMÁTICA: Se o ficheiro NÃO existir no disco, grava-o
                if (!System.IO.File.Exists(filePath))
                {
                    await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);
                }
                // Se já existir, o código ignora a escrita física e reutiliza o ficheiro atual!

                // 4. Guarda na Base de Dados o caminho com o nome limpo e original
                mapper.ImageUrl = $"/uploads/{originalFileName}";
            }
            catch (Exception ex)
            {
                return Results.Problem($"Erro ao processar o ficheiro físico da imagem: {ex.Message}");
            }
        }

        var modalidades = _context.Modalidades;

        if (modalidades is not null)
        {
            modalidades.Add(mapper);

            try
            {
                await _context.SaveChangesAsync();
                return Results.Ok("Modalidade Adicionada com Successo.");
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

        if (_context.Modalidades is null)
            return Empty;

        var oldmodalidade = await _context.Modalidades.FirstOrDefaultAsync(a => a.Id == modalidade.Id);

        if(oldmodalidade is null)
            return NotFound("A Modalidade não foi encontrado");
        
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
        
        return Ok("Modalidade actualizada com sucesso.");
    }

    [HttpPut("modalidade_softdelete/{id}")]
    public async Task<IResult> DeleteModalidade_Soft(int id)
    {
        if (id == null)
            return Results.Empty;
        
        if (_context.Modalidades is not null)
        {
            var modalidade = await _context.Modalidades.FirstOrDefaultAsync(t => t.Id == id);

            if (modalidade is null)
                return Results.NotFound("Modalidade não foi encontrado");

            modalidade.IsDeleted = true;
            modalidade.UpdatedDate = DateTime.UtcNow;

            var aulas = await _context.Aulas
                .Where(a => a.ModalidadeId == id && !a.IsDeleted)
                .ToListAsync();

            foreach (var aula in aulas)
            {
                aula.IsDeleted = true;
                aula.UpdatedDate = DateTime.UtcNow;

                var aulaSocios = await _context.AulaSocios
                    .Where(x => x.AulaId == aula.Id && !x.IsDeleted)
                    .ToListAsync();

                foreach (var aulaSocio in aulaSocios)
                {
                    aulaSocio.IsDeleted = true;
                    aulaSocio.UpdatedDate = DateTime.UtcNow;
                }
            }
            
            var instrutorModalidades = await _context.InstrutorMods
                .Where(im => im.ModalidadeId == id && !im.IsDeleted)
                .ToListAsync();

            foreach (var instrutorModalidade in instrutorModalidades)
            {
                instrutorModalidade.IsDeleted = true;
                instrutorModalidade.UpdatedDate = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return Results.Ok("Modalidade apagada com Successo.");
        }

        return Results.Empty;
    }
}