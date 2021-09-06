﻿// <auto-generated />
using System;
using Contas.Infra.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Contas.Infra.Repositories.Migrations
{
    [DbContext(typeof(ContasContext))]
    [Migration("20210905223421_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
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

                    b.Property<Guid>("Usuario")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("categorias");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2780819b-924d-4ac3-9d2e-50ff9a823c65"),
                            DataCriacao = new DateTime(2021, 9, 5, 19, 34, 20, 652, DateTimeKind.Local).AddTicks(2621),
                            DataUltimaAtualizacao = new DateTime(2021, 9, 5, 19, 34, 20, 655, DateTimeKind.Local).AddTicks(6354),
                            Descricao = "Receita",
                            Nome = "Receita",
                            Usuario = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("796b29c3-55c4-420e-b9d0-31faca96e27e"),
                            DataCriacao = new DateTime(2021, 9, 5, 19, 34, 20, 655, DateTimeKind.Local).AddTicks(8091),
                            DataUltimaAtualizacao = new DateTime(2021, 9, 5, 19, 34, 20, 655, DateTimeKind.Local).AddTicks(8115),
                            Descricao = "Despesa",
                            Nome = "Despesa",
                            Usuario = new Guid("00000000-0000-0000-0000-000000000000")
                        });
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
