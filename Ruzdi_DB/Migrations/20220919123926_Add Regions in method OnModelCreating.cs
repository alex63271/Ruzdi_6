using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ruzdi_DB.Migrations
{
    public partial class AddRegionsinmethodOnModelCreating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "CodeRegion", "Region" },
                values: new object[,]
                {
                    { "1", "77", "Москва" },
                    { "10", "76", "Ярославская область" },
                    { "11", "67", "Смоленская область" },
                    { "2", "50", "Московская область" },
                    { "3", "78", "Санкт-Петербург" },
                    { "4", "29", "Архангельская область" },
                    { "5", "36", "Воронежская область" },
                    { "6", "40", "Калужская область" },
                    { "7", "48", "Липецкая область" },
                    { "8", "62", "Рязанская область" },
                    { "9", "71", "Тульская область" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: "10");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: "11");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: "6");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: "7");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: "8");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: "9");
        }
    }
}
