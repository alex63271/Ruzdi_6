using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ruzdi_DB.Migrations
{
    public partial class creation_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    Number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TermOfContract = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    CodeRegion = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Region = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.CodeRegion);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RegistrationCertificate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    documentAndSignature = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationCertificate", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pledgor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameFull = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OGRN = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    INN = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Organizations_RegionCodeRegion = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Last = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    First = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Middle = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PersonDocument = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegionCodeRegion = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pledgor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pledgor_Regions_Organizations_RegionCodeRegion",
                        column: x => x.Organizations_RegionCodeRegion,
                        principalTable: "Regions",
                        principalColumn: "CodeRegion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pledgor_Regions_RegionCodeRegion",
                        column: x => x.RegionCodeRegion,
                        principalTable: "Regions",
                        principalColumn: "CodeRegion",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Packageguid = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Packageid = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContractsID = table.Column<int>(type: "int", nullable: true),
                    ZipArchive = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumberNotification = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TypeNotification = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Error = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CertInfo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    registrationCertificateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Contracts_ContractsID",
                        column: x => x.ContractsID,
                        principalTable: "Contracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_RegistrationCertificate_registrationCertificat~",
                        column: x => x.registrationCertificateId,
                        principalTable: "RegistrationCertificate",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NotificationPledgor",
                columns: table => new
                {
                    NotificationsId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PledgorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationPledgor", x => new { x.NotificationsId, x.PledgorsId });
                    table.ForeignKey(
                        name: "FK_NotificationPledgor_Notifications_NotificationsId",
                        column: x => x.NotificationsId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationPledgor_Pledgor_PledgorsId",
                        column: x => x.PledgorsId,
                        principalTable: "Pledgor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "CodeRegion", "Region" },
                values: new object[,]
                {
                    { "01", "Республика Адыгея (Адыгея)" },
                    { "02", "Республика Башкортостан" },
                    { "03", "Республика Бурятия" },
                    { "04", "Республика Алтай" },
                    { "05", "Республика Дагестан" },
                    { "06", "Республика Ингушетия" },
                    { "07", "Кабардино-Балкарская Республика" },
                    { "08", "Республика Калмыкия" },
                    { "09", "Карачаево-Черкесская Республика" },
                    { "10", "Республика Карелия" },
                    { "11", "Республика Коми" },
                    { "12", "Республика Марий Эл" },
                    { "13", "Республика Мордовия" },
                    { "14", "Республика Саха (Якутия)" },
                    { "15", "Республика Северная Осетия — Алания" },
                    { "16", "Республика Татарстан (Татарстан)" },
                    { "17", "Республика Тыва" },
                    { "18", "Удмуртская Республика" },
                    { "19", "Республика Хакасия" },
                    { "20", "Чеченская Республика" },
                    { "21", "Чувашская Республика — Чувашия" },
                    { "22", "Алтайский край" },
                    { "23", "Краснодарский край" },
                    { "24", "Красноярский край" },
                    { "25", "Приморский край" },
                    { "26", "Ставропольский край" },
                    { "27", "Хабаровский край" },
                    { "28", "Амурская область" },
                    { "29", "Архангельская область" },
                    { "30", "Астраханская область" },
                    { "31", "Белгородская область" },
                    { "32", "Брянская область" },
                    { "33", "Владимирская область" },
                    { "34", "Волгоградская область" },
                    { "35", "Вологодская область" },
                    { "36", "Воронежская область" },
                    { "37", "Ивановская область" },
                    { "38", "Иркутская область" },
                    { "39", "Калининградская область" },
                    { "40", "Калужская область" },
                    { "41", "Камчатский край" },
                    { "42", "Кемеровская область — Кузбасс" },
                    { "43", "Кировская область" },
                    { "44", "Костромская область" },
                    { "45", "Курганская область" },
                    { "46", "Курская область" },
                    { "47", "Ленинградская область" },
                    { "48", "Липецкая область" },
                    { "49", "Магаданская область" },
                    { "50", "Московская область" },
                    { "51", "Мурманская область" },
                    { "52", "Нижегородская область" },
                    { "53", "Новгородская область" },
                    { "54", "Новосибирская область" },
                    { "55", "Омская область" },
                    { "56", "Оренбургская область" },
                    { "57", "Орловская область" },
                    { "58", "Пензенская область" },
                    { "59", "Пермский край" },
                    { "60", "Псковская область" },
                    { "61", "Ростовская область" },
                    { "62", "Рязанская область" },
                    { "63", "Самарская область" },
                    { "64", "Саратовская область" },
                    { "65", "Сахалинская область" },
                    { "66", "Свердловская область" },
                    { "67", "Смоленская область" },
                    { "68", "Тамбовская область" },
                    { "69", "Тверская область" },
                    { "70", "Томская область" },
                    { "71", "Тульская область" },
                    { "72", "Тюменская область" },
                    { "73", "Ульяновская область" },
                    { "74", "Челябинская область" },
                    { "75", "Забайкальский край" },
                    { "76", "Ярославская область" },
                    { "77", "г. Москва" },
                    { "78", "Санкт-Петербург" },
                    { "79", "Еврейская автономная область" },
                    { "83", "Ненецкий автономный округ" },
                    { "86", "Ханты-Мансийский автономный округ — Югра" },
                    { "87", "Чукотский автономный округ" },
                    { "89", "Ямало-Ненецкий автономный округ" },
                    { "91", "Республика Крым" },
                    { "92", "Севастополь" },
                    { "99", "Иные территории, включая город и космодром Байконур" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationPledgor_PledgorsId",
                table: "NotificationPledgor",
                column: "PledgorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ContractsID",
                table: "Notifications",
                column: "ContractsID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_registrationCertificateId",
                table: "Notifications",
                column: "registrationCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_Pledgor_Organizations_RegionCodeRegion",
                table: "Pledgor",
                column: "Organizations_RegionCodeRegion");

            migrationBuilder.CreateIndex(
                name: "IX_Pledgor_RegionCodeRegion",
                table: "Pledgor",
                column: "RegionCodeRegion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationPledgor");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Pledgor");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "RegistrationCertificate");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
