using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contas.Infra.Repositories.Migrations
{
    public partial class Usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "identity");

            migrationBuilder.RenameColumn(
                name: "usuario",
                schema: "financeiro",
                table: "contas",
                newName: "id_usuario");

            migrationBuilder.RenameColumn(
                name: "usuario",
                schema: "financeiro",
                table: "categorias",
                newName: "id_usuario");

            migrationBuilder.CreateTable(
                name: "usuarios",
                schema: "identity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    sobrenome = table.Column<string>(type: "text", nullable: false),
                    data_nascimento = table.Column<DateTime>(type: "date", nullable: false),
                    id_identity_user = table.Column<string>(type: "text", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_ultima_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuarios", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_contas_id_usuario",
                schema: "financeiro",
                table: "contas",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "ix_categorias_id_usuario",
                schema: "financeiro",
                table: "categorias",
                column: "id_usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIO_CATEGORIAS",
                schema: "financeiro",
                table: "categorias",
                column: "id_usuario",
                principalSchema: "identity",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIO_CONTAS",
                schema: "financeiro",
                table: "contas",
                column: "id_usuario",
                principalSchema: "identity",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USUARIO_CATEGORIAS",
                schema: "financeiro",
                table: "categorias");

            migrationBuilder.DropForeignKey(
                name: "FK_USUARIO_CONTAS",
                schema: "financeiro",
                table: "contas");

            migrationBuilder.DropTable(
                name: "usuarios",
                schema: "identity");

            migrationBuilder.DropIndex(
                name: "ix_contas_id_usuario",
                schema: "financeiro",
                table: "contas");

            migrationBuilder.DropIndex(
                name: "ix_categorias_id_usuario",
                schema: "financeiro",
                table: "categorias");

            migrationBuilder.RenameColumn(
                name: "id_usuario",
                schema: "financeiro",
                table: "contas",
                newName: "usuario");

            migrationBuilder.RenameColumn(
                name: "id_usuario",
                schema: "financeiro",
                table: "categorias",
                newName: "usuario");
        }
    }
}
