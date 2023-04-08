namespace CouponAPI.Domain.Entity.CouponDTO
{
    public record struct CouponDTO
    {
        public CouponDTO(int couponId, string couponCode, decimal discountAmount, DateTime dateTimeCreateCoupon)
        {
            CouponId = couponId;
            CouponCode = couponCode;
            DiscountAmount = discountAmount;
            DateTimeCreateCoupon = dateTimeCreateCoupon;
        }
        public int CouponId { get; init; }
        public string CouponCode { get; init; }
        public decimal DiscountAmount { get; init; }
        public DateTime DateTimeCreateCoupon { get; init; } 
    }
}
