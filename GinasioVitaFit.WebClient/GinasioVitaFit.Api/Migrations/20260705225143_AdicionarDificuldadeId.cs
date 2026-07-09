using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GinasioVitaFit.Api.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarDificuldadeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl",
                table: "Aulas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemUrl",
                table: "Aulas");
        }
    }
}
