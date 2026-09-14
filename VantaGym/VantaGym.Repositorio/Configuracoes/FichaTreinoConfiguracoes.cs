using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VantaGym.Dominio.Entidades;

namespace VantaGym.Repositorio.Configuracoes;

public class FichaTreinoConfiguracoes : IEntityTypeConfiguration<FichaTreino>
{
    public void Configure(EntityTypeBuilder<FichaTreino> builder)
    {
        builder.ToTable("FichaTreino").HasKey(x => x.ID);
        builder.Property(nameof(FichaTreino.ID)).HasColumnName("FichaTreinoID");
        builder.Property(nameof(FichaTreino.Nome)).HasColumnName("Nome").IsRequired(true);
        builder.Property(nameof(FichaTreino.Ativo)).HasColumnName("Ativo").IsRequired(true);
        builder.Property(nameof(FichaTreino.UsuarioID)).HasColumnName("UsuarioID").IsRequired(true);

        builder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey(x => x.UsuarioID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Exercicios)
            .WithOne(x => x.FichaTreino)
            .HasForeignKey(x => x.FichaTreinoID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => x.Ativo);
    }
}