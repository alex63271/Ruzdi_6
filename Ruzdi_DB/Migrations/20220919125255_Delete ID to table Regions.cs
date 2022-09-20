using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ruzdi_DB.Migrations
{
    public partial class DeleteIDtotableRegions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pledgor_Regions_Organizations_RegionId",
                table: "Pledgor");

            migrationBuilder.DropForeignKey(
                name: "FK_Pledgor_Regions_RegionId",
                table: "Pledgor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Regions");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "Pledgor",
                newName: "RegionCodeRegion");

            migrationBuilder.RenameColumn(
                name: "Organizations_RegionId",
                table: "Pledgor",
                newName: "Organizations_RegionCodeRegion");

            migrationBuilder.RenameIndex(
                name: "IX_Pledgor_RegionId",
                table: "Pledgor",
                newName: "IX_Pledgor_RegionCodeRegion");

            migrationBuilder.RenameIndex(
                name: "IX_Pledgor_Organizations_RegionId",
                table: "Pledgor",
                newName: "IX_Pledgor_Organizations_RegionCodeRegion");

            migrationBuilder.AlterColumn<string>(
                name: "CodeRegion",
                table: "Regions",
                type: "varchar(95)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "CodeRegion");

            migrationBuilder.AddForeignKey(
                name: "FK_Pledgor_Regions_Organizations_RegionCodeRegion",
                table: "Pledgor",
                column: "Organizations_RegionCodeRegion",
                principalTable: "Regions",
                principalColumn: "CodeRegion",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pledgor_Regions_RegionCodeRegion",
                table: "Pledgor",
                column: "RegionCodeRegion",
                principalTable: "Regions",
                principalColumn: "CodeRegion",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pledgor_Regions_Organizations_RegionCodeRegion",
                table: "Pledgor");

            migrationBuilder.DropForeignKey(
                name: "FK_Pledgor_Regions_RegionCodeRegion",
                table: "Pledgor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "29");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "36");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "40");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "48");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "50");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "62");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "67");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "71");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "76");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "77");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "78");

            migrationBuilder.RenameColumn(
                name: "RegionCodeRegion",
                table: "Pledgor",
                newName: "RegionId");

            migrationBuilder.RenameColumn(
                name: "Organizations_RegionCodeRegion",
                table: "Pledgor",
                newName: "Organizations_RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Pledgor_RegionCodeRegion",
                table: "Pledgor",
                newName: "IX_Pledgor_RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Pledgor_Organizations_RegionCodeRegion",
                table: "Pledgor",
                newName: "IX_Pledgor_Organizations_RegionId");

            migrationBuilder.AlterColumn<string>(
                name: "CodeRegion",
                table: "Regions",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(95)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Regions",
                type: "varchar(95)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Pledgor_Regions_Organizations_RegionId",
                table: "Pledgor",
                column: "Organizations_RegionId",
                principalTable: "Regions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pledgor_Regions_RegionId",
                table: "Pledgor",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
