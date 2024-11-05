using Microsoft.Extensions.Caching.Distributed;
using SqlDistributedCache.Demo.Models;
using System.Text.Json;

namespace SqlDistributedCache.Demo.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task<BaseResponse<List<Person>>> GetCacheAsync(string cache)
        {
            if (string.IsNullOrWhiteSpace(cache))
                return new BaseResponse<List<Person>>(null)
                {
                    Message = "Cache not found.",
                    StatusCode = BaseResponseStatusCode.NotFound
                };

            var response = await distributedCache.GetStringAsync(cache);
            var deserializedResponse = JsonSerializer.Deserialize<List<Person>>(response) ?? new List<Person>();

            if (string.IsNullOrWhiteSpace(response))
                return new BaseResponse<List<Person>>(null)
                {
                    Message = "Cache not found.",
                    StatusCode = BaseResponseStatusCode.NotFound
                };

            return new BaseResponse<List<Person>>(deserializedResponse)
            {
                Message = "Cache has been retrieved successfully.",
                StatusCode = BaseResponseStatusCode.Success
            };
        }

        public async Task<BaseResponse> SetCacheAsync(string cache)
        {
            if (string.IsNullOrWhiteSpace(cache))
                return new BaseResponse
                {
                    Message = "You must provide cache name",
                    StatusCode = BaseResponseStatusCode.BadRequest
                };

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.UtcNow.AddDays(2)
            };

            var personsData = GetPersonsSeedData();
            var serializedData = JsonSerializer.Serialize(personsData);

            await distributedCache.SetStringAsync(cache, serializedData, cacheOptions);

            return new BaseResponse
            {
                Message = "Cache added successfully.",
                StatusCode = BaseResponseStatusCode.Success
            };
        }

        public async Task<BaseResponse> DeleteCacheAsync(string cache)
        {
            if (string.IsNullOrWhiteSpace(cache))
                return new BaseResponse
                {
                    Message = "This cache does not exists!",
                    StatusCode = BaseResponseStatusCode.NotFound
                };

            await distributedCache.RemoveAsync(cache);

            return new BaseResponse
            {
                Message = "Cache deleted successfully.",
                StatusCode = BaseResponseStatusCode.Success
            };
        }

        private List<Person> GetPersonsSeedData()
        {
            var persons = new List<Person>()
            {
                new Person
                {
                    Name = "Person 1",
                    Age = 28,
                    Job = "Software Developer"
                },
                new Person
                {
                    Name = "Person 2",
                    Age = 27,
                    Job = "HR"
                },
                new Person
                {
                    Name = "Person 3",
                    Age = 30,
                    Job = "Sales"
                }
            };

            return persons;
        }
    }
}