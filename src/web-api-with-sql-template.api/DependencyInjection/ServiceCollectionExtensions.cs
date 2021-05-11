using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WebApiWithSqlTemplate.Domain.Handlers;

namespace WebApiWithSqlTemplate.Api.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web Api With SQL Template", Version = "v1" });
            });

            return services;
        }
        
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<GetTodoListHandler>();

            return services;
        }
    }
}