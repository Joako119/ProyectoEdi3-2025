using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionCompeticiones.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class migration1111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Piloto_PilotoId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_PilotoId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PilotoId",
                table: "User");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "Piloto",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Piloto_UsuarioId",
                table: "Piloto",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Piloto_User_UsuarioId",
                table: "Piloto",
                column: "UsuarioId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Piloto_User_UsuarioId",
                table: "Piloto");

            migrationBuilder.DropIndex(
                name: "IX_Piloto_UsuarioId",
                table: "Piloto");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Piloto");

            migrationBuilder.AddColumn<int>(
                name: "PilotoId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_PilotoId",
                table: "User",
                column: "PilotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Piloto_PilotoId",
                table: "User",
                column: "PilotoId",
                principalTable: "Piloto",
                principalColumn: "Id");
        }
    }
}
