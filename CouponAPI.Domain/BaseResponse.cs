namespace CouponAPI.Domain
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T? Result { get; set; }
        public string Error { get; set; } = string.Empty;

        public ResponseStatus ResponseStatus  { get; set; }
        public BaseResponse<T> Success(T value, ResponseStatus responseStatus) =>
            new BaseResponse<T> { IsSuccess = true, Result = value , ResponseStatus = responseStatus };
        public BaseResponse<T> Failure(string error) => 
            new BaseResponse<T> { IsSuccess = false, Error = error, ResponseStatus = ResponseStatus.Error};
    }
    public interface IBaseResponse<T>
    {
        bool IsSuccess { get; set; }
        public T? Result { get; set; }
        string Error { get; set; }
        BaseResponse<T> Success(T value, ResponseStatus responseStatus);
        BaseResponse<T> Failure(string error);
        ResponseStatus ResponseStatus { get; set; }
    }
}
public enum ResponseStatus
{
    Ok,
    Created,
    Error
}
