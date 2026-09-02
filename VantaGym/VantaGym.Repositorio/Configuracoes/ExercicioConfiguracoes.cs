using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VantaGym.Dominio.Entidades;

namespace VantaGym.Repositorio.Configuracoes;

public class ExercicioConfiguracoes : IEntityTypeConfiguration<Exercicio>
{
    public void Configure(EntityTypeBuilder<Exercicio> builder)
    {
        builder.ToTable("Exercicio").HasKey(x => x.ID);

        builder.Property(nameof(Exercicio.ID)).HasColumnName("ExercicioID");
        builder.Property(nameof(Exercicio.Nome)).HasColumnName("Nome").IsRequired(true);
        builder.Property(nameof(Exercicio.Series)).HasColumnName("Series").IsRequired(true);
        builder.Property(nameof(Exercicio.Repeticoes)).HasColumnName("Repeticoes").IsRequired(true);
        builder.Property(nameof(Exercicio.GrupoMuscular)).HasColumnName("GrupoMuscular").HasConversion<string>().IsRequired(true);
        builder.Property(nameof(Exercicio.Ativo)).HasColumnName("Ativo").IsRequired(true);

        builder.HasQueryFilter(x => x.Ativo);
    }
}