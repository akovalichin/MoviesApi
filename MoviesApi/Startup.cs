using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Middleware;
using Serilog;
using MoviesApi.DbModels;
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
            var options = new DbContextOptionsBuilder<InMemoryContext>().UseInMemoryDatabase(databaseName: "in_memory").Options;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MoviesApi", Version = "v1" });
            });

            using (var context = new InMemoryContext(options))
            {
                // add service here
            }
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