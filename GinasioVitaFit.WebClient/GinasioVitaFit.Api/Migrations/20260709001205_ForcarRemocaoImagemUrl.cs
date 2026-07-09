using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GinasioVitaFit.Api.Migrations
{
    /// <inheritdoc />
    public partial class ForcarRemocaoImagemUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropColumn(
            //     name: "ImagemUrl",
            //     table: "Aulas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl",
                table: "Aulas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
