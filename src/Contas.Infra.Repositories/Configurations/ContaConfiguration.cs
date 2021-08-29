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
                .Property(prop => prop.Nome)
                .IsRequired();

            contas
                .Property(prop => prop.DataCriacao)
                .IsRequired();

            contas
                .Property(prop => prop.DataUltimaAtualizacao)
                .IsRequired();
        }
    }
}