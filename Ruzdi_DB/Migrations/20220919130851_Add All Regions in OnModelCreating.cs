using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ruzdi_DB.Migrations
{
    public partial class AddAllRegionsinOnModelCreating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "77",
                column: "Region",
                value: "г. Москва");

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
                    { "30", "Астраханская область" },
                    { "31", "Белгородская область" },
                    { "32", "Брянская область" },
                    { "33", "Владимирская область" },
                    { "34", "Волгоградская область" },
                    { "35", "Вологодская область" },
                    { "37", "Ивановская область" },
                    { "38", "Иркутская область" },
                    { "39", "Калининградская область" },
                    { "41", "Камчатский край" },
                    { "42", "Кемеровская область — Кузбасс" },
                    { "43", "Кировская область" },
                    { "44", "Костромская область" },
                    { "45", "Курганская область" },
                    { "46", "Курская область" },
                    { "47", "Ленинградская область" },
                    { "49", "Магаданская область" },
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
                    { "63", "Самарская область" },
                    { "64", "Саратовская область" },
                    { "65", "Сахалинская область" },
                    { "66", "Свердловская область" },
                    { "68", "Тамбовская область" },
                    { "69", "Тверская область" },
                    { "70", "Томская область" },
                    { "72", "Тюменская область" },
                    { "73", "Ульяновская область" },
                    { "74", "Челябинская область" },
                    { "75", "Забайкальский край" },
                    { "79", "Еврейская автономная область" },
                    { "83", "Ненецкий автономный округ" },
                    { "86", "Ханты-Мансийский автономный округ — Югра" },
                    { "87", "Чукотский автономный округ" },
                    { "89", "Ямало-Ненецкий автономный округ" },
                    { "91", "Республика Крым" },
                    { "92", "Севастополь" },
                    { "99", "Иные территории, включая город и космодром Байконур" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "01");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "02");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "03");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "04");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "05");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "06");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "07");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "08");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "09");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "10");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "11");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "12");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "13");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "14");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "15");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "16");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "17");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "18");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "19");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "20");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "21");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "22");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "23");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "24");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "25");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "26");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "27");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "28");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "30");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "31");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "32");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "33");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "34");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "35");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "37");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "38");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "39");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "41");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "42");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "43");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "44");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "45");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "46");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "47");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "49");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "51");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "52");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "53");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "54");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "55");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "56");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "57");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "58");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "59");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "60");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "61");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "63");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "64");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "65");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "66");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "68");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "69");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "70");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "72");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "73");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "74");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "75");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "79");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "83");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "86");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "87");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "89");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "91");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "92");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "99");

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "CodeRegion",
                keyValue: "77",
                column: "Region",
                value: "Москва");
        }
    }
}
