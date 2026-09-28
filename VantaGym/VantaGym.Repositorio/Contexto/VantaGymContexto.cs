using Microsoft.EntityFrameworkCore;
using VantaGym.Dominio.Entidades;
using VantaGym.Repositorio.Configuracoes;

public class VantaGymContexto : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Exercicio> Exercicios { get; set; }
    public DbSet<FichaTreino> FichaTreinos { get; set; }
    public DbSet<HistoricoTreino> HistoricoTreinos { get; set; }

    public VantaGymContexto(DbContextOptions<VantaGymContexto> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioConfiguracoes());
        modelBuilder.ApplyConfiguration(new ExercicioConfiguracoes());
        modelBuilder.ApplyConfiguration(new FichaTreinoConfiguracoes());
        modelBuilder.ApplyConfiguration(new HistoricoTreinoConfiguracoes());
    }
}