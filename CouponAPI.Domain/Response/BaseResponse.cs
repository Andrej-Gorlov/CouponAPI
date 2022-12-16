namespace CouponAPI.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public T? Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public Status Status { get; set; }
    }
    public interface IBaseResponse<T>
    {
        public T? Result { get; set; }
    }
    public enum Status
    {
        Ok,
        ExistsCode
    }
}
