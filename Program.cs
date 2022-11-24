﻿using System;
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
            Console.WriteLine("Parcing Yamaha");

            SampleContext context = new SampleContext();
            HttpClient httpClient = new HttpClient();
            Console.WriteLine("Press 1 for run parcing all models to sql");
            var line=Console.ReadLine();
            if (line=="1")
                { 
                ParcingSite parcing = new ParcingSite();
                await parcing.ParceModelsAsync(httpClient, context);
                
                }
            Parts parts = new Parts();
            await parts.GetParts(httpClient, context);





            httpClient.Dispose();

            Console.ReadKey();
        

        }
    }
}