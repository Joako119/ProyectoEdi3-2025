using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionCompeticiones.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class FkFaltantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Apellido",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AddColumn<int>(
                name: "FederacionId",
                table: "Campeonato",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Campeonato_FederacionId",
                table: "Campeonato",
                column: "FederacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campeonato_Federacion_FederacionId",
                table: "Campeonato",
                column: "FederacionId",
                principalTable: "Federacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campeonato_Federacion_FederacionId",
                table: "Campeonato");

            migrationBuilder.DropIndex(
                name: "IX_Campeonato_FederacionId",
                table: "Campeonato");

            migrationBuilder.DropColumn(
                name: "FederacionId",
                table: "Campeonato");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Usuario",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Apellido",
                table: "Usuario",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
