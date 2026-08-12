using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VantaGym.Dominio.Entidades;

namespace VantaGym.Repositorio.Configuracoes;

public class UsuarioConfiguracoes : IEntityTypeConfiguration<Exercicio>
{
    public void Configure(EntityTypeBuilder<Exercicio> builder)
    {
        builder.ToTable("Usuario").HasKey(x => x.ID);

        builder.Property(nameof(Exercicio.ID)).HasColumnName("UsuarioID");
        builder.Property(nameof(Exercicio.Nome)).HasColumnName("Nome").IsRequired(true);
        builder.Property(nameof(Exercicio.Email)).HasColumnName("Email").IsRequired(true);
        builder.Property(nameof(Exercicio.Senha)).HasColumnName("Senha").IsRequired(true);

        builder.HasMany(u => u.FichasTreino)
               .WithOne(f => f.Usuario)
               .HasForeignKey(f => f.UsuarioID);

        builder.HasMany(u => u.HistoricoTreino)
               .WithOne(h => h.Usuario)
               .HasForeignKey(h => h.UsuarioID);
    }
}