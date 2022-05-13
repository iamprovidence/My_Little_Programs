using EventBus.RabbitMq;
using Microsoft.EntityFrameworkCore;
using TicketApi.Application;
using TicketApi.Infrastructure;
using TicketApi.WebHost.GrpcEndpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
services.AddGrpc();

if (builder.Environment.IsDevelopment())
{
    services.AddDbContextPool<TicketDbContext>(opt =>
    {
        opt.UseInMemoryDatabase(databaseName: "InMemDb");
    });
}
else
{
    services.AddDbContextPool<TicketDbContext>(opt =>
    {
        opt.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("TicketApiSqlServer"));
    });
}

services.AddScoped<TicketAppService>();
services.AddRabbitMqEventBus(builder.Configuration);

var app = builder.Build();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapGrpcService<TicketGrpcEndpoint>();
    endpoints.MapGet("protos/tickets", async context =>
    {
        await context.Response.WriteAsync(File.ReadAllText("WebHost/GrpcEndpoints/Tickets.proto"));
    });
});

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TicketDbContext>();

    if (app.Environment.IsProduction())
    {
        dbContext.Database.Migrate();
    }

    TicketDbSeed.Seed(dbContext);
}

app.Run();
