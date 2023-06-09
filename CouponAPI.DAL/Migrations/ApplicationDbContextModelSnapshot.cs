﻿// <auto-generated />
using System;
using CouponAPI.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CouponAPI.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CouponAPI.Domain.Entity.Coupon", b =>
                {
                    b.Property<int>("CouponId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CouponId"));

                    b.Property<string>("CouponCode")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTimeCreateCoupon")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("DiscountAmount")
                        .HasColumnType("numeric");

                    b.HasKey("CouponId");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            CouponId = 1,
                            CouponCode = "05OGA",
                            DateTimeCreateCoupon = new DateTime(2023, 2, 8, 22, 8, 50, 203, DateTimeKind.Local).AddTicks(497),
                            DiscountAmount = 5m
                        },
                        new
                        {
                            CouponId = 2,
                            CouponCode = "09OGA",
                            DateTimeCreateCoupon = new DateTime(2023, 2, 8, 22, 8, 50, 203, DateTimeKind.Local).AddTicks(526),
                            DiscountAmount = 9m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
