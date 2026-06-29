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
        
       
        optionsBuilder.UseSqlServer("Data Source=SQL8005.site4now.net;Initial Catalog=db_acb35f_gestaoginasio;User Id=db_acb35f_gestaoginasio_admin;Password=2026Brazil;Encrypt=True;TrustServerCertificate=True");
    }
    
    
}