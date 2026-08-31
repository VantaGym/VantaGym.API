using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VantaGym.Dominio.Entidades;

namespace VantaGym.Repositorio.Configuracoes;

public class UsuarioConfiguracoes : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuario").HasKey(x => x.ID);

        builder.Property(nameof(Usuario.ID)).HasColumnName("UsuarioID");
        builder.Property(nameof(Usuario.Nome)).HasColumnName("Nome").IsRequired(true);
        builder.Property(nameof(Usuario.Email)).HasColumnName("Email").IsRequired(true);
        builder.Property(nameof(Usuario.Senha)).HasColumnName("Senha").IsRequired(true);
        builder.Property(nameof(Usuario.Ativo)).HasColumnName("Ativo").IsRequired(true);
    }
}