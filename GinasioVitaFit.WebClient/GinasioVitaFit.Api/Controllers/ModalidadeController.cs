using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Data;
using GinasioVitaFit.Api.Entities;
using GinasioVitaFit.Shared.Models;

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

    [HttpGet("/Modalidades")]
    public async Task<IActionResult> GetAllModalidades()
    {
        if (_context.Modalidades is not null)
        {
            var modalidades = await _context.Modalidades
                .Where(m => !m.IsDeleted)
                .Include(m => m.Dificuldade)
                .ToListAsync();

            List<ModalidadeDto> Modalidadesmapped = _mapper.Map<List<ModalidadeDto>>(modalidades);
            
            if (Modalidadesmapped.Any())
                return Ok(Modalidadesmapped);
        }

        return NotFound();
    }

    [HttpGet("/Modalidade/{id}")]
    public async Task<IActionResult> GetModalidade(int id)
    {
        if (_context.Modalidades is not null)
        {
            var modalidade = await _context.Modalidades
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);

            var ModalidadeMapped = _mapper.Map<Modalidade,ModalidadeDto>(modalidade);
            
            if (ModalidadeMapped is not null)
                return Ok(ModalidadeMapped);
        }

        return NotFound();
    }

    private async Task<string> ProcessarImagem(string imageUrl, IWebHostEnvironment env)
    {
        // 1. Separa o Nome Original do Ficheiro dos dados do Base64
        var parts = imageUrl.Split('|');
        var originalFileName = parts[0]; // ex: "futebol.png"
        var base64Raw = parts[1];

        var base64Data = base64Raw.Split(',');
        var imageBytes = Convert.FromBase64String(base64Data[1]);

        // 2. Define o caminho da pasta e o caminho final do ficheiro usando o nome original
        var uploadsFolder = Path.Combine(env.ContentRootPath, "wwwroot", "uploads");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var filePath = Path.Combine(uploadsFolder, originalFileName);

        // 3. VERIFICAÇÃO AUTOMÁTICA: Se o ficheiro NÃO existir no disco, grava-o
        if (!System.IO.File.Exists(filePath))
        {
            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);
        }
        // Se já existir, o código ignora a escrita física e reutiliza o ficheiro atual!

        // 4. O caminho com o nome limpo e original
        return $"/uploads/{originalFileName}";
    }

    [HttpPost("/Modalidade")]
    public async Task<IResult> AddModalidade([FromBody] ModalidadeDto? modalidade,
        [FromServices] IWebHostEnvironment env)
    {
        if (modalidade is null)
            return Results.BadRequest("Dados inválidos.");

        var mapper = _mapper.Map<ModalidadeDto, Modalidade>(modalidade);
        mapper.CreatedDate = DateTime.UtcNow;
        mapper.UpdatedDate = DateTime.UtcNow;

        if (!string.IsNullOrEmpty(modalidade.ImageUrl) && modalidade.ImageUrl.Contains("|"))
        {
            try
            {
                mapper.ImageUrl = await ProcessarImagem(modalidade.ImageUrl, env);
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
                return Results.Ok("ModalidadeDto Adicionada com Successo.");
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        }

        return Results.Empty;
    }

    [HttpPut("/Modalidade")]
    public async Task<IActionResult> UpdateModalidade([FromBody] ModalidadeDto? modalidade,
        [FromServices] IWebHostEnvironment env)
    {
        if (modalidade is null)
            return Empty;

        if (_context.Modalidades is null)
            return Empty;

        var ModalidadeMapped = _mapper.Map<ModalidadeDto,Modalidade>(modalidade);
        ModalidadeMapped.UpdatedDate = DateTime.UtcNow;
        
        var oldmodalidade = await _context.Modalidades.
            FirstOrDefaultAsync(a => a.Id == ModalidadeMapped.Id);

        
        if (oldmodalidade is null)
            return NotFound("A ModalidadeDto não foi encontrado");

        if (!string.IsNullOrEmpty(ModalidadeMapped.ImageUrl) && ModalidadeMapped.ImageUrl.Contains("|"))
        {
            try
            {
                ModalidadeMapped.ImageUrl = await ProcessarImagem(ModalidadeMapped.ImageUrl, env);
            }
            catch (Exception ex)
            {
                return Problem($"Erro ao processar o ficheiro físico da imagem: {ex.Message}");
            }
        }

        ModalidadeMapped.Adapt(oldmodalidade);
        
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

        return Ok("ModalidadeDto actualizada com sucesso.");
    }

    [HttpPut("Modalidadesoftdelete/{id}")]
    public async Task<IResult> DeleteModalidade_Soft(int id)
    {
        if (id == null)
            return Results.Empty;

        if (_context.Modalidades is not null)
        {
            var modalidade = await _context.Modalidades.FirstOrDefaultAsync(t => t.Id == id);

            if (modalidade is null)
                return Results.NotFound("ModalidadeDto não foi encontrado");

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

            return Results.Ok("ModalidadeDto apagada com Successo.");
        }

        return Results.Empty;
    }
}