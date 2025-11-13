using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionCompeticiones.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class _1111migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categoria_User_UsuarioResponsableId",
                table: "Categoria");

            migrationBuilder.DropIndex(
                name: "IX_Categoria_UsuarioResponsableId",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "UsuarioResponsableId",
                table: "Categoria");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Categoria",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_UserId",
                table: "Categoria",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_User_UserId",
                table: "Categoria",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categoria_User_UserId",
                table: "Categoria");

            migrationBuilder.DropIndex(
                name: "IX_Categoria_UserId",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Categoria");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioResponsableId",
                table: "Categoria",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_UsuarioResponsableId",
                table: "Categoria",
                column: "UsuarioResponsableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_User_UsuarioResponsableId",
                table: "Categoria",
                column: "UsuarioResponsableId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
