﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ruzdi_DB.Context;

#nullable disable

namespace Ruzdi_DB.Migrations
{
    [DbContext(typeof(DB_Ruzdi))]
    [Migration("20220630065841_30.06")]
    partial class _3006
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Ruzdi_DB.Entityes.Contracts", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(95)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("TermOfContract")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Notification", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(95)");

                    b.Property<DateTime>("DataTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Error")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NumberNotification")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Packageguid")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Packageid")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PledgeContractId")
                        .IsRequired()
                        .HasColumnType("varchar(95)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TypeNotification")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ZipArchive")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("registrationCertificateId")
                        .HasColumnType("varchar(95)");

                    b.HasKey("Id");

                    b.HasIndex("PledgeContractId");

                    b.HasIndex("registrationCertificateId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Organizations", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(95)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<uint>("INN")
                        .HasColumnType("int unsigned");

                    b.Property<string>("NameFull")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<uint>("OGRN")
                        .HasColumnType("int unsigned");

                    b.Property<string>("RegionId")
                        .IsRequired()
                        .HasColumnType("varchar(95)");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Persons", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(95)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("First")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Last")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Middle")
                        .HasColumnType("longtext");

                    b.Property<uint>("PersonDocument")
                        .HasColumnType("int unsigned");

                    b.Property<string>("RegionId")
                        .IsRequired()
                        .HasColumnType("varchar(95)");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Pledgor", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(95)");

                    b.Property<string>("NotificationId")
                        .HasColumnType("varchar(95)");

                    b.Property<string>("OrganizationId")
                        .HasColumnType("varchar(95)");

                    b.Property<string>("PersonId")
                        .HasColumnType("varchar(95)");

                    b.HasKey("Id");

                    b.HasIndex("NotificationId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("PersonId");

                    b.ToTable("Pledgor");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Regions", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(95)");

                    b.Property<string>("CodeRegion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.RegistrationCertificate", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(95)");

                    b.Property<string>("documentAndSignature")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("RegistrationCertificate");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Notification", b =>
                {
                    b.HasOne("Ruzdi_DB.Entityes.Contracts", "PledgeContract")
                        .WithMany()
                        .HasForeignKey("PledgeContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ruzdi_DB.Entityes.RegistrationCertificate", "registrationCertificate")
                        .WithMany()
                        .HasForeignKey("registrationCertificateId");

                    b.Navigation("PledgeContract");

                    b.Navigation("registrationCertificate");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Organizations", b =>
                {
                    b.HasOne("Ruzdi_DB.Entityes.Regions", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Persons", b =>
                {
                    b.HasOne("Ruzdi_DB.Entityes.Regions", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Pledgor", b =>
                {
                    b.HasOne("Ruzdi_DB.Entityes.Notification", null)
                        .WithMany("Pledgors")
                        .HasForeignKey("NotificationId");

                    b.HasOne("Ruzdi_DB.Entityes.Organizations", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.HasOne("Ruzdi_DB.Entityes.Persons", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.Navigation("Organization");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Ruzdi_DB.Entityes.Notification", b =>
                {
                    b.Navigation("Pledgors");
                });
#pragma warning restore 612, 618
        }
    }
}
