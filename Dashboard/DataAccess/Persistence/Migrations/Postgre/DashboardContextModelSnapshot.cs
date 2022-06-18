﻿// <auto-generated />
using System;
using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Persistence.Migrations.Postgre
{
    [DbContext(typeof(DashboardContext))]
    partial class DashboardContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.Account", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ActivateCode")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Fullname")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Accounts", "public");
                });

            modelBuilder.Entity("Core.Entities.Configs", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AdditionalProp1")
                        .HasColumnType("jsonb");

                    b.Property<string>("AdditionalProp2")
                        .HasColumnType("jsonb");

                    b.Property<string>("AdditionalProp3")
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.ToTable("Configs", "public");
                });

            modelBuilder.Entity("Core.Entities.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Avatar")
                        .HasColumnType("text");

                    b.Property<string>("Department")
                        .HasColumnType("text");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Project")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Contacts", "public");
                });

            modelBuilder.Entity("Core.Entities.Dashboard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ConfigsId")
                        .HasColumnType("uuid");

                    b.Property<string>("LayoutType")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ConfigsId");

                    b.HasIndex("UserId");

                    b.ToTable("Dashboards", "public");
                });

            modelBuilder.Entity("Core.Entities.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<string>("TaskName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks", "public");
                });

            modelBuilder.Entity("Core.Entities.Token", b =>
                {
                    b.Property<Guid>("RefreshToken")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccessToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ExpiredDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("IsRevoked")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("RefreshToken");

                    b.HasIndex("UserId");

                    b.ToTable("Tokens", "public");
                });

            modelBuilder.Entity("Core.Entities.Widget", b =>
                {
                    b.Property<Guid>("WidgetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ConfigsId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DashboardId")
                        .HasColumnType("uuid");

                    b.Property<int?>("MinWidth")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<string>("WidgetType")
                        .HasColumnType("text");

                    b.Property<int?>("minHeight")
                        .HasColumnType("integer");

                    b.HasKey("WidgetId");

                    b.HasIndex("ConfigsId");

                    b.HasIndex("DashboardId");

                    b.ToTable("Widgets", "public");
                });

            modelBuilder.Entity("Core.Entities.Dashboard", b =>
                {
                    b.HasOne("Core.Entities.Configs", "Configs")
                        .WithMany()
                        .HasForeignKey("ConfigsId");

                    b.HasOne("Core.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Configs");
                });

            modelBuilder.Entity("Core.Entities.Task", b =>
                {
                    b.HasOne("Core.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Core.Entities.Token", b =>
                {
                    b.HasOne("Core.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Core.Entities.Widget", b =>
                {
                    b.HasOne("Core.Entities.Configs", "Configs")
                        .WithMany()
                        .HasForeignKey("ConfigsId");

                    b.HasOne("Core.Entities.Dashboard", null)
                        .WithMany("Widgets")
                        .HasForeignKey("DashboardId");

                    b.Navigation("Configs");
                });

            modelBuilder.Entity("Core.Entities.Dashboard", b =>
                {
                    b.Navigation("Widgets");
                });
#pragma warning restore 612, 618
        }
    }
}
