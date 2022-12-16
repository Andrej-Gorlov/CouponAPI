namespace CouponAPI.Domain.Entity.CouponDTO
{
    public class UpdateCouponDTO
    {
        [Required(ErrorMessage = "Укажите id купона.")]
        public int CouponId { get; set; }
        [StringLength(9, MinimumLength = 5, ErrorMessage = "Код купона должен содержать быть не менее 5 и не более 9 символов")]
        public string? CouponCode { get; set; }
        [Range(0, 100, ErrorMessage = "Скидка не может быть меньше 0 и больше 100.")]
        public decimal DiscountAmount { get; set; }
    }
}
