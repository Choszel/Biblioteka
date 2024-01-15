using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteka.Migrations
{
    /// <inheritdoc />
    public partial class tagi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Book_Tag",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_Tag",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Tag");

            migrationBuilder.CreateTable(
                name: "BookTag",
                columns: table => new
                {
                    Book = table.Column<int>(type: "int", nullable: false),
                    Tag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTag", x => new { x.Book, x.Tag });
                    table.ForeignKey(
                        name: "FK_BookTag_Book_Tag",
                        column: x => x.Tag,
                        principalTable: "Book",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTag_Tag_Book",
                        column: x => x.Book,
                        principalTable: "Tag",
                        principalColumn: "tagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookTag_Tag",
                table: "BookTag",
                column: "Tag");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookTag");

            migrationBuilder.AddColumn<int>(
                name: "Tag",
                table: "Tag",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_Tag",
                table: "Tag",
                column: "Tag");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Book_Tag",
                table: "Tag",
                column: "Tag",
                principalTable: "Book",
                principalColumn: "bookId");
        }
    }
}
