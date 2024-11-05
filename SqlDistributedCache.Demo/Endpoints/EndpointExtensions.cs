using Microsoft.AspNetCore.Mvc;
using SqlDistributedCache.Demo.Models;
using SqlDistributedCache.Demo.Models.Constants;
using SqlDistributedCache.Demo.Services;

namespace SqlDistributedCache.Demo.Endpoints
{
    public static class EndpointExtensions
    {
        public static IEndpointRouteBuilder MapSqlDistributedCacheEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/cache/get", HandleGetCache)
                .WithTags(EndpointTag.CacheApis);

            app.MapPost("api/cache/set", HandleSetCache)
                .WithTags(EndpointTag.CacheApis);

            app.MapDelete("api/cache/delete", HandleDeleteCache)
                .WithTags(EndpointTag.CacheApis);

            return app;
        }

        private static async Task<BaseResponse<List<Person>>> HandleGetCache(string cacheName, [FromServices] ICacheService cacheService)
        {
            var resposne = await cacheService.GetCacheAsync(cacheName);

            return resposne;
        }

        private static async Task<BaseResponse> HandleSetCache(string cacheName, [FromServices] ICacheService cacheService)
        {
            var resposne = await cacheService.SetCacheAsync(cacheName);

            return resposne;
        }

        private static async Task<BaseResponse> HandleDeleteCache(string cacheName, [FromServices] ICacheService cacheService)
        {
            var resposne = await cacheService.DeleteCacheAsync(cacheName);

            return resposne;
        }
    }
}