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
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;


namespace ParcingYamaha
{
    internal class Program
    {
        
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Parcing Yamaha");

            SampleContext context = new SampleContext();
            HttpClient httpClient = new HttpClient();
            Console.WriteLine("Press 1 for run parcing all models to sql or press Enter to skip");
            if (Console.ReadLine() == "1")
                { 
                ParcingSite parcing = new ParcingSite();
                await parcing.ParceModelsAsync(httpClient, context);
                }
            Console.WriteLine("Enter model name to parce parts or press Enter key to skip");
            string? input = Console.ReadLine();
            if (!input.IsNullOrEmpty())
            {
                Parts parts = new Parts();
                await parts.GetParts(httpClient, context, input);
            }

            httpClient.Dispose();
            //Parts chkParts = new Parts();
            //chkParts.CheckEqualParts(context, "XTZ690-U");


            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        

        }
    }
}