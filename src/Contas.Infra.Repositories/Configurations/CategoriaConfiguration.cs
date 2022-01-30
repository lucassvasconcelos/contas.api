using Contas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infra.Repositories.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> categorias)
        {
            categorias.ToTable("categorias", "financeiro");

            categorias
                .HasKey(key => key.Id);

            categorias
                .Property(prop => prop.DataCriacao)
                .IsRequired();

            categorias
                .Property(prop => prop.DataUltimaAtualizacao)
                .IsRequired();

            categorias
                .Property(prop => prop.Nome)
                .IsRequired();

            categorias
                .Property(prop => prop.Descricao)
                .IsRequired();

            categorias
                .Property(prop => prop.Tipo)
                .IsRequired();

            categorias
                .HasOne(ho => ho.Usuario)
                .WithMany(wm => wm.Categorias)
                .HasForeignKey(fk => fk.IdUsuario)
                .HasConstraintName("FK_USUARIO_CATEGORIAS");
        }
    }
}