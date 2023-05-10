using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaTenis.Core.Jogadores.AcessoDados.Migrations
{
    public partial class Criacao_das_tabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaracteristicaDeJogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JogadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaoDominante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Backhand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstiloDeJogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaracteristicaDeJogo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localizacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JogadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jogador",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalizacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CaracteristicaDeJogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jogador_CaracteristicaDeJogo_CaracteristicaDeJogoId",
                        column: x => x.CaracteristicaDeJogoId,
                        principalTable: "CaracteristicaDeJogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Jogador_Localizacao_LocalizacaoId",
                        column: x => x.LocalizacaoId,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_CaracteristicaDeJogoId",
                table: "Jogador",
                column: "CaracteristicaDeJogoId",
                unique: true,
                filter: "[CaracteristicaDeJogoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_LocalizacaoId",
                table: "Jogador",
                column: "LocalizacaoId",
                unique: true,
                filter: "[LocalizacaoId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jogador");

            migrationBuilder.DropTable(
                name: "CaracteristicaDeJogo");

            migrationBuilder.DropTable(
                name: "Localizacao");
        }
    }
}
