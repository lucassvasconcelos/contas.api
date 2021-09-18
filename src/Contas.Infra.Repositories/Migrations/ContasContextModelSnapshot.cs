﻿// <auto-generated />
using System;
using Contas.Infra.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Contas.Infra.Repositories.Migrations
{
    [DbContext(typeof(ContasContext))]
    partial class ContasContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("financeiro")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Contas.Domain.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataUltimaAtualizacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Tipo")
                        .HasColumnType("integer");

                    b.Property<Guid>("Usuario")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("categorias");
                });

            modelBuilder.Entity("Contas.Domain.Conta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Data")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataUltimaAtualizacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("IdCategoria")
                        .HasColumnType("uuid");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumeroParcelas")
                        .HasColumnType("integer");

                    b.Property<string>("Observacao")
                        .HasColumnType("text");

                    b.Property<bool>("Parcelado")
                        .HasColumnType("boolean");

                    b.Property<Guid>("Usuario")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("IdCategoria");

                    b.ToTable("contas");
                });

            modelBuilder.Entity("Contas.Domain.Conta", b =>
                {
                    b.HasOne("Contas.Domain.Categoria", "Categoria")
                        .WithMany("Contas")
                        .HasForeignKey("IdCategoria")
                        .HasConstraintName("FK_CATEGORIA_CONTAS")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("Contas.Domain.Categoria", b =>
                {
                    b.Navigation("Contas");
                });
#pragma warning restore 612, 618
        }
    }
}
