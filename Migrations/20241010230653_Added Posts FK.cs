using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaChozaComercial.Migrations
{
    /// <inheritdoc />
    public partial class AddedPostsFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreVendedor",
                table: "Publicaciones");

            migrationBuilder.AddColumn<string>(
                name: "usuarioId",
                table: "Publicaciones",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Publicaciones_usuarioId",
                table: "Publicaciones",
                column: "usuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publicaciones_AspNetUsers_usuarioId",
                table: "Publicaciones",
                column: "usuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publicaciones_AspNetUsers_usuarioId",
                table: "Publicaciones");

            migrationBuilder.DropIndex(
                name: "IX_Publicaciones_usuarioId",
                table: "Publicaciones");

            migrationBuilder.DropColumn(
                name: "usuarioId",
                table: "Publicaciones");

            migrationBuilder.AddColumn<string>(
                name: "NombreVendedor",
                table: "Publicaciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
