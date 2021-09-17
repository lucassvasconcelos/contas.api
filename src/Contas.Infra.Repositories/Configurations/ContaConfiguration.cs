using Contas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infra.Repositories.Configurations
{
    public class ContaConfiguration : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> contas)
        {
            contas.ToTable("contas");

            contas
                .HasKey(key => key.Id);

            contas
                .Property(prop => prop.DataCriacao)
                .IsRequired();

            contas
                .Property(prop => prop.DataUltimaAtualizacao)
                .IsRequired();

            contas
                .Property(prop => prop.Nome)
                .IsRequired();

            contas
                .Property(prop => prop.Data)
                .IsRequired();

            contas
                .Property(prop => prop.Valor)
                .IsRequired();

            contas
                .Property(prop => prop.Parcelado)
                .IsRequired();

            contas
                .Property(prop => prop.NumeroParcelas);

            contas
                .Property(prop => prop.Observacao);

            contas
                .Property(prop => prop.Usuario)
                .IsRequired();

            contas
                .HasOne(ho => ho.Categoria)
                .WithMany(wm => wm.Contas)
                .HasForeignKey(fk => fk.IdCategoria)
                .HasConstraintName("FK_CATEGORIA_CONTAS");
        }
    }
}