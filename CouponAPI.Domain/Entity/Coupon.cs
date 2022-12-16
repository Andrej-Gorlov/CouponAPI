namespace CouponAPI.Domain.Entity
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        public string? CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime DateTimeCreateCoupon { get; set; } = DateTime.Now;
    }
}
