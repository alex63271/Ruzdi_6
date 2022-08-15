using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ruzdi_DB.Migrations
{
    public partial class _0407 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Regions_RegionId",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Regions_RegionId",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "Persons",
                newName: "RegionCodeRegion");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_RegionId",
                table: "Persons",
                newName: "IX_Persons_RegionCodeRegion");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "Organizations",
                newName: "RegionCodeRegion");

            migrationBuilder.RenameIndex(
                name: "IX_Organizations_RegionId",
                table: "Organizations",
                newName: "IX_Organizations_RegionCodeRegion");

            migrationBuilder.AlterColumn<string>(
                name: "CodeRegion",
                table: "Regions",
                type: "varchar(95)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Regions",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(95)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "CodeRegion");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Regions_RegionCodeRegion",
                table: "Organizations",
                column: "RegionCodeRegion",
                principalTable: "Regions",
                principalColumn: "CodeRegion",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Regions_RegionCodeRegion",
                table: "Persons",
                column: "RegionCodeRegion",
                principalTable: "Regions",
                principalColumn: "CodeRegion",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Regions_RegionCodeRegion",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Regions_RegionCodeRegion",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.RenameColumn(
                name: "RegionCodeRegion",
                table: "Persons",
                newName: "RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_RegionCodeRegion",
                table: "Persons",
                newName: "IX_Persons_RegionId");

            migrationBuilder.RenameColumn(
                name: "RegionCodeRegion",
                table: "Organizations",
                newName: "RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Organizations_RegionCodeRegion",
                table: "Organizations",
                newName: "IX_Organizations_RegionId");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Regions",
                type: "varchar(95)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CodeRegion",
                table: "Regions",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(95)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Regions_RegionId",
                table: "Organizations",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Regions_RegionId",
                table: "Persons",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
