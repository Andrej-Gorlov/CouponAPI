namespace CouponAPI.Service.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<IBaseResponse<T>> GetByIdServiceAsync(int model);
        Task<IBaseResponse<bool>> DeleteServiceAsync(int id);
    }
}
