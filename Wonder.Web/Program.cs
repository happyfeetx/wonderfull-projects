using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Wonder.Web
{
    internal static class Program
    {
        private static AppSettings AppSettings { get; private set; }

        internal static async Task Main(string[] args)
        {
            try
            {
                await LoadConfigAsync();

                CreateHostBuilder(args).Build().Run();

                try
                {
                    await Task.Delay(Timeout.Infinite);
                } catch (TaskCanceledException)
                {

                }
            } catch (Exception e)
            {

            }

            Console.ReadKey();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task LoadConfigAsync(bool https)
        {
            Console.WriteLine("Loading app configuration...             ");

            string json = "{}";
            var utf = new UTF8Encoding(false);
            var fi = new FileInfo("appsettings.json");
            if (!fi.Exists)
            {
                json = JsonConvert.SerializeObject(new AppSettings(https));

                Console.WriteLine("Configuration file not found!            ");
                Console.WriteLine();

                try
                {
                    using (FileStream fs = fi.Create())
                    {
                        using (StreamWriter sw = new StreamWriter(fs, utf))
                        {
                            await sw.WriteAsync(json);
                            await sw.FlushAsync();
                        }
                    }
                } catch (Exception e)
                {
                    Console.WriteLine("An exception occurred!\n\n           ");

                    Console.WriteLine($"{e.GetType()} :\n{e.Message}        ");
                    Console.WriteLine($"{e.StackTrace}\n                    ");
                    if (!(e.InnerException is null))
                    {
                        Console.WriteLine($"{e.InnerException.GetType()} :\n{e.InnerException.Message}    ");
                        Console.WriteLine($"{e.InnerException.StackTrace}   ");
                    }
                }

                Console.ReadKey();

                throw new IOException("Configuration file not found!        ");
            }

            try
            {
                using (FileStream fs = fi.OpenRead())
                {
                    using (StreamReader sr = new StreamReader(fs, utf))
                    {
                        json = await sr.ReadToEndAsync();
                    }
                }

                AppSettings = JsonConvert.DeserializeObject<AppSettings>(json);
                
            } catch (Exception e)
            {
                Console.WriteLine("An exception occurred!\n\n               ");

                Console.WriteLine($"{e.GetType()} :\n{e.Message}            ");
                Console.WriteLine($"{e.StackTrace}\n                        ");
                if (!(e.InnerException is null))
                {
                    Console.WriteLine($"{e.InnerException.GetType()} :\n{e.InnerException.Message}");
                    Console.WriteLine($"{e.InnerException.StackTrace}       ");
                }
            }
        }
    }
}
