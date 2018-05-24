using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Alura.WebAPI.WebApp.Data.Migrations
{
    public partial class AjusteLivro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_ListasDeLeitura_ListaId",
                table: "Livros");

            migrationBuilder.RenameColumn(
                name: "ListaId",
                table: "Livros",
                newName: "ListaLeituraId");

            migrationBuilder.RenameIndex(
                name: "IX_Livros_ListaId",
                table: "Livros",
                newName: "IX_Livros_ListaLeituraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_ListasDeLeitura_ListaLeituraId",
                table: "Livros",
                column: "ListaLeituraId",
                principalTable: "ListasDeLeitura",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_ListasDeLeitura_ListaLeituraId",
                table: "Livros");

            migrationBuilder.RenameColumn(
                name: "ListaLeituraId",
                table: "Livros",
                newName: "ListaId");

            migrationBuilder.RenameIndex(
                name: "IX_Livros_ListaLeituraId",
                table: "Livros",
                newName: "IX_Livros_ListaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_ListasDeLeitura_ListaId",
                table: "Livros",
                column: "ListaId",
                principalTable: "ListasDeLeitura",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
