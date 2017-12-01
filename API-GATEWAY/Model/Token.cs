using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WebApi.GatewayTokenDtos;
using WebApi.Dtos;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Configuration;
using ApiGateway.Controllers;


namespace tokennamespace
{
public class GatewayTokenClass
    {
      
       public async Task<string> GatewayToken(GatewayTokenDto tokendto)
        {        
           //Token requesting method
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));            
            AppConfig api=new AppConfig();
            var stringTask = await client.PostAsync(api.tokenserverurl + "api/token/tokengenerator",new StringContent(JsonConvert.SerializeObject(tokendto),Encoding.UTF8,"application/json"));
            var msg=stringTask.Content.ReadAsStringAsync().Result;
            //returning token 
            return msg; 
        }
    }
}