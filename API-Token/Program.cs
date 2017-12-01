using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Token.Controllers;
using WebApi.TokenDtos;
using WebApi.Helpers;
namespace API_Token
{
    public class Program
    {
        public static void Main(string[] args)
        {
         
            var host = new WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseIISIntegration()
            .UseStartup<Startup>()
            // .UseUrls("http://localhost:5003")
            .UseUrls("http://172.23.238.152:5003")
            .Build();

             host.Run();
        }

        
    }
}
