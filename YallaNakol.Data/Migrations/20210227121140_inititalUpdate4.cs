using Microsoft.EntityFrameworkCore.Migrations;

namespace YallaNakol.Data.Migrations
{
    public partial class inititalUpdate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "InStock", "MenuId", "Name", "Price" },
                values: new object[] { 13, 2, "A new Sandwich introduced from macdonalds from our chefs with our great sauses", "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/298f37b4-f9f7-44ea-a297-02cda66bd50b.jpg", true, 1, "Chicked Grilled New Sandwich", 100m });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "InStock", "MenuId", "Name", "Price" },
                values: new object[] { 14, 3, "Juicy beef patty smothered in three extraordinary slices of Emmental cheese and topped with sliced tomato, shredded lettuce, onions and that special Big Tasty sauce", "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/a660c55e-d9ea-4c0d-be33-4d0cc5d0a7b4.jpg", true, 1, "Big Tasty", 100m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
