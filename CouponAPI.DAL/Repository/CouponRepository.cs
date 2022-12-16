using static System.Net.Mime.MediaTypeNames;

namespace CouponAPI.DAL.Repository
{
    public class CouponRepository : BaseRepository<Coupon>, ICouponRepository
    {
        private readonly ApplicationDbContext _db;
        public CouponRepository(ApplicationDbContext db) : base(db) => _db = db;

        public async Task<IEnumerable<Coupon>> GetAsync(Expression<Func<Coupon, bool>>? filter = null, string? search = null)
        {
            IQueryable<Coupon> coupons = _db.Coupons;
            if (filter != null)
            {
                WatchLogger.Log($"Применен фильтр: {filter.Body},Type: {filter.Type}.");
                coupons = coupons.Where(filter);
            }
            if (search != null)
            {
                WatchLogger.Log($"Применен поиск: {search}.");
                coupons = coupons.Where(x => EF.Functions.Like(x.CouponCode, $"%{search}%"));
            }
            WatchLogger.Log("Возвращение списка купонов.");
            return await coupons.ToListAsync();
        }

        public async Task<Coupon> GetByAsync(Expression<Func<Coupon, bool>> filter, bool tracking = true)
        {
            IQueryable<Coupon> coupons = _db.Coupons;
            if (!tracking)
            {
                WatchLogger.Log($"Применен AsNoTracking. Данные не помещены в кэш");
                coupons = coupons.AsNoTracking();
            }
            WatchLogger.Log($"Возвращен отфильтрованный список. Filter: {filter.Body},Type: {filter.Type}.");
            return await coupons.FirstOrDefaultAsync(filter);
        }
    }
}
