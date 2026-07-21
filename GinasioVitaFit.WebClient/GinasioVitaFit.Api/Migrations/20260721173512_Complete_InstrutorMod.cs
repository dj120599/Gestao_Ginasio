using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GinasioVitaFit.Api.Migrations
{
    /// <inheritdoc />
    public partial class Complete_InstrutorMod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InstrutorMods_ModalidadeId",
                table: "InstrutorMods",
                column: "ModalidadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstrutorMods_Instrutores_InstrutorId",
                table: "InstrutorMods",
                column: "InstrutorId",
                principalTable: "Instrutores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstrutorMods_Modalidades_ModalidadeId",
                table: "InstrutorMods",
                column: "ModalidadeId",
                principalTable: "Modalidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstrutorMods_Instrutores_InstrutorId",
                table: "InstrutorMods");

            migrationBuilder.DropForeignKey(
                name: "FK_InstrutorMods_Modalidades_ModalidadeId",
                table: "InstrutorMods");

            migrationBuilder.DropIndex(
                name: "IX_InstrutorMods_ModalidadeId",
                table: "InstrutorMods");
        }
    }
}
