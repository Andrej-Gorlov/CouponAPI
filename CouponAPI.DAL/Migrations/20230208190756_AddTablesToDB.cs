using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    CouponId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CouponCode = table.Column<string>(type: "text", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    DateTimeCreateCoupon = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    { 1, "05OGA", new DateTime(2023, 2, 8, 22, 7, 56, 98, DateTimeKind.Local).AddTicks(5127), 5m },
                    { 2, "09OGA", new DateTime(2023, 2, 8, 22, 7, 56, 98, DateTimeKind.Local).AddTicks(5153), 9m }
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
