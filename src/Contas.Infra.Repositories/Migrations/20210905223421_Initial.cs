using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contas.Infra.Repositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Usuario = table.Column<Guid>(type: "uuid", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "contas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    Parcelado = table.Column<bool>(type: "boolean", nullable: false),
                    NumeroParcelas = table.Column<int>(type: "integer", nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    Usuario = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCategoria = table.Column<Guid>(type: "uuid", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CATEGORIA_CONTAS",
                        column: x => x.IdCategoria,
                        principalTable: "categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categorias",
                columns: new[] { "Id", "DataCriacao", "DataUltimaAtualizacao", "Descricao", "Nome", "Usuario" },
                values: new object[,]
                {
                    { new Guid("2780819b-924d-4ac3-9d2e-50ff9a823c65"), new DateTime(2021, 9, 5, 19, 34, 20, 652, DateTimeKind.Local).AddTicks(2621), new DateTime(2021, 9, 5, 19, 34, 20, 655, DateTimeKind.Local).AddTicks(6354), "Receita", "Receita", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("796b29c3-55c4-420e-b9d0-31faca96e27e"), new DateTime(2021, 9, 5, 19, 34, 20, 655, DateTimeKind.Local).AddTicks(8091), new DateTime(2021, 9, 5, 19, 34, 20, 655, DateTimeKind.Local).AddTicks(8115), "Despesa", "Despesa", new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_contas_IdCategoria",
                table: "contas",
                column: "IdCategoria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contas");

            migrationBuilder.DropTable(
                name: "categorias");
        }
    }
}
