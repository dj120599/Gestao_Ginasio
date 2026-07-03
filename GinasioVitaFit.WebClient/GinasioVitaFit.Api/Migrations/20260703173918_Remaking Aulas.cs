using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GinasioVitaFit.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemakingAulas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sala",
                table: "Aulas");

            migrationBuilder.AddColumn<int>(
                name: "SalaId",
                table: "Aulas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_SalaId",
                table: "Aulas",
                column: "SalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Salas_SalaId",
                table: "Aulas",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Salas_SalaId",
                table: "Aulas");

            migrationBuilder.DropIndex(
                name: "IX_Aulas_SalaId",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "SalaId",
                table: "Aulas");

            migrationBuilder.AddColumn<string>(
                name: "Sala",
                table: "Aulas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
