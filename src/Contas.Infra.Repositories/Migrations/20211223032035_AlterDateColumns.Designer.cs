﻿// <auto-generated />
using System;
using Contas.Infra.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Contas.Infra.Repositories.Migrations
{
    [DbContext(typeof(ContasContext))]
    [Migration("20211223032035_AlterDateColumns")]
    partial class AlterDateColumns
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("financeiro")
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Contas.Domain.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_criacao");

                    b.Property<DateTime>("DataUltimaAtualizacao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_ultima_atualizacao");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("descricao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.Property<int>("Tipo")
                        .HasColumnType("integer")
                        .HasColumnName("tipo");

                    b.Property<Guid>("Usuario")
                        .HasColumnType("uuid")
                        .HasColumnName("usuario");

                    b.HasKey("Id")
                        .HasName("pk_categorias");

                    b.ToTable("categorias", "financeiro");
                });

            modelBuilder.Entity("Contas.Domain.Conta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("Data")
                        .HasColumnType("date")
                        .HasColumnName("data");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_criacao");

                    b.Property<DateTime>("DataUltimaAtualizacao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_ultima_atualizacao");

                    b.Property<Guid>("IdCategoria")
                        .HasColumnType("uuid")
                        .HasColumnName("id_categoria");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.Property<int>("NumeroParcelas")
                        .HasColumnType("integer")
                        .HasColumnName("numero_parcelas");

                    b.Property<string>("Observacao")
                        .HasColumnType("text")
                        .HasColumnName("observacao");

                    b.Property<bool>("Parcelado")
                        .HasColumnType("boolean")
                        .HasColumnName("parcelado");

                    b.Property<Guid>("Usuario")
                        .HasColumnType("uuid")
                        .HasColumnName("usuario");

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric")
                        .HasColumnName("valor");

                    b.HasKey("Id")
                        .HasName("pk_contas");

                    b.HasIndex("IdCategoria")
                        .HasDatabaseName("ix_contas_id_categoria");

                    b.ToTable("contas", "financeiro");
                });

            modelBuilder.Entity("Contas.Domain.Conta", b =>
                {
                    b.HasOne("Contas.Domain.Categoria", "Categoria")
                        .WithMany("Contas")
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_CATEGORIA_CONTAS");

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