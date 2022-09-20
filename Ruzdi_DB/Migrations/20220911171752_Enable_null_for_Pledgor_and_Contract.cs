using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ruzdi_DB.Migrations
{
    public partial class Enable_null_for_Pledgor_and_Contract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Contracts_ContractsID",
                table: "Notifications");

            migrationBuilder.AlterColumn<string>(
                name: "ContractsID",
                table: "Notifications",
                type: "varchar(95)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(95)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Contracts_ContractsID",
                table: "Notifications",
                column: "ContractsID",
                principalTable: "Contracts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Contracts_ContractsID",
                table: "Notifications");

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "ContractsID",
                keyValue: null,
                column: "ContractsID",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ContractsID",
                table: "Notifications",
                type: "varchar(95)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(95)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Contracts_ContractsID",
                table: "Notifications",
                column: "ContractsID",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
