using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using WebApi.AuthenticationControllers;
using WebApi.Dtos;
using WebApi.Services;
using WebApi.Entities;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
        
            BuildWebHost(args).Run();
            
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                 .UseUrls("http://172.23.238.152:5004/")
                .Build();
        
    }
}
