using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GinasioVitaFit.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixModelsEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InstrutorMods",
                table: "InstrutorMods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AulaSocios",
                table: "AulaSociosDto");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "InstrutorMods");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AulaSociosDto");

            migrationBuilder.DropColumn(
                name: "ImagemUrl",
                table: "Aulas");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Salas",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ModalidadeID",
                table: "InstrutorMods",
                newName: "ModalidadeId");

            migrationBuilder.RenameColumn(
                name: "InstrutorID",
                table: "InstrutorMods",
                newName: "InstrutorId");

            migrationBuilder.RenameColumn(
                name: "SocioID",
                table: "AulaSociosDto",
                newName: "SocioId");

            migrationBuilder.RenameColumn(
                name: "AulaID",
                table: "AulaSociosDto",
                newName: "AulaId");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Nascimento",
                table: "Socios",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "Aulas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstrutorMods",
                table: "InstrutorMods",
                columns: new[] { "InstrutorId", "ModalidadeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AulaSocios",
                table: "AulaSociosDto",
                columns: new[] { "AulaId", "SocioId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InstrutorMods",
                table: "InstrutorMods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AulaSocios",
                table: "AulaSociosDto");

            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "Aulas");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Salas",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "ModalidadeId",
                table: "InstrutorMods",
                newName: "ModalidadeID");

            migrationBuilder.RenameColumn(
                name: "InstrutorId",
                table: "InstrutorMods",
                newName: "InstrutorID");

            migrationBuilder.RenameColumn(
                name: "SocioId",
                table: "AulaSociosDto",
                newName: "SocioID");

            migrationBuilder.RenameColumn(
                name: "AulaId",
                table: "AulaSociosDto",
                newName: "AulaID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Nascimento",
                table: "Socios",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "InstrutorMods",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AulaSociosDto",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl",
                table: "Aulas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstrutorMods",
                table: "InstrutorMods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AulaSocios",
                table: "AulaSociosDto",
                column: "Id");
        }
    }
}
