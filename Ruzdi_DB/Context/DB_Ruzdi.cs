using Microsoft.EntityFrameworkCore;
using Ruzdi_DB.Entityes;

namespace Ruzdi_DB.Context
{
    public class DB_Ruzdi : DbContext
    {

        public DB_Ruzdi(DbContextOptions<DB_Ruzdi> options) : base(options)
        {            
            try
            {
                Database.Migrate();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Regions> Regions { get; set; }

        public DbSet<Persons> Persons { get; set; }

        public DbSet<Organizations> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Regions>().HasData(
                new Regions[]
                {
                    new Regions { CodeRegion = "22", Region = "Алтайский край" },
                    new Regions { CodeRegion = "28", Region = "Амурская область" },
                    new Regions { CodeRegion = "29", Region = "Архангельская область" },
                    new Regions { CodeRegion = "30", Region = "Астраханская область" },
                    new Regions { CodeRegion = "31", Region = "Белгородская область" },
                    new Regions { CodeRegion = "32", Region = "Брянская область" },
                    new Regions { CodeRegion = "33", Region = "Владимирская область" },
                    new Regions { CodeRegion = "34", Region = "Волгоградская область" },
                    new Regions { CodeRegion = "35", Region = "Вологодская область" },
                    new Regions { CodeRegion = "36", Region = "Воронежская область" },
                    new Regions { CodeRegion = "77", Region = "г. Москва" },
                    new Regions { CodeRegion = "79", Region = "Еврейская автономная область" },
                    new Regions { CodeRegion = "75", Region = "Забайкальский край" },
                    new Regions { CodeRegion = "37", Region = "Ивановская область" },
                    new Regions { CodeRegion = "99", Region = "Иные территории, включая город и космодром Байконур" },
                    new Regions { CodeRegion = "38", Region = "Иркутская область" },
                    new Regions { CodeRegion = "07", Region = "Кабардино-Балкарская Республика" },
                    new Regions { CodeRegion = "39", Region = "Калининградская область" },
                    new Regions { CodeRegion = "40", Region = "Калужская область" },
                    new Regions { CodeRegion = "41", Region = "Камчатский край" },
                    new Regions { CodeRegion = "09", Region = "Карачаево-Черкесская Республика" },
                    new Regions { CodeRegion = "42", Region = "Кемеровская область — Кузбасс" },
                    new Regions { CodeRegion = "43", Region = "Кировская область" },
                    new Regions { CodeRegion = "44", Region = "Костромская область" },
                    new Regions { CodeRegion = "23", Region = "Краснодарский край" },
                    new Regions { CodeRegion = "24", Region = "Красноярский край" },
                    new Regions { CodeRegion = "45", Region = "Курганская область" },
                    new Regions { CodeRegion = "46", Region = "Курская область" },
                    new Regions { CodeRegion = "47", Region = "Ленинградская область" },
                    new Regions { CodeRegion = "48", Region = "Липецкая область" },
                    new Regions { CodeRegion = "49", Region = "Магаданская область" },
                    new Regions { CodeRegion = "50", Region = "Московская область" },
                    new Regions { CodeRegion = "51", Region = "Мурманская область" },
                    new Regions { CodeRegion = "83", Region = "Ненецкий автономный округ" },
                    new Regions { CodeRegion = "52", Region = "Нижегородская область" },
                    new Regions { CodeRegion = "53", Region = "Новгородская область" },
                    new Regions { CodeRegion = "54", Region = "Новосибирская область" },
                    new Regions { CodeRegion = "55", Region = "Омская область" },
                    new Regions { CodeRegion = "56", Region = "Оренбургская область" },
                    new Regions { CodeRegion = "57", Region = "Орловская область" },
                    new Regions { CodeRegion = "58", Region = "Пензенская область" },
                    new Regions { CodeRegion = "59", Region = "Пермский край" },
                    new Regions { CodeRegion = "25", Region = "Приморский край" },
                    new Regions { CodeRegion = "60", Region = "Псковская область" },
                    new Regions { CodeRegion = "01", Region = "Республика Адыгея (Адыгея)" },
                    new Regions { CodeRegion = "04", Region = "Республика Алтай" },
                    new Regions { CodeRegion = "02", Region = "Республика Башкортостан" },
                    new Regions { CodeRegion = "03", Region = "Республика Бурятия" },
                    new Regions { CodeRegion = "05", Region = "Республика Дагестан" },
                    new Regions { CodeRegion = "06", Region = "Республика Ингушетия" },
                    new Regions { CodeRegion = "08", Region = "Республика Калмыкия" },
                    new Regions { CodeRegion = "10", Region = "Республика Карелия" },
                    new Regions { CodeRegion = "11", Region = "Республика Коми" },
                    new Regions { CodeRegion = "91", Region = "Республика Крым" },
                    new Regions { CodeRegion = "12", Region = "Республика Марий Эл" },
                    new Regions { CodeRegion = "13", Region = "Республика Мордовия" },
                    new Regions { CodeRegion = "14", Region = "Республика Саха (Якутия)" },
                    new Regions { CodeRegion = "15", Region = "Республика Северная Осетия — Алания" },
                    new Regions { CodeRegion = "16", Region = "Республика Татарстан (Татарстан)" },
                    new Regions { CodeRegion = "17", Region = "Республика Тыва" },
                    new Regions { CodeRegion = "19", Region = "Республика Хакасия" },
                    new Regions { CodeRegion = "61", Region = "Ростовская область" },
                    new Regions { CodeRegion = "62", Region = "Рязанская область" },
                    new Regions { CodeRegion = "63", Region = "Самарская область" },
                    new Regions { CodeRegion = "78", Region = "Санкт-Петербург" },
                    new Regions { CodeRegion = "64", Region = "Саратовская область" },
                    new Regions { CodeRegion = "65", Region = "Сахалинская область" },
                    new Regions { CodeRegion = "66", Region = "Свердловская область" },
                    new Regions { CodeRegion = "92", Region = "Севастополь" },
                    new Regions { CodeRegion = "67", Region = "Смоленская область" },
                    new Regions { CodeRegion = "26", Region = "Ставропольский край" },
                    new Regions { CodeRegion = "68", Region = "Тамбовская область" },
                    new Regions { CodeRegion = "69", Region = "Тверская область" },
                    new Regions { CodeRegion = "70", Region = "Томская область" },
                    new Regions { CodeRegion = "71", Region = "Тульская область" },
                    new Regions { CodeRegion = "72", Region = "Тюменская область" },
                    new Regions { CodeRegion = "18", Region = "Удмуртская Республика" },
                    new Regions { CodeRegion = "73", Region = "Ульяновская область" },
                    new Regions { CodeRegion = "27", Region = "Хабаровский край" },
                    new Regions { CodeRegion = "86", Region = "Ханты-Мансийский автономный округ — Югра" },
                    new Regions { CodeRegion = "74", Region = "Челябинская область" },
                    new Regions { CodeRegion = "20", Region = "Чеченская Республика" },
                    new Regions { CodeRegion = "21", Region = "Чувашская Республика — Чувашия" },
                    new Regions { CodeRegion = "87", Region = "Чукотский автономный округ" },
                    new Regions { CodeRegion = "89", Region = "Ямало-Ненецкий автономный округ" },
                    new Regions { CodeRegion = "76", Region = "Ярославская область" }
                }
            );
        }

    }

}
