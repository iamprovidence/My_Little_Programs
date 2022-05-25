using FunWithThreads.Application;

namespace FunWithThreads
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddHostedServices();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<JobService>();
            builder.Services.AddTransient<LogTimeJob>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }

    public static class ServicesExtensions
    {
        public static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            var addHostedServiceMathod = typeof(ServiceCollectionHostedServiceExtensions)
                .GetMethod(nameof(ServiceCollectionHostedServiceExtensions.AddHostedService), new[] { typeof(IServiceCollection) })!;

            foreach (var hostedServiceType in typeof(Program).Assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(IHostedService))))
            {
                addHostedServiceMathod.MakeGenericMethod(hostedServiceType).Invoke(null, new[] { services });
            }

            return services;
        }
    }
}