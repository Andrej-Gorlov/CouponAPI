using CouponAPI.Domain.Entity;

namespace CouponAPI.DAL
{
    public class OriginData
    {
        public static async Task SeedData(ApplicationDbContext context)
        {
            if (context.Coupons.Any()) return;

            var coupons = new List<Coupon>
            {
                new Coupon
                {
                    CouponId = 1,
                    CouponCode = "05OGA",
                    DiscountAmount = 5
                },
                new Coupon
                {
                    CouponId = 2,
                CouponCode = "09OGA",
                DiscountAmount = 9
                },
            };

            await context.Coupons.AddRangeAsync(coupons);
            await context.SaveChangesAsync();
        }
    }
}
