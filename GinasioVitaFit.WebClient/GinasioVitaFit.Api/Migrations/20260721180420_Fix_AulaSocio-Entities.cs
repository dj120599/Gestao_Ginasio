using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GinasioVitaFit.Api.Migrations
{
    /// <inheritdoc />
    public partial class Fix_AulaSocioEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AulaSocios_SocioId",
                table: "AulaSocios",
                column: "SocioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AulaSocios_Aulas_AulaId",
                table: "AulaSocios",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AulaSocios_Socios_SocioId",
                table: "AulaSocios",
                column: "SocioId",
                principalTable: "Socios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AulaSocios_Aulas_AulaId",
                table: "AulaSocios");

            migrationBuilder.DropForeignKey(
                name: "FK_AulaSocios_Socios_SocioId",
                table: "AulaSocios");

            migrationBuilder.DropIndex(
                name: "IX_AulaSocios_SocioId",
                table: "AulaSocios");
        }
    }
}
