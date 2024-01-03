using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteka.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Book_bookId",
                table: "Rental");

            migrationBuilder.DropIndex(
                name: "IX_Rental_bookId",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "bookId",
                table: "Rental");

            migrationBuilder.AddColumn<int>(
                name: "Book",
                table: "Book",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_Book",
                table: "Book",
                column: "Book");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Rental_Book",
                table: "Book",
                column: "Book",
                principalTable: "Rental",
                principalColumn: "rentalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Rental_Book",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_Book",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "Book",
                table: "Book");

            migrationBuilder.AddColumn<int>(
                name: "bookId",
                table: "Rental",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rental_bookId",
                table: "Rental",
                column: "bookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Book_bookId",
                table: "Rental",
                column: "bookId",
                principalTable: "Book",
                principalColumn: "bookId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
