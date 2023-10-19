using AccountServer.DB;
using AccountServer.Utils;
using Microsoft.EntityFrameworkCore;

namespace AccountServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfigurationRoot configuration = new ConfigurationBuilder()
                                                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                .AddJsonFile("config.json")
                                                .Build();

            string connectionString = configuration.GetConnectionString("MyConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySQL(connectionString));

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddMvc().AddRazorRuntimeCompilation();

            builder.Services.AddSingleton<PasswordEncryptor>();

            WebApplication app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {

                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}