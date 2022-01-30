using Contas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infra.Repositories.Configurations
{
    public class ContaConfiguration : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> contas)
        {
            contas.ToTable("contas", "financeiro");

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
                .HasColumnType("date")
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
                .HasOne(ho => ho.Categoria)
                .WithMany(wm => wm.Contas)
                .HasForeignKey(fk => fk.IdCategoria)
                .HasConstraintName("FK_CATEGORIA_CONTAS");

            contas
                .HasOne(ho => ho.Usuario)
                .WithMany(wm => wm.Contas)
                .HasForeignKey(fk => fk.IdUsuario)
                .HasConstraintName("FK_USUARIO_CONTAS");
        }
    }
}