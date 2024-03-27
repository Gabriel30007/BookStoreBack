using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedGenretable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Product");

            migrationBuilder.AddColumn<Guid>(
                name: "genreID",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_genreID",
                table: "Product",
                column: "genreID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Genre_genreID",
                table: "Product",
                column: "genreID",
                principalTable: "Genre",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Genre_genreID",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Product_genreID",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "genreID",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
