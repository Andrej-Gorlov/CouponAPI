namespace CouponAPI.Service.Interfaces
{
    public interface ICouponService : IBaseService<CouponDTO>
    {
        Task<IBaseResponse<PagedList<CouponDTO>>> GetServiceAsync(PagingQueryParameters paging, string? filter = null, string? search = null);
        Task<IBaseResponse<CouponDTO>> CreateServiceAsync(CreateCouponDTO createModel);
        Task<IBaseResponse<CouponDTO>> UpdateServiceAsync(UpdateCouponDTO updateModel);
    }   
}
