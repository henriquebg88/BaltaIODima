using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dima.API.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    description = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: true),
                    userID = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    paidOrReceivedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    type = table.Column<short>(type: "SMALLINT", nullable: false),
                    amount = table.Column<decimal>(type: "MONEY", nullable: false),
                    categoryId = table.Column<long>(type: "BIGINT", nullable: false),
                    userId = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_Transaction_Category_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_categoryId",
                table: "Transaction",
                column: "categoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
