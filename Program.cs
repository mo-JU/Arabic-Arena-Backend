using Arabic_Arena.Config;
using Arabic_Arena.Services;
using MongoDB.Driver;

namespace Arabic_Arena
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("arabicarena",
                    builder => builder
                        .WithOrigins("https://arabicarena.netlify.app", "https://arabicarenaadmin.netlify.app")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
            var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
            builder.Services.AddSingleton(mongoDbSettings);
            builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("ConnectionString");
                return new MongoClient(connectionString);
            });
            builder.Services.AddSingleton<MongoDbContext>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("arabicarena");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}