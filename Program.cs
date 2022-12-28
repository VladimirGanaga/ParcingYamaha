using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.X86;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ParcingYamaha.Networks;

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
                .AddTransient<IParcingParts, ParcingParts1>()
                .AddTransient < NetworkService>()
                .AddTransient<App>()
            .BuildServiceProvider();

           await serviceProvider
           .GetService<App>().Start();


        }



        public class App
        {
            //MotoContext motoContext;
            //HttpClient httpClient;

            //public App(MotoContext motoContext, HttpClient httpClient)
            //{
            //    this.motoContext = motoContext;
            //    this.httpClient = httpClient;
            //}

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

                //test.ffoo();
                //httpClient.Dispose();

                Console.WriteLine("Press any key to close");
                Console.ReadKey();
            }

        }



        public static class test
        {
            public static void ffoo()
            { 
            
            
            }
        }


    }
}