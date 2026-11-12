using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocadoraCarros.Migrations
{
    public partial class update_locacao_usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_locacao_cliente_cliente_id",
                table: "locacao");

            migrationBuilder.RenameColumn(
                name: "cliente_id",
                table: "locacao",
                newName: "usuario_id");

            migrationBuilder.RenameIndex(
                name: "IX_locacao_cliente_id",
                table: "locacao",
                newName: "IX_locacao_usuario_id");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "cliente",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_locacao_AspNetUsers_usuario_id",
                table: "locacao",
                column: "usuario_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_locacao_AspNetUsers_usuario_id",
                table: "locacao");

            migrationBuilder.RenameColumn(
                name: "usuario_id",
                table: "locacao",
                newName: "cliente_id");

            migrationBuilder.RenameIndex(
                name: "IX_locacao_usuario_id",
                table: "locacao",
                newName: "IX_locacao_cliente_id");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "cliente",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_locacao_cliente_cliente_id",
                table: "locacao",
                column: "cliente_id",
                principalTable: "cliente",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
