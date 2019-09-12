using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesApi.DbModels;
using MoviesApi.Middleware;
using MoviesApi.Repos;
using MoviesApi.Services;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace MoviesApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MoviesApi", Version = "v1" });
            });

            services.AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<InMemoryContext>(options =>
                        options.UseInMemoryDatabase("in_memory"));

            services.AddTransient<IMoviesService, MoviesService>();
            services.AddTransient<IMoviesRepo, MoviesRepo>();
            services.AddTransient<IRatingService, RatingService>();
            services.AddSingleton<IRoundingService, RoundingService>();
            Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(Configuration)
                    .CreateLogger();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpStatusCodeExceptionMiddleware();
            }
            else
            {
                app.UseExceptionHandler();
                app.UseHttpStatusCodeExceptionMiddleware();
                app.UseHsts();
            }
            app.UseSerilogRequestLogging();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MoviesApi");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}