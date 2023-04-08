namespace CouponAPI.Domain.Entity.CouponDTO
{
    public record struct CreateCouponDTO
    {
        public CreateCouponDTO(string couponCode, decimal discountAmount)
        {
            CouponCode=couponCode;
            DiscountAmount=discountAmount;
        }
        [Required(ErrorMessage = "Введите наименование купона.")]
        [StringLength(9, MinimumLength = 5, ErrorMessage = "Код купона должен содержать быть не менее 5 и не более 9 символов")]
        public string CouponCode { get; init; }
        [Required(ErrorMessage = "Введите процент скидки.")]
        [Range(0, 100, ErrorMessage = "Скидка не может быть меньше 0 и больше 100.")]
        public decimal DiscountAmount { get; init; }
    }
}
