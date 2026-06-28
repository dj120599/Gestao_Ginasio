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
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        
        //Kawany, Seque o video do prof para prencheres o que falta abaixo.
        optionsBuilder.UseSqlServer();
    }
    
    
}