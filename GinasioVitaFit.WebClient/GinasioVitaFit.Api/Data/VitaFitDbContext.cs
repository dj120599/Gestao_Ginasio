using Microsoft.EntityFrameworkCore;
using GinasioVitaFit.Api.Entities;

namespace GinasioVitaFit.Api.Data;

public class VitaFitDbContext: DbContext, IVitaFitDbContext
{
    public DbSet<Aula> Aulas { get; set; }
    public DbSet<Dificuldade> Dificuldades { get; set; }
    public DbSet<Instrutor> Instrutores { get; set; }
    public DbSet<Modalidade> Modalidades { get; set; }
    public DbSet<Plano> Planos { get; set; }
    public DbSet<Socio> Socios { get; set; }
    public DbSet<InstrutorMod> InstrutorMods { get; set; }
    public DbSet<AulaSocios> AulaSocios { get; set; }
    public DbSet<Sala> Salas { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder )
    {
        base.OnConfiguring(optionsBuilder);
        
       
        optionsBuilder.UseSqlServer("Data Source=SQL8012.site4now.net;Initial Catalog=db_acb35f_ginasiovitafit;User Id=db_acb35f_ginasiovitafit_admin;Password=2026Brazil;Encrypt=True;TrustServerCertificate=True;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Resolve o erro do AulaSocios definindo a chave composta
        modelBuilder.Entity<AulaSocios>()
            .HasKey(asoc => new { asoc.AulaId, asoc.SocioId });

        // Resolve o provável erro do InstrutorMod (ajuste os nomes das propriedades se forem diferentes)
        modelBuilder.Entity<InstrutorMod>()
            .HasKey(imod => new { imod.InstrutorId, imod.ModalidadeId });
    }
    
    
}