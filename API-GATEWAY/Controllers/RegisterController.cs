using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using Newtonsoft.Json;
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
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using tokennamespace;
using WebApi.GatewayTokenDtos;
using WebApi.HelpersException;
namespace ApiGateway.Controllers
{
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        
       [HttpPost]
       public async Task<string> RegisterUser(int id,[FromBody]UserDto userDto)
        {  
            
            if (userDto.secretregister=="secretcode")
            {
                Console.WriteLine("email"+userDto.photoUrl);
           //Requesting token from token server    
           GatewayTokenClass gatewayTokenClassObjRef = new GatewayTokenClass();
           GatewayTokenDto tokendtoref = new GatewayTokenDto();
           string tokenref = gatewayTokenClassObjRef.GatewayToken(tokendtoref).Result ;
           object jsonObject = JsonConvert.DeserializeObject(tokenref);
           var obj3= JObject.Parse(tokenref);
           string token= obj3["token"].ToString(); 
           //passing token from gateway to respective microservice
           userDto.TokenString = token;
           //Microservice request
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));  
            AppConfig api=new AppConfig();          
           var result = await client.PostAsync( api.serverurl + "api/register",new StringContent(JsonConvert.SerializeObject(userDto),Encoding.UTF8,"application/json"));
          

            var msg=result.Content.ReadAsStringAsync().Result;
            
            if(msg!="")
            {
              throw new AppException("Sorry!!Email has already registered with us ");
            }
        
            }
             return null;
          
        }
        
       
    }
}