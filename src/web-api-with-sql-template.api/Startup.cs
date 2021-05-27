using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiWithSqlTemplate.Api.DependencyInjection;
using WebApiWithSqlTemplate.Api.Swagger;
using WebApiWithSqlTemplate.Domain.Contexts;

namespace WebApiWithSqlTemplate.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoListContext>(opt =>
            {
                if (_env.IsDevelopment())
                {
                    opt.UseSqlite(Configuration.GetConnectionString("TodoListContext"));
                }
                else
                {
                    opt.UseSqlServer(Configuration.GetConnectionString("TodoListContext"));
                }
            });
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllers();

            // TODO: add fluent validation

            services.AddSwagger();

            services.AddHandlers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}