using SqlDistributedCache.Demo.Models;

namespace SqlDistributedCache.Demo.Services
{
    public interface ICacheService
    {
        Task<BaseResponse<List<Person>>> GetCacheAsync(string cache);
        Task<BaseResponse> SetCacheAsync(string cache);
        Task<BaseResponse> DeleteCacheAsync(string cache);
    }
}