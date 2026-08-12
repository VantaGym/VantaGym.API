using Microsoft.EntityFrameworkCore;
using VantaGym.Dominio.Entidades;
using VantaGym.Repositorio.Configuracoes;

public class VantaGymContexto : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Exercicio> Exercicios { get; set; }
    public DbSet<FichaTreino> FichaTreinos { get; set; }
    public DbSet<HistoricoTreino> HistoricoTreinos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-58UEHOH\\SQLEXPRESS;Database=VantaGym;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioConfiguracoes());
        modelBuilder.ApplyConfiguration(new ExercicioConfiguracoes());
        modelBuilder.ApplyConfiguration(new FichaTreinoConfiguracoes());
        modelBuilder.ApplyConfiguration(new HistoricoTreinoConfiguracoes());
    }
}