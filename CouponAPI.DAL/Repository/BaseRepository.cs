namespace CouponAPI.DAL.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public BaseRepository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SeveAsync();
            WatchLogger.Log("Сущность создана.");
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await SeveAsync();
            WatchLogger.Log("Сущность удалена.");
        }

        public async Task<T> UpdateAsync(T entity, T carent)
        {
            _db.Entry(carent).CurrentValues.SetValues(entity);
            await SeveAsync();
            WatchLogger.Log("Сущность обновлена.");
            return entity;
        }
        /// <summary>
        /// Create to DB
        /// </summary>
        /// <returns></returns>
        private async Task SeveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
