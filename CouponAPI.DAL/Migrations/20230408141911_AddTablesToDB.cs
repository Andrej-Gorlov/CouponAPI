using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CouponAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    CouponId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CouponCode = table.Column<string>(type: "TEXT", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    DateTimeCreateCoupon = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.CouponId);
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "CouponCode", "DateTimeCreateCoupon", "DiscountAmount" },
                values: new object[,]
                {
                    { 1, "05OGA", new DateTime(2023, 4, 8, 17, 19, 11, 773, DateTimeKind.Local).AddTicks(6890), 5m },
                    { 2, "09OGA", new DateTime(2023, 4, 8, 17, 19, 11, 773, DateTimeKind.Local).AddTicks(6921), 9m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
