using SqlDistributedCache.Demo.DataAccess.Entities;
using SqlDistributedCache.Demo.Endpoints;
using SqlDistributedCache.Demo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICacheService, CacheService>();

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("SqlDistributedCacheAppContext");
    options.SchemaName = "dbo";
    options.TableName = nameof(CachedItems);
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapSqlDistributedCacheEndpoints();

app.Run();