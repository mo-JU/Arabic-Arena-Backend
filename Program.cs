using Arabic_Arena.Config;
using Arabic_Arena.Services;

namespace Arabic_Arena
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
            builder.Services.AddSingleton(mongoDbSettings);
            builder.Services.AddSingleton<MongoDbContext>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(policy =>
                policy.WithOrigins("https://arabicarena.netlify.app") // Replace with your React app's URL
                      .AllowAnyHeader()
                      .AllowAnyMethod());
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}