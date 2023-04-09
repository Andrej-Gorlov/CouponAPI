namespace CouponAPI.Domain
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public T? Result { get; set; }
    }
    public interface IBaseResponse<T>
    {
        public T? Result { get; set; }
    }
}
