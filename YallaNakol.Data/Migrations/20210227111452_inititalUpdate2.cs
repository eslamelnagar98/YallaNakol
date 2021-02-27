using Microsoft.EntityFrameworkCore.Migrations;

namespace YallaNakol.Data.Migrations
{
    public partial class inititalUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 12,
                column: "CategoryId",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 12,
                column: "CategoryId",
                value: 1);
        }
    }
}
