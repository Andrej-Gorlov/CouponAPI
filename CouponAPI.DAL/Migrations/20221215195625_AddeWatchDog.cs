using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CouponAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddeWatchDog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1,
                column: "DateTimeCreateCoupon",
                value: new DateTime(2022, 12, 15, 22, 56, 25, 197, DateTimeKind.Local).AddTicks(9098));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 2,
                column: "DateTimeCreateCoupon",
                value: new DateTime(2022, 12, 15, 22, 56, 25, 197, DateTimeKind.Local).AddTicks(9131));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1,
                column: "DateTimeCreateCoupon",
                value: new DateTime(2022, 12, 15, 22, 53, 47, 456, DateTimeKind.Local).AddTicks(1366));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 2,
                column: "DateTimeCreateCoupon",
                value: new DateTime(2022, 12, 15, 22, 53, 47, 456, DateTimeKind.Local).AddTicks(1398));
        }
    }
}
