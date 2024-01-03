using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteka.Migrations
{
    /// <inheritdoc />
    public partial class tagi_wypozyczenia_stanmgwksiazkach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "stockLevel",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Rental",
                columns: table => new
                {
                    rentalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    bookId = table.Column<int>(type: "int", nullable: false),
                    rentalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    rentalState = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    stateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    rentalCity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    rentalStreet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rental", x => x.rentalId);
                    table.ForeignKey(
                        name: "FK_Rental_Book_bookId",
                        column: x => x.bookId,
                        principalTable: "Book",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    tagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tagName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.tagId);
                });

            migrationBuilder.CreateTable(
                name: "TagBook",
                columns: table => new
                {
                    tagId = table.Column<int>(type: "int", nullable: false),
                    bookId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_Rental_bookId",
                table: "Rental",
                column: "bookId");

            migrationBuilder.CreateIndex(
                name: "IX_TagBook_bookId",
                table: "TagBook",
                column: "bookId");

            migrationBuilder.CreateIndex(
                name: "IX_TagBook_tagId",
                table: "TagBook",
                column: "tagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rental");

            migrationBuilder.DropTable(
                name: "TagBook");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropColumn(
                name: "stockLevel",
                table: "Book");
        }
    }
}
