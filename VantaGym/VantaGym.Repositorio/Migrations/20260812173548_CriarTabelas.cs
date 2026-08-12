using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VantaGym.Repositorio.Migrations
{
    public partial class CriarTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioID);
                });

            migrationBuilder.CreateTable(
                name: "FichaTreino",
                columns: table => new
                {
                    FichaTreinoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativa = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaTreino", x => x.FichaTreinoID);
                    table.ForeignKey(
                        name: "FK_FichaTreino_Usuario_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercicio",
                columns: table => new
                {
                    ExercicioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Series = table.Column<int>(type: "int", nullable: false),
                    Repeticoes = table.Column<int>(type: "int", nullable: false),
                    GrupoMuscular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FichaTreinoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercicio", x => x.ExercicioID);
                    table.ForeignKey(
                        name: "FK_Exercicio_FichaTreino_FichaTreinoID",
                        column: x => x.FichaTreinoID,
                        principalTable: "FichaTreino",
                        principalColumn: "FichaTreinoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoTreino",
                columns: table => new
                {
                    HistoricoTreinoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CargaKg = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    RepeticoesFeitas = table.Column<int>(type: "int", nullable: false),
                    DataExecucao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FichaTreinoID = table.Column<int>(type: "int", nullable: false),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    ExercicioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoTreino", x => x.HistoricoTreinoID);
                    table.ForeignKey(
                        name: "FK_HistoricoTreino_Exercicio_ExercicioID",
                        column: x => x.ExercicioID,
                        principalTable: "Exercicio",
                        principalColumn: "ExercicioID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoricoTreino_FichaTreino_FichaTreinoID",
                        column: x => x.FichaTreinoID,
                        principalTable: "FichaTreino",
                        principalColumn: "FichaTreinoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoricoTreino_Usuario_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercicio_FichaTreinoID",
                table: "Exercicio",
                column: "FichaTreinoID");

            migrationBuilder.CreateIndex(
                name: "IX_FichaTreino_UsuarioID",
                table: "FichaTreino",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoTreino_ExercicioID",
                table: "HistoricoTreino",
                column: "ExercicioID");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoTreino_FichaTreinoID",
                table: "HistoricoTreino",
                column: "FichaTreinoID");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoTreino_UsuarioID",
                table: "HistoricoTreino",
                column: "UsuarioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoTreino");

            migrationBuilder.DropTable(
                name: "Exercicio");

            migrationBuilder.DropTable(
                name: "FichaTreino");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
