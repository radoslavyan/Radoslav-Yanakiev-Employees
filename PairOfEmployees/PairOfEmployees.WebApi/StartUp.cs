using Microsoft.AspNetCore.Builder;
using PairOfEmployees.Application;
using PairOfEmployees.Persistence;

namespace PairOfEmployees.WebApi
{
    public class StartUp
    {
        private readonly IWebHostEnvironment _environment;
        public IConfigurationRoot Configuration { get; }

        public StartUp(IWebHostEnvironment env, IConfiguration configuration)
        {
            _environment = env;
            var configbuilder = new ConfigurationBuilder()
           .SetBasePath(env.ContentRootPath)
           .AddEnvironmentVariables();
            Configuration = configbuilder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddApplication(Configuration);
            services.AddPersistence(Configuration);

            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

            services.AddCors(c => c.AddPolicy("CorsPolicy", p =>
            p.WithOrigins("https://localhost:5193", "http://localhost:5194")
            .AllowAnyHeader()
            .AllowAnyMethod()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
          
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            }); ;
        }
    }
}