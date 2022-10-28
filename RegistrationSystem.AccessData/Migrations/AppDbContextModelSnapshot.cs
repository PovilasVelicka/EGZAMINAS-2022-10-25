﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistrationSystem.AccessData;

#nullable disable

namespace RegistrationSystem.AccessData.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RegistrationSystem.Entities.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(460)
                        .HasColumnType("varbinary(460)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(460)
                        .HasColumnType("varbinary(460)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Accounts", "RegistrationSystem");
                });

            modelBuilder.Entity("RegistrationSystem.Entities.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AppartmentNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Addresses", "RegistrationSystem");
                });

            modelBuilder.Entity("RegistrationSystem.Entities.Models.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("PersonalCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<byte[]>("ProfilePicture")
                        .IsRequired()
                        .HasMaxLength(330000)
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("AddressId");

                    b.ToTable("UserInfos", "RegistrationSystem");
                });

            modelBuilder.Entity("RegistrationSystem.Entities.Models.UserInfo", b =>
                {
                    b.HasOne("RegistrationSystem.Entities.Models.Account", "Account")
                        .WithOne("UserInfo")
                        .HasForeignKey("RegistrationSystem.Entities.Models.UserInfo", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RegistrationSystem.Entities.Models.Address", "Address")
                        .WithMany("UserInfos")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("RegistrationSystem.Entities.Models.Account", b =>
                {
                    b.Navigation("UserInfo")
                        .IsRequired();
                });

            modelBuilder.Entity("RegistrationSystem.Entities.Models.Address", b =>
                {
                    b.Navigation("UserInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
