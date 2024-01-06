using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteka.Migrations
{
    /// <inheritdoc />
    public partial class employee2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    birthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pesel = table.Column<decimal>(type: "NUMERIC(11,0)", nullable: false),
                    street = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    houseNumber = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: true),
                    town = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    zipCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    dateOfEmployment = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
