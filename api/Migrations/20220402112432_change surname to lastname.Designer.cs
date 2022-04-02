﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

namespace api.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220402112432_change surname to lastname")]
    partial class changesurnametolastname
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.15");

            modelBuilder.Entity("api.Models.Files", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuditorCardDuplicate")
                        .HasColumnType("TEXT");

                    b.Property<string>("IdCardDuplicate")
                        .HasColumnType("TEXT");

                    b.Property<string>("LicenceDuplicate")
                        .HasColumnType("TEXT");

                    b.Property<string>("MissingDocument")
                        .HasColumnType("TEXT");

                    b.Property<string>("OtherDuplicate")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhotoFiles")
                        .HasColumnType("TEXT");

                    b.Property<string>("RenameDuplicate")
                        .HasColumnType("TEXT");

                    b.Property<string>("SlipPayment")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("api.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ActionDetail")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("PageAction")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("api.Models.Services", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NewCardServices")
                        .HasColumnType("TEXT");

                    b.Property<string>("PrintServices")
                        .HasColumnType("TEXT");

                    b.Property<string>("RenewServices")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReplaceServices")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("api.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AddressContact")
                        .HasColumnType("TEXT");

                    b.Property<string>("AddressHouse")
                        .HasColumnType("TEXT");

                    b.Property<string>("AddressWork")
                        .HasColumnType("TEXT");

                    b.Property<string>("AmphurContact")
                        .HasColumnType("TEXT");

                    b.Property<string>("AmphurHouse")
                        .HasColumnType("TEXT");

                    b.Property<string>("AuditorVf")
                        .HasColumnType("TEXT");

                    b.Property<string>("Bankrupt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<int>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DistrictContact")
                        .HasColumnType("TEXT");

                    b.Property<string>("DistrictHouse")
                        .HasColumnType("TEXT");

                    b.Property<string>("DistrictWork")
                        .HasColumnType("TEXT");

                    b.Property<string>("Domicile")
                        .HasColumnType("TEXT");

                    b.Property<string>("EducateDegree")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailWork")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Imprisonment")
                        .HasColumnType("TEXT");

                    b.Property<string>("Insane")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Majority")
                        .HasColumnType("TEXT");

                    b.Property<string>("ManualVf")
                        .HasColumnType("TEXT");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("MobileWork")
                        .HasColumnType("TEXT");

                    b.Property<string>("MooContact")
                        .HasColumnType("TEXT");

                    b.Property<string>("MooHouse")
                        .HasColumnType("TEXT");

                    b.Property<string>("MooWork")
                        .HasColumnType("TEXT");

                    b.Property<string>("Occupation")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("BLOB");

                    b.Property<string>("PerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneWork")
                        .HasColumnType("TEXT");

                    b.Property<string>("PostDelivery")
                        .HasColumnType("TEXT");

                    b.Property<string>("PrefixName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProvinceContact")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProvinceHouse")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProvinceWork")
                        .HasColumnType("TEXT");

                    b.Property<string>("Registration")
                        .HasColumnType("TEXT");

                    b.Property<string>("Revoke")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoadContact")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoadHouse")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoadWork")
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<string>("SoiContact")
                        .HasColumnType("TEXT");

                    b.Property<string>("SoiHouse")
                        .HasColumnType("TEXT");

                    b.Property<string>("SoiWork")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkPlace")
                        .HasColumnType("TEXT");

                    b.Property<string>("ZipCodeContact")
                        .HasColumnType("TEXT");

                    b.Property<string>("ZipCodeHouse")
                        .HasColumnType("TEXT");

                    b.Property<string>("ZipCodeWork")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
