using Five9.Library.Data.Context;
using Five9.Services.Implementation;
using Five9.Services.Interfaces;
using Microsoft.OpenApi.Models;

namespace Five9.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<ApplicationLogger>>();

            // Add services to the container.
            services.AddLogging();
            services.AddSingleton(typeof(ILogger), logger);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ICallCenterEventService, CallCenterEventService>();
            
            // Db context
            services.AddDbContext<ApplicationDbContext>();

            // Cors
            services.AddCors();
            
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() {
                    Title = "CallCenterEventApi",
                    Version = "V1",
                    Contact = new OpenApiContact(){
                        Name = "Paulo Zuchini ",
                        Url = new Uri("https://www.linkedin.com/in/paulozuchini/"),
                        Email = "paulo.hilton@gmail.com"
                    }
                });
            });
            services.AddControllers();

            var app = builder.Build();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // global error handler
            // TODO
            //app.UseMiddleware<ErrorHandlerMiddleware>();

            app.MapControllers();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CallCenterEventApi V1");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}