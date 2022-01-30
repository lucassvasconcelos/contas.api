using Contas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infra.Repositories.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> usuarios)
        {
            usuarios.ToTable("usuarios", "identity");

            usuarios
                .HasKey(key => key.Id);

            usuarios
                .Property(prop => prop.DataCriacao)
                .IsRequired();

            usuarios
                .Property(prop => prop.DataUltimaAtualizacao)
                .IsRequired();

            usuarios
                .Property(prop => prop.Nome)
                .IsRequired();

            usuarios
                .Property(prop => prop.Sobrenome)
                .IsRequired();

            usuarios
                .Property(prop => prop.DataNascimento)
                .HasColumnType("date")
                .IsRequired();
        }
    }
}