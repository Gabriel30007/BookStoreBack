using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Migration2005230113 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bucket_Users_userID",
                table: "Bucket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bucket_User_userID",
                table: "Bucket",
                column: "userID",
                principalTable: "User",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bucket_User_userID",
                table: "Bucket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bucket_Users_userID",
                table: "Bucket",
                column: "userID",
                principalTable: "Users",
                principalColumn: "ID");
        }
    }
}
