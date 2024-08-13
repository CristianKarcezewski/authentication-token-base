using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CMLApplication.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "grupos_colaboradores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grupos_colaboradores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permissoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissoes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "colaboradores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    id_grupos_colaboradores = table.Column<int>(type: "int", nullable: false),
                    login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nome_completo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    ultima_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dthr = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colaboradores", x => x.id);
                    table.ForeignKey(
                        name: "FK_colaboradores_grupos_colaboradores_id",
                        column: x => x.id,
                        principalTable: "grupos_colaboradores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "grupos_colaboradores_permissoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_grupos_colaboradores = table.Column<int>(type: "int", nullable: false),
                    id_permissoes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grupos_colaboradores_permissoes", x => x.id);
                    table.ForeignKey(
                        name: "FK_grupos_colaboradores_permissoes_grupos_colaboradores_id_grupos_colaboradores",
                        column: x => x.id_grupos_colaboradores,
                        principalTable: "grupos_colaboradores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grupos_colaboradores_permissoes_permissoes_id_permissoes",
                        column: x => x.id_permissoes,
                        principalTable: "permissoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "grupos_colaboradores",
                columns: new[] { "id", "nome" },
                values: new object[,]
                {
                    { 1, "Colaborador" },
                    { 2, "Administrador" },
                    { 3, "Auditor" }
                });

            migrationBuilder.InsertData(
                table: "permissoes",
                columns: new[] { "id", "descricao" },
                values: new object[,]
                {
                    { 1, "Consultar" },
                    { 2, "Inserir" },
                    { 3, "Atualizar" },
                    { 4, "Excluir" }
                });

            migrationBuilder.InsertData(
                table: "colaboradores",
                columns: new[] { "id", "ativo", "dthr", "email", "id_grupos_colaboradores", "login", "nome_completo", "telefone", "ultima_atualizacao" },
                values: new object[] { 1, true, new DateTime(2024, 7, 3, 11, 42, 52, 163, DateTimeKind.Local).AddTicks(4840), "diego.santos@keyworks.com.br", 2, "str_dsantos", "Diego dos Santos", null, new DateTime(2024, 7, 3, 11, 42, 52, 163, DateTimeKind.Local).AddTicks(4830) });

            migrationBuilder.InsertData(
                table: "grupos_colaboradores_permissoes",
                columns: new[] { "id", "id_grupos_colaboradores", "id_permissoes" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 2, 2 },
                    { 4, 2, 3 },
                    { 5, 2, 4 },
                    { 6, 3, 1 },
                    { 7, 3, 2 },
                    { 8, 3, 3 },
                    { 9, 3, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_grupos_colaboradores_permissoes_id_grupos_colaboradores",
                table: "grupos_colaboradores_permissoes",
                column: "id_grupos_colaboradores");

            migrationBuilder.CreateIndex(
                name: "IX_grupos_colaboradores_permissoes_id_permissoes",
                table: "grupos_colaboradores_permissoes",
                column: "id_permissoes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "colaboradores");

            migrationBuilder.DropTable(
                name: "grupos_colaboradores_permissoes");

            migrationBuilder.DropTable(
                name: "grupos_colaboradores");

            migrationBuilder.DropTable(
                name: "permissoes");
        }
    }
}
