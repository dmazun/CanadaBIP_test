﻿// <auto-generated />
using System;
using CanadaBIP_test.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CanadaBIP_test.Migrations
{
    [DbContext(typeof(BudgetDbContext))]
    [Migration("20231109190240_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CanadaBIP_test.Models.BudgetManagerViewModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<decimal>("Amount_Allocated")
                        .HasColumnType("decimal(38, 2)");

                    b.Property<decimal?>("Amount_Budget")
                        .HasColumnType("decimal(38, 2)");

                    b.Property<decimal?>("Amount_Left")
                        .HasColumnType("decimal(38, 2)");

                    b.Property<string>("BU")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("BU_NAME")
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("Changed")
                        .HasColumnType("datetime2");

                    b.Property<string>("Changer")
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Creator")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Employee_Email")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Employee_ID")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Employee_Name")
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("Is_BMD")
                        .HasColumnType("int");

                    b.Property<int?>("Is_BMR")
                        .HasColumnType("int");

                    b.Property<string>("Product")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Sales_Area_Code")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Sales_Area_Name")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Sales_Area_Type")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Sales_Force_Code")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Sales_Force_Name")
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("V_Budget_Manager", "budget");
                });
#pragma warning restore 612, 618
        }
    }
}
