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
    [Migration("20220206042223_Usuarios")]
    partial class Usuarios
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
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

                    b.Property<Guid>("IdUsuario")
                        .HasColumnType("uuid")
                        .HasColumnName("id_usuario");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.Property<int>("Tipo")
                        .HasColumnType("integer")
                        .HasColumnName("tipo");

                    b.HasKey("Id")
                        .HasName("pk_categorias");

                    b.HasIndex("IdUsuario")
                        .HasDatabaseName("ix_categorias_id_usuario");

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

                    b.Property<Guid>("IdUsuario")
                        .HasColumnType("uuid")
                        .HasColumnName("id_usuario");

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

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric")
                        .HasColumnName("valor");

                    b.HasKey("Id")
                        .HasName("pk_contas");

                    b.HasIndex("IdCategoria")
                        .HasDatabaseName("ix_contas_id_categoria");

                    b.HasIndex("IdUsuario")
                        .HasDatabaseName("ix_contas_id_usuario");

                    b.ToTable("contas", "financeiro");
                });

            modelBuilder.Entity("Contas.Domain.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_criacao");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("date")
                        .HasColumnName("data_nascimento");

                    b.Property<DateTime>("DataUltimaAtualizacao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_ultima_atualizacao");

                    b.Property<string>("IdIdentityUser")
                        .HasColumnType("text")
                        .HasColumnName("id_identity_user");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("sobrenome");

                    b.HasKey("Id")
                        .HasName("pk_usuarios");

                    b.ToTable("usuarios", "identity");
                });

            modelBuilder.Entity("Contas.Domain.Categoria", b =>
                {
                    b.HasOne("Contas.Domain.Usuario", "Usuario")
                        .WithMany("Categorias")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_USUARIO_CATEGORIAS");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Contas.Domain.Conta", b =>
                {
                    b.HasOne("Contas.Domain.Categoria", "Categoria")
                        .WithMany("Contas")
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_CATEGORIA_CONTAS");

                    b.HasOne("Contas.Domain.Usuario", "Usuario")
                        .WithMany("Contas")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_USUARIO_CONTAS");

                    b.Navigation("Categoria");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Contas.Domain.Categoria", b =>
                {
                    b.Navigation("Contas");
                });

            modelBuilder.Entity("Contas.Domain.Usuario", b =>
                {
                    b.Navigation("Categorias");

                    b.Navigation("Contas");
                });
#pragma warning restore 612, 618
        }
    }
}
