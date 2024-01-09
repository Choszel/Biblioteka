using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteka.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Readers_email",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "userE_mail",
                table: "Rental");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Rental",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_email",
                table: "Rental",
                newName: "IX_Rental_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Readers_userId",
                table: "Rental",
                column: "userId",
                principalTable: "Readers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Readers_userId",
                table: "Rental");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Rental",
                newName: "email");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_userId",
                table: "Rental",
                newName: "IX_Rental_email");

            migrationBuilder.AddColumn<string>(
                name: "userE_mail",
                table: "Rental",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Readers_email",
                table: "Rental",
                column: "email",
                principalTable: "Readers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
