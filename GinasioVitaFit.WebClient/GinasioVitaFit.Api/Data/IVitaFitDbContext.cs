using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Entities;

namespace GinasioVitaFit.Api.Data;

public interface IVitaFitDbContext
{
    public DbSet<Aula> Aulas { get; set; }
    public DbSet<Dificuldade> Dificuldades { get; set; }
    public DbSet<Instrutor> Instrutores { get; set; }
    public DbSet<Modalidade> Modalidades { get; set; }
    public DbSet<Plano> Planos { get; set; }
    public DbSet<Socio> Socios { get; set; }
    public DbSet<InstrutorMod> InstrutorMods { get; set; }
    public DbSet<AulaSocios> AulaSocios { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}