using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocadoraCarros.Migrations
{
    public partial class ApplicationUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cliente",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "cliente",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(350)",
                oldMaxLength: 350)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "cnh",
                table: "cliente",
                type: "varchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "data_nascimento",
                table: "cliente",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "validade_cnh",
                table: "cliente",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "cnh",
                table: "AspNetUsers",
                type: "varchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "data_nascimento",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "endereco",
                table: "AspNetUsers",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "validade_cnh",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cnh",
                table: "cliente");

            migrationBuilder.DropColumn(
                name: "data_nascimento",
                table: "cliente");

            migrationBuilder.DropColumn(
                name: "validade_cnh",
                table: "cliente");

            migrationBuilder.DropColumn(
                name: "cnh",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "data_nascimento",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "endereco",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "validade_cnh",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "cliente",
                type: "varchar(350)",
                maxLength: 350,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "cliente",
                columns: new[] { "id", "email", "nome", "telefone" },
                values: new object[] { 1L, "moacir@teste.com", "Moacir Carvalho", "+55 21 994132-XXXX" });
        }
    }
}
