using Aspros.Project.User.Application.Commands;
using Aspros.Project.User.Application.Queries;
using Aspros.Project.User.BootStrapping;
using Infrastructure.Consul.Core.Configuration;
using Infrastructure.Ioc.Core;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Aspros.Project.User.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonConsul(Program.ConfigurationRegister)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(UserGetQuery));
            services.AddMediatR(typeof(UserCreateCommand));
            services.AddSingleton(Configuration);
            //添加数据库连接
            services.Configure(
                Configuration["BasicSetting:ConnectionString"] + Configuration[$"{Program.ServiceName}:database"],
                Configuration["BasicSetting:RabbitMQ"]);

            services.AddMvc(options => { options.EnableEndpointRouting = false; })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm";
                });

            //验证授权地址
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Configuration["SP_BasicSetting:IssuerUri"];
                    options.RequireHttpsMetadata = false;
                    options.Audience = "org";
                });

            services.AddDistributedRedisCache(options =>
            {
                options.InstanceName = "";
                options.Configuration = Configuration["SP_BasicSetting:RedisServer"];
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            ServiceLocator.Instance = app.ApplicationServices;

            app.UseAuthentication();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            // swagger
            // app.UseSwagger(c => { c.RouteTemplate = "{documentName}/swagger.json"; });
            // app.UseSwaggerUI(s => { s.SwaggerEndpoint($"/{Program.ServiceName}/swagger.json", Program.ServiceName); });
        }
    }
}