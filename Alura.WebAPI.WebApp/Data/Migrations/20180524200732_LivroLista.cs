using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Alura.WebAPI.WebApp.Data.Migrations
{
    public partial class LivroLista : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_ListasDeLeitura_ListaLeituraId",
                table: "Livros");

            migrationBuilder.AlterColumn<int>(
                name: "ListaLeituraId",
                table: "Livros",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_ListasDeLeitura_ListaLeituraId",
                table: "Livros",
                column: "ListaLeituraId",
                principalTable: "ListasDeLeitura",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_ListasDeLeitura_ListaLeituraId",
                table: "Livros");

            migrationBuilder.AlterColumn<int>(
                name: "ListaLeituraId",
                table: "Livros",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_ListasDeLeitura_ListaLeituraId",
                table: "Livros",
                column: "ListaLeituraId",
                principalTable: "ListasDeLeitura",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
