namespace CouponAPI.Domain.Entity.CouponDTO
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        public string? CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime DateTimeCreateCoupon { get; set; }
    }
}
