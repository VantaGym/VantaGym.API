using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VantaGym.Dominio.Entidades;

namespace VantaGym.Repositorio.Configuracoes;

public class HistoricoTreinoConfiguracoes : IEntityTypeConfiguration<HistoricoTreino>
{
    public void Configure(EntityTypeBuilder<HistoricoTreino> builder)
    {
        builder.ToTable("HistoricoTreino").HasKey(x => x.ID);

        builder.Property(nameof(HistoricoTreino.ID)).HasColumnName("HistoricoTreinoID");
        builder.Property(nameof(HistoricoTreino.CargaKg)).HasColumnName("CargaKg").HasPrecision(6, 2).IsRequired(true);
        builder.Property(nameof(HistoricoTreino.RepeticoesFeitas)).HasColumnName("RepeticoesFeitas").IsRequired(true);
        builder.Property(nameof(HistoricoTreino.DataExecucao)).HasColumnName("DataExecucao").IsRequired(true);
        builder.Property(nameof(HistoricoTreino.FichaTreinoID)).HasColumnName("FichaTreinoID").IsRequired(true);
        builder.Property(nameof(HistoricoTreino.UsuarioID)).HasColumnName("UsuarioID").IsRequired(true);
        builder.Property(nameof(HistoricoTreino.ExercicioID)).HasColumnName("ExercicioID").IsRequired(true);

        builder.HasOne(x => x.FichaTreino)
            .WithMany()
            .HasForeignKey(x => x.FichaTreinoID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Usuario)
            .WithMany(x => x.HistoricoTreino)
            .HasForeignKey(x => x.UsuarioID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Exercicio)
            .WithMany()
            .HasForeignKey(x => x.ExercicioID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}