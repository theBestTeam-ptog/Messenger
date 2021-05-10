using ChatService.Services;
using Core.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataAccess.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Domain.Repositories;


namespace ChatService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #if DEBUG
                services.Configure<DataBaseSettings>(
                    Configuration.GetSection(nameof(DataBaseSettings)));
            #else
                services.Configure<DataBaseSettings>(
                    Configuration.GetSection("ReleaseSettings"));
            #endif

            services.AddSingleton<IDataBaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DataBaseSettings>>().Value);

            services.AddSingleton<Repository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IChatRepository, ChatRepository>();

            services.AddGrpc();
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddGrpcReflection();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                if (env.IsDevelopment())
                {
                    endpoints.MapGrpcReflectionService();
                }

                endpoints.MapGrpcService<GreeterService>();

                endpoints.MapGet("/",
                    async context =>
                    {
                        await context.Response.WriteAsync(
                            "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                    });
            });
        }
    }
}