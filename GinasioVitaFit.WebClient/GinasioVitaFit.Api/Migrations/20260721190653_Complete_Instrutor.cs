using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GinasioVitaFit.Api.Migrations
{
    /// <inheritdoc />
    public partial class Complete_Instrutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstrutorId",
                table: "Modalidades",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modalidades_InstrutorId",
                table: "Modalidades",
                column: "InstrutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modalidades_Instrutores_InstrutorId",
                table: "Modalidades",
                column: "InstrutorId",
                principalTable: "Instrutores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modalidades_Instrutores_InstrutorId",
                table: "Modalidades");

            migrationBuilder.DropIndex(
                name: "IX_Modalidades_InstrutorId",
                table: "Modalidades");

            migrationBuilder.DropColumn(
                name: "InstrutorId",
                table: "Modalidades");
        }
    }
}
