namespace SqlDistributedCache.Demo.Models
{
    public class BaseResponse
    {
        public string Message { get; set; } = string.Empty;
        public BaseResponseStatusCode StatusCode { get; set; } = BaseResponseStatusCode.Success;
    }

    public class BaseResponse<TData> : BaseResponse
    {
        public TData Data { get; set; }

        public BaseResponse(TData data)
        {
            this.Data = data;
        }
    }

    public enum BaseResponseStatusCode
    {
        Success = 200,
        InternalServer = 500,
        BadRequest = 400,
        Conflict = 409,
        NotFound = 404
    }
}