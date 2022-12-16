using System.Linq.Expressions;

namespace CouponAPI.DAL.Interfaces
{
    public interface ICouponRepository : IBaseRepository<Coupon>
    {
        Task<IEnumerable<Coupon>> GetAsync(Expression<Func<Coupon, bool>>? filter = null, string? search = null);
        Task<Coupon> GetByAsync(Expression<Func<Coupon, bool>> filter, bool tracking = true);
    }
}
