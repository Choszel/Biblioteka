using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteka.Migrations
{
    /// <inheritdoc />
    public partial class BookRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "BookRental",
                columns: table => new
                {
                    Book = table.Column<int>(type: "int", nullable: false),
                    Rental = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRental", x => new { x.Book, x.Rental });
                    table.ForeignKey(
                        name: "FK_BookRental_Book_Rental",
                        column: x => x.Rental,
                        principalTable: "Book",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookRental_Rental_Book",
                        column: x => x.Book,
                        principalTable: "Rental",
                        principalColumn: "rentalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookRental_Rental",
                table: "BookRental",
                column: "Rental");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookRental");

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
    }
}
