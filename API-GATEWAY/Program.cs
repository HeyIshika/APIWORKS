using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using tokennamespace;
using WebApi.GatewayTokenDtos;
using ApiGateway.Controllers;
using WebApi.Dtos;

namespace gateway
{
    public class Program
    {
       public static void Main(string[] args)
    {
        // GatewayTokenClass obj = new GatewayTokenClass();
        // GatewayTokenDto tokendto= new GatewayTokenDto();
        //  var str = obj.GatewayToken(tokendto).Result;

        // AuthenticationController obj2 = new AuthenticationController();
        // UserDto userDto = new UserDto();
        // var str2 = obj2.ValidateUser(userDto).Result;

        var host = new WebHostBuilder()
            // .UseKestrel()
            // .UseContentRoot(Directory.GetCurrentDirectory())
            // .UseIISIntegration()
            .UseStartup<Startup>()
            .UseUrls("http://localhost:5000/")
            .Build();
        

       host.Run();


       
        
    }

    }
}
