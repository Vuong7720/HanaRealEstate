﻿// <auto-generated />
using System;
using Hana.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hana.Migrations
{
    [DbContext(typeof(HanaContext))]
    [Migration("20231107021850_changeLengthUrl")]
    partial class changeLengthUrl
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hana.Models.DataModels.AboutUs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.HasKey("Id");

                    b.ToTable("ABOUT_US", (string)null);
                });

            modelBuilder.Entity("Hana.Models.DataModels.Agent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ActiveKey")
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("AgentName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("ConfirmPhoneNumber")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Facebook")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<string>("LoginName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("ResetPasswordKey")
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Zalo")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.ToTable("AGENT", (string)null);
                });

            modelBuilder.Entity("Hana.Models.DataModels.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("CITY", (string)null);
                });

            modelBuilder.Entity("Hana.Models.DataModels.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("DistrictName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("DISTRICT", (string)null);
                });

            modelBuilder.Entity("Hana.Models.DataModels.Faq", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .HasColumnType("ntext");

                    b.Property<string>("Question")
                        .HasColumnType("ntext");

                    b.HasKey("Id");

                    b.ToTable("FAQ", (string)null);
                });

            modelBuilder.Entity("Hana.Models.DataModels.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LevelName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("LEVEL", (string)null);
                });

            modelBuilder.Entity("Hana.Models.DataModels.Map", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<int?>("DistrictId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<decimal?>("Longtitude")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<int?>("RealEstateId")
                        .HasColumnType("int");

                    b.Property<int?>("WardId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("RealEstateId")
                        .IsUnique()
                        .HasDatabaseName("UQ__MAP__C037863418DC284F")
                        .HasFilter("[RealEstateId] IS NOT NULL");

                    b.HasIndex("WardId");

                    b.ToTable("MAP", (string)null);
                });

            modelBuilder.Entity("Hana.Models.DataModels.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("NativeHeight")
                        .HasColumnType("int");

                    b.Property<int>("NativeWidth")
                        .HasColumnType("int");

                    b.Property<string>("PictureName")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("RealEstateId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("URL");

                    b.HasKey("Id");

                    b.HasIndex("RealEstateId");

                    b.ToTable("PICTURE", (string)null);
                });

            modelBuilder.Entity("Hana.Models.DataModels.Policy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PolicyContent")
                        .HasColumnType("ntext");

                    b.HasKey("Id");

                    b.ToTable("POLICY", (string)null);
                });

            modelBuilder.Entity("Hana.Models.DataModels.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<int>("RealEstateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RealEstateId");

                    b.ToTable("RATING", (string)null);
                });

            modelBuilder.Entity("Hana.Models.DataModels.RealEstate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AgentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BeginTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ConfirmStatus")
                        .HasColumnType("int");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExprireTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAvaiable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsConfirm")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("PostTime")
                        .HasColumnType("datetime");

                    b.Property<int?>("RealEstateTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AgentId");

                    b.HasIndex("RealEstateTypeId");

                    b.ToTable("REAL_ESTATE", (string)null);
                });

            modelBuilder.Entity("Hana.Models.DataModels.RealEstateDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Acreage")
                        .HasColumnType("int");

                    b.Property<bool>("AllowCook")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("ntext");

                    b.Property<int?>("ElectronicPrice")
                        .HasColumnType("int");

                    b.Property<bool>("FreeTime")
                        .HasColumnType("bit");

                    b.Property<bool>("HasMezzanine")
                        .HasColumnType("bit");

                    b.Property<bool>("HasPrivateWc")
                        .HasColumnType("bit")
                        .HasColumnName("HasPrivateWC");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<int?>("RealEstateId")
                        .HasColumnType("int");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<bool>("SecurityCamera")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int?>("WaterPrice")
                        .HasColumnType("int");

                    b.Property<decimal?>("WifiPrice")
                        .HasColumnType("decimal(18, 0)");

                    b.HasKey("Id");

                    b.HasIndex("RealEstateId")
                        .IsUnique()
                        .HasDatabaseName("UQ__REAL_EST__C0378634981FED9E")
                        .HasFilter("[RealEstateId] IS NOT NULL");

                    b.ToTable("REAL_ESTATE_DETAIL", (string)null);
                });

            modelBuilder.Entity("Hana.Models.DataModels.RealEstateType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RealEstateTypeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("REAL_ESTATE_TYPE", (string)null);
                });

            modelBuilder.Entity("Hana.Models.DataModels.SocialLogin", b =>
                {
                    b.Property<string>("ProviderKey")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Provider")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ProviderKey")
                        .HasName("PK__SOCIAL_L__8DE43C5E9E312291");

                    b.HasIndex("UserId");

                    b.ToTable("SOCIAL_LOGIN", (string)null);
                });

            modelBuilder.Entity("Hana.Models.DataModels.Ward", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DistrictId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("WardName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("WARD", (string)null);
                });

            modelBuilder.Entity("Hana.Models.DataModels.Agent", b =>
                {
                    b.HasOne("Hana.Models.DataModels.Level", "Level")
                        .WithMany("Agent")
                        .HasForeignKey("LevelId")
                        .IsRequired()
                        .HasConstraintName("FK__AGENT__LevelId__3B75D760");

                    b.Navigation("Level");
                });

            modelBuilder.Entity("Hana.Models.DataModels.District", b =>
                {
                    b.HasOne("Hana.Models.DataModels.City", "City")
                        .WithMany("District")
                        .HasForeignKey("CityId")
                        .HasConstraintName("FK__DISTRICT__CityId__145C0A3F");

                    b.Navigation("City");
                });

            modelBuilder.Entity("Hana.Models.DataModels.Map", b =>
                {
                    b.HasOne("Hana.Models.DataModels.City", "City")
                        .WithMany("Map")
                        .HasForeignKey("CityId")
                        .HasConstraintName("FK__MAP__CityId__48CFD27E");

                    b.HasOne("Hana.Models.DataModels.District", "District")
                        .WithMany("Map")
                        .HasForeignKey("DistrictId")
                        .HasConstraintName("FK__MAP__DistrictId__47DBAE45");

                    b.HasOne("Hana.Models.DataModels.RealEstate", "RealEstate")
                        .WithOne("Map")
                        .HasForeignKey("Hana.Models.DataModels.Map", "RealEstateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__MAP__RealEstateI__49C3F6B7");

                    b.HasOne("Hana.Models.DataModels.Ward", "Ward")
                        .WithMany("Map")
                        .HasForeignKey("WardId")
                        .HasConstraintName("FK__MAP__WardId__46E78A0C");

                    b.Navigation("City");

                    b.Navigation("District");

                    b.Navigation("RealEstate");

                    b.Navigation("Ward");
                });

            modelBuilder.Entity("Hana.Models.DataModels.Picture", b =>
                {
                    b.HasOne("Hana.Models.DataModels.RealEstate", "RealEstate")
                        .WithMany("Picture")
                        .HasForeignKey("RealEstateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__PICTURE__RealEst__4CA06362");

                    b.Navigation("RealEstate");
                });

            modelBuilder.Entity("Hana.Models.DataModels.Rating", b =>
                {
                    b.HasOne("Hana.Models.DataModels.RealEstate", "RealEstate")
                        .WithMany("Rating")
                        .HasForeignKey("RealEstateId")
                        .IsRequired()
                        .HasConstraintName("FK__RATING__RealEsta__4F7CD00D");

                    b.Navigation("RealEstate");
                });

            modelBuilder.Entity("Hana.Models.DataModels.RealEstate", b =>
                {
                    b.HasOne("Hana.Models.DataModels.Agent", "Agent")
                        .WithMany("RealEstate")
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__REAL_ESTA__Agent__3F466844");

                    b.HasOne("Hana.Models.DataModels.RealEstateType", "ReaEstateType")
                        .WithMany("RealEstate")
                        .HasForeignKey("RealEstateTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__REAL_ESTA__ReaEs__3E52440B");

                    b.Navigation("Agent");

                    b.Navigation("ReaEstateType");
                });

            modelBuilder.Entity("Hana.Models.DataModels.RealEstateDetail", b =>
                {
                    b.HasOne("Hana.Models.DataModels.RealEstate", "RealEstate")
                        .WithOne("RealEstateDetail")
                        .HasForeignKey("Hana.Models.DataModels.RealEstateDetail", "RealEstateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__REAL_ESTA__RealE__5629CD9C");

                    b.Navigation("RealEstate");
                });

            modelBuilder.Entity("Hana.Models.DataModels.SocialLogin", b =>
                {
                    b.HasOne("Hana.Models.DataModels.Agent", "User")
                        .WithMany("SocialLogin")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__SOCIAL_LO__UserI__52593CB8");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Hana.Models.DataModels.Ward", b =>
                {
                    b.HasOne("Hana.Models.DataModels.District", "District")
                        .WithMany("Ward")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__WARD__DistrictId__173876EA");

                    b.Navigation("District");
                });

            modelBuilder.Entity("Hana.Models.DataModels.Agent", b =>
                {
                    b.Navigation("RealEstate");

                    b.Navigation("SocialLogin");
                });

            modelBuilder.Entity("Hana.Models.DataModels.City", b =>
                {
                    b.Navigation("District");

                    b.Navigation("Map");
                });

            modelBuilder.Entity("Hana.Models.DataModels.District", b =>
                {
                    b.Navigation("Map");

                    b.Navigation("Ward");
                });

            modelBuilder.Entity("Hana.Models.DataModels.Level", b =>
                {
                    b.Navigation("Agent");
                });

            modelBuilder.Entity("Hana.Models.DataModels.RealEstate", b =>
                {
                    b.Navigation("Map");

                    b.Navigation("Picture");

                    b.Navigation("Rating");

                    b.Navigation("RealEstateDetail");
                });

            modelBuilder.Entity("Hana.Models.DataModels.RealEstateType", b =>
                {
                    b.Navigation("RealEstate");
                });

            modelBuilder.Entity("Hana.Models.DataModels.Ward", b =>
                {
                    b.Navigation("Map");
                });
#pragma warning restore 612, 618
        }
    }
}
