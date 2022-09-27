using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ruzdi_DB.Migrations
{
    public partial class addThumbprintCertinclassNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbprintCert",
                table: "Notifications",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbprintCert",
                table: "Notifications");
        }
    }
}
