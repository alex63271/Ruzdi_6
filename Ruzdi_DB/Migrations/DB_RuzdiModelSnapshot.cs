﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ruzdi_DB.Context;

#nullable disable

namespace Ruzdi_DB.Migrations
{
    [DbContext(typeof(DB_Ruzdi))]
    partial class DB_RuzdiModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("NotificationPledgor", b =>
                {
                    b.Property<string>("NotificationsId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("PledgorsId")
                        .HasColumnType("int");

                    b.HasKey("NotificationsId", "PledgorsId");

                    b.HasIndex("PledgorsId");

                    b.ToTable("NotificationPledgor");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Contracts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateOnly>("Data")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateOnly>("TermOfContract")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Notification", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("ContractsID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Error")
                        .HasColumnType("longtext");

                    b.Property<string>("NumberNotification")
                        .HasColumnType("longtext");

                    b.Property<string>("Packageguid")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Packageid")
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ThumbprintCert")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TypeNotification")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ZipArchive")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("registrationCertificateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContractsID");

                    b.HasIndex("registrationCertificateId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Pledgor", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Pledgor");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Pledgor");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Regions", b =>
                {
                    b.Property<string>("CodeRegion")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CodeRegion");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            CodeRegion = "22",
                            Region = "Алтайский край"
                        },
                        new
                        {
                            CodeRegion = "28",
                            Region = "Амурская область"
                        },
                        new
                        {
                            CodeRegion = "29",
                            Region = "Архангельская область"
                        },
                        new
                        {
                            CodeRegion = "30",
                            Region = "Астраханская область"
                        },
                        new
                        {
                            CodeRegion = "31",
                            Region = "Белгородская область"
                        },
                        new
                        {
                            CodeRegion = "32",
                            Region = "Брянская область"
                        },
                        new
                        {
                            CodeRegion = "33",
                            Region = "Владимирская область"
                        },
                        new
                        {
                            CodeRegion = "34",
                            Region = "Волгоградская область"
                        },
                        new
                        {
                            CodeRegion = "35",
                            Region = "Вологодская область"
                        },
                        new
                        {
                            CodeRegion = "36",
                            Region = "Воронежская область"
                        },
                        new
                        {
                            CodeRegion = "77",
                            Region = "г. Москва"
                        },
                        new
                        {
                            CodeRegion = "79",
                            Region = "Еврейская автономная область"
                        },
                        new
                        {
                            CodeRegion = "75",
                            Region = "Забайкальский край"
                        },
                        new
                        {
                            CodeRegion = "37",
                            Region = "Ивановская область"
                        },
                        new
                        {
                            CodeRegion = "99",
                            Region = "Иные территории, включая город и космодром Байконур"
                        },
                        new
                        {
                            CodeRegion = "38",
                            Region = "Иркутская область"
                        },
                        new
                        {
                            CodeRegion = "07",
                            Region = "Кабардино-Балкарская Республика"
                        },
                        new
                        {
                            CodeRegion = "39",
                            Region = "Калининградская область"
                        },
                        new
                        {
                            CodeRegion = "40",
                            Region = "Калужская область"
                        },
                        new
                        {
                            CodeRegion = "41",
                            Region = "Камчатский край"
                        },
                        new
                        {
                            CodeRegion = "09",
                            Region = "Карачаево-Черкесская Республика"
                        },
                        new
                        {
                            CodeRegion = "42",
                            Region = "Кемеровская область — Кузбасс"
                        },
                        new
                        {
                            CodeRegion = "43",
                            Region = "Кировская область"
                        },
                        new
                        {
                            CodeRegion = "44",
                            Region = "Костромская область"
                        },
                        new
                        {
                            CodeRegion = "23",
                            Region = "Краснодарский край"
                        },
                        new
                        {
                            CodeRegion = "24",
                            Region = "Красноярский край"
                        },
                        new
                        {
                            CodeRegion = "45",
                            Region = "Курганская область"
                        },
                        new
                        {
                            CodeRegion = "46",
                            Region = "Курская область"
                        },
                        new
                        {
                            CodeRegion = "47",
                            Region = "Ленинградская область"
                        },
                        new
                        {
                            CodeRegion = "48",
                            Region = "Липецкая область"
                        },
                        new
                        {
                            CodeRegion = "49",
                            Region = "Магаданская область"
                        },
                        new
                        {
                            CodeRegion = "50",
                            Region = "Московская область"
                        },
                        new
                        {
                            CodeRegion = "51",
                            Region = "Мурманская область"
                        },
                        new
                        {
                            CodeRegion = "83",
                            Region = "Ненецкий автономный округ"
                        },
                        new
                        {
                            CodeRegion = "52",
                            Region = "Нижегородская область"
                        },
                        new
                        {
                            CodeRegion = "53",
                            Region = "Новгородская область"
                        },
                        new
                        {
                            CodeRegion = "54",
                            Region = "Новосибирская область"
                        },
                        new
                        {
                            CodeRegion = "55",
                            Region = "Омская область"
                        },
                        new
                        {
                            CodeRegion = "56",
                            Region = "Оренбургская область"
                        },
                        new
                        {
                            CodeRegion = "57",
                            Region = "Орловская область"
                        },
                        new
                        {
                            CodeRegion = "58",
                            Region = "Пензенская область"
                        },
                        new
                        {
                            CodeRegion = "59",
                            Region = "Пермский край"
                        },
                        new
                        {
                            CodeRegion = "25",
                            Region = "Приморский край"
                        },
                        new
                        {
                            CodeRegion = "60",
                            Region = "Псковская область"
                        },
                        new
                        {
                            CodeRegion = "01",
                            Region = "Республика Адыгея (Адыгея)"
                        },
                        new
                        {
                            CodeRegion = "04",
                            Region = "Республика Алтай"
                        },
                        new
                        {
                            CodeRegion = "02",
                            Region = "Республика Башкортостан"
                        },
                        new
                        {
                            CodeRegion = "03",
                            Region = "Республика Бурятия"
                        },
                        new
                        {
                            CodeRegion = "05",
                            Region = "Республика Дагестан"
                        },
                        new
                        {
                            CodeRegion = "06",
                            Region = "Республика Ингушетия"
                        },
                        new
                        {
                            CodeRegion = "08",
                            Region = "Республика Калмыкия"
                        },
                        new
                        {
                            CodeRegion = "10",
                            Region = "Республика Карелия"
                        },
                        new
                        {
                            CodeRegion = "11",
                            Region = "Республика Коми"
                        },
                        new
                        {
                            CodeRegion = "91",
                            Region = "Республика Крым"
                        },
                        new
                        {
                            CodeRegion = "12",
                            Region = "Республика Марий Эл"
                        },
                        new
                        {
                            CodeRegion = "13",
                            Region = "Республика Мордовия"
                        },
                        new
                        {
                            CodeRegion = "14",
                            Region = "Республика Саха (Якутия)"
                        },
                        new
                        {
                            CodeRegion = "15",
                            Region = "Республика Северная Осетия — Алания"
                        },
                        new
                        {
                            CodeRegion = "16",
                            Region = "Республика Татарстан (Татарстан)"
                        },
                        new
                        {
                            CodeRegion = "17",
                            Region = "Республика Тыва"
                        },
                        new
                        {
                            CodeRegion = "19",
                            Region = "Республика Хакасия"
                        },
                        new
                        {
                            CodeRegion = "61",
                            Region = "Ростовская область"
                        },
                        new
                        {
                            CodeRegion = "62",
                            Region = "Рязанская область"
                        },
                        new
                        {
                            CodeRegion = "63",
                            Region = "Самарская область"
                        },
                        new
                        {
                            CodeRegion = "78",
                            Region = "Санкт-Петербург"
                        },
                        new
                        {
                            CodeRegion = "64",
                            Region = "Саратовская область"
                        },
                        new
                        {
                            CodeRegion = "65",
                            Region = "Сахалинская область"
                        },
                        new
                        {
                            CodeRegion = "66",
                            Region = "Свердловская область"
                        },
                        new
                        {
                            CodeRegion = "92",
                            Region = "Севастополь"
                        },
                        new
                        {
                            CodeRegion = "67",
                            Region = "Смоленская область"
                        },
                        new
                        {
                            CodeRegion = "26",
                            Region = "Ставропольский край"
                        },
                        new
                        {
                            CodeRegion = "68",
                            Region = "Тамбовская область"
                        },
                        new
                        {
                            CodeRegion = "69",
                            Region = "Тверская область"
                        },
                        new
                        {
                            CodeRegion = "70",
                            Region = "Томская область"
                        },
                        new
                        {
                            CodeRegion = "71",
                            Region = "Тульская область"
                        },
                        new
                        {
                            CodeRegion = "72",
                            Region = "Тюменская область"
                        },
                        new
                        {
                            CodeRegion = "18",
                            Region = "Удмуртская Республика"
                        },
                        new
                        {
                            CodeRegion = "73",
                            Region = "Ульяновская область"
                        },
                        new
                        {
                            CodeRegion = "27",
                            Region = "Хабаровский край"
                        },
                        new
                        {
                            CodeRegion = "86",
                            Region = "Ханты-Мансийский автономный округ — Югра"
                        },
                        new
                        {
                            CodeRegion = "74",
                            Region = "Челябинская область"
                        },
                        new
                        {
                            CodeRegion = "20",
                            Region = "Чеченская Республика"
                        },
                        new
                        {
                            CodeRegion = "21",
                            Region = "Чувашская Республика — Чувашия"
                        },
                        new
                        {
                            CodeRegion = "87",
                            Region = "Чукотский автономный округ"
                        },
                        new
                        {
                            CodeRegion = "89",
                            Region = "Ямало-Ненецкий автономный округ"
                        },
                        new
                        {
                            CodeRegion = "76",
                            Region = "Ярославская область"
                        });
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.RegistrationCertificate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("documentAndSignature")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("RegistrationCertificate");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Organizations", b =>
                {
                    b.HasBaseType("Ruzdi_DB.Entityes.Pledgor");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("INN")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NameFull")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("OGRN")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RegionCodeRegion")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("Organizations_RegionCodeRegion");

                    b.HasIndex("RegionCodeRegion");

                    b.HasDiscriminator().HasValue("Organizations");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Persons", b =>
                {
                    b.HasBaseType("Ruzdi_DB.Entityes.Pledgor");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("First")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Last")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Middle")
                        .HasColumnType("longtext");

                    b.Property<string>("PersonDocument")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RegionCodeRegion")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasIndex("RegionCodeRegion");

                    b.HasDiscriminator().HasValue("Persons");
                });

            modelBuilder.Entity("NotificationPledgor", b =>
                {
                    b.HasOne("Ruzdi_DB.Entityes.Notification", null)
                        .WithMany()
                        .HasForeignKey("NotificationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ruzdi_DB.Entityes.Pledgor", null)
                        .WithMany()
                        .HasForeignKey("PledgorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Notification", b =>
                {
                    b.HasOne("Ruzdi_DB.Entityes.Contracts", "PledgeContract")
                        .WithMany("Notifications")
                        .HasForeignKey("ContractsID");

                    b.HasOne("Ruzdi_DB.Entityes.RegistrationCertificate", "registrationCertificate")
                        .WithMany()
                        .HasForeignKey("registrationCertificateId");

                    b.Navigation("PledgeContract");

                    b.Navigation("registrationCertificate");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Organizations", b =>
                {
                    b.HasOne("Ruzdi_DB.Entityes.Regions", "Region")
                        .WithMany("Organizations")
                        .HasForeignKey("RegionCodeRegion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Persons", b =>
                {
                    b.HasOne("Ruzdi_DB.Entityes.Regions", "Region")
                        .WithMany("Persons")
                        .HasForeignKey("RegionCodeRegion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Contracts", b =>
                {
                    b.Navigation("Notifications");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Regions", b =>
                {
                    b.Navigation("Organizations");

                    b.Navigation("Persons");
                });
#pragma warning restore 612, 618
        }
    }
}
