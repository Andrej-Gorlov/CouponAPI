using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CouponAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddWatchDog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1,
                column: "DateTimeCreateCoupon",
                value: new DateTime(2023, 2, 8, 22, 8, 50, 203, DateTimeKind.Local).AddTicks(497));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 2,
                column: "DateTimeCreateCoupon",
                value: new DateTime(2023, 2, 8, 22, 8, 50, 203, DateTimeKind.Local).AddTicks(526));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1,
                column: "DateTimeCreateCoupon",
                value: new DateTime(2023, 2, 8, 22, 7, 56, 98, DateTimeKind.Local).AddTicks(5127));

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 2,
                column: "DateTimeCreateCoupon",
                value: new DateTime(2023, 2, 8, 22, 7, 56, 98, DateTimeKind.Local).AddTicks(5153));
        }
    }
}
