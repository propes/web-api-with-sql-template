using Microsoft.Extensions.DependencyInjection;
using WebApiWithSqlTemplate.Domain.Handlers;

namespace WebApiWithSqlTemplate.Api.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<GetTodoListHandler>();
            services.AddScoped<AddTodoItemHandler>();
            services.AddScoped<UpdateTodoItemHandler>();
            services.AddScoped<RemoveTodoItemHandler>();

            return services;
        }
    }
}