using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ParcingYamaha.Dtos;
using ParcingYamaha.Networks;
using ParcingYamaha.Services;

namespace ParcingYamaha
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Parcing Yamaha");

            var serviceProvider = new ServiceCollection()
                .AddDbContext<MotoContext>(options => options.UseSqlServer("Data Source=DESKTOP-34RSAGO\\SQLEXPRESS;Initial Catalog=Yamaha;Trusted_Connection=True;TrustServerCertificate=true"))
                .AddSingleton<HttpClient>()
                .AddTransient<ParcingModels>()
                .AddTransient<IParcingParts, ParcingPartsService>()
                .AddTransient < NetworkService>()
                .AddScoped<ModelsDB>()
                .AddScoped<ChaptersDB>()
                .AddScoped<PartsDB>()
                .AddTransient<App>()
                .AddAutoMapper(typeof(AppMappingProfile))
            .BuildServiceProvider();

           await serviceProvider
           .GetService<App>().Start();
        }
        public class App
        {
            ParcingModels parcingModels;
            IParcingParts parcingParts;
            public App(ParcingModels parcingModels, IParcingParts parcingParts)
            {
                this.parcingModels = parcingModels;
                this.parcingParts = parcingParts;
            }

            public async Task Start()
            {

                Console.WriteLine("Press 1 for run parcing all models to sql or press Enter to skip");
                if (Console.ReadLine() == "1")
                {
  
                    await parcingModels.ParceModelsAsync();
                }
                Console.WriteLine("Enter model name to parce parts or press Enter key to skip");
                string? input = Console.ReadLine();
                if (!input.IsNullOrEmpty())
                {
                    await parcingParts.GetParts(input);
                }
                           

                Console.WriteLine("Press any key to close");
                Console.ReadKey();
            }

        }



        


    }
}