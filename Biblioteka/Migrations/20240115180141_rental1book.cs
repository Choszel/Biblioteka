using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteka.Migrations
{
    /// <inheritdoc />
    public partial class rental1book : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookRental");

            migrationBuilder.DropTable(
                name: "RentalBook");

            migrationBuilder.DropTable(
                name: "TagBook");

            migrationBuilder.DropColumn(
                name: "houseNumber",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "rentalCity",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "rentalStreet",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "stateDate",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "zipCode",
                table: "Rental");

            migrationBuilder.AddColumn<int>(
                name: "Book",
                table: "Rental",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rental",
                table: "Rental",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rental_Book",
                table: "Rental",
                column: "Book");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_Rental",
                table: "Rental",
                column: "Rental");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Book_Book",
                table: "Rental",
                column: "Book",
                principalTable: "Book",
                principalColumn: "bookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Book_Rental",
                table: "Rental",
                column: "Rental",
                principalTable: "Book",
                principalColumn: "bookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Book_Book",
                table: "Rental");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Book_Rental",
                table: "Rental");

            migrationBuilder.DropIndex(
                name: "IX_Rental_Book",
                table: "Rental");

            migrationBuilder.DropIndex(
                name: "IX_Rental_Rental",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Book",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Rental",
                table: "Rental");

            migrationBuilder.AddColumn<decimal>(
                name: "houseNumber",
                table: "Rental",
                type: "NUMERIC(3,0)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "rentalCity",
                table: "Rental",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "rentalStreet",
                table: "Rental",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "stateDate",
                table: "Rental",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "zipCode",
                table: "Rental",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateTable(
                name: "RentalBook",
                columns: table => new
                {
                    bookId = table.Column<int>(type: "int", nullable: false),
                    rentalId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_RentalBook_Book_bookId",
                        column: x => x.bookId,
                        principalTable: "Book",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalBook_Rental_rentalId",
                        column: x => x.rentalId,
                        principalTable: "Rental",
                        principalColumn: "rentalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagBook",
                columns: table => new
                {
                    bookId = table.Column<int>(type: "int", nullable: false),
                    tagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_TagBook_Book_bookId",
                        column: x => x.bookId,
                        principalTable: "Book",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagBook_Tag_tagId",
                        column: x => x.tagId,
                        principalTable: "Tag",
                        principalColumn: "tagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookRental_Rental",
                table: "BookRental",
                column: "Rental");

            migrationBuilder.CreateIndex(
                name: "IX_RentalBook_bookId",
                table: "RentalBook",
                column: "bookId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalBook_rentalId",
                table: "RentalBook",
                column: "rentalId");

            migrationBuilder.CreateIndex(
                name: "IX_TagBook_bookId",
                table: "TagBook",
                column: "bookId");

            migrationBuilder.CreateIndex(
                name: "IX_TagBook_tagId",
                table: "TagBook",
                column: "tagId");
        }
    }
}
