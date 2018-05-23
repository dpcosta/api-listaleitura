using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Alura.WebAPI.WebApp.Data.Migrations
{
    public partial class ListaLeitura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListaId",
                table: "Livros",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ListasDeLeitura",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListasDeLeitura", x => x.Id);
                    table.UniqueConstraint("AK_ListasDeLeitura_Tipo_UsuarioId", x => new { x.Tipo, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_ListasDeLeitura_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livros_ListaId",
                table: "Livros",
                column: "ListaId");

            migrationBuilder.CreateIndex(
                name: "IX_ListasDeLeitura_UsuarioId",
                table: "ListasDeLeitura",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_ListasDeLeitura_ListaId",
                table: "Livros",
                column: "ListaId",
                principalTable: "ListasDeLeitura",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_ListasDeLeitura_ListaId",
                table: "Livros");

            migrationBuilder.DropTable(
                name: "ListasDeLeitura");

            migrationBuilder.DropIndex(
                name: "IX_Livros_ListaId",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "ListaId",
                table: "Livros");
        }
    }
}
