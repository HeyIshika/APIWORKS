using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using tokennamespace;
using WebApi.GatewayTokenDtos;
using WebApi.FacebookDto;




namespace ApiGateway.Controllers
{
    [Route("api/[controller]")]
    public class FacebookController : Controller
    {
       [HttpPost]
       public async Task<string> ValidateUser([FromBody]FacebookDto facebookDto)
        {  
        
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
                 client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));            
            try
            {
            AppConfig api=new AppConfig();
            var stringTask=await client.PostAsync(api.facebookurl+"api/Facebookapi",new StringContent(JsonConvert.SerializeObject(facebookDto),Encoding.UTF8,"application/json"));
            var msg=stringTask.Content.ReadAsStringAsync().Result;
            return msg;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        
        return null;

        }
    }
}