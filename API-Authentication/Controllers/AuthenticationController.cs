using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Dtos;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebApi.Entities;
using Microsoft.AspNetCore.Authorization;
using WebApi.AuthenticationTokenDtos;
using Configuration;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using tokennamespace;
using System.IO;
using System.Security.Cryptography;


namespace WebApi.AuthenticationControllers
{
    
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    { 
        
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AuthenticationController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
       
       
        [HttpPost ]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
                //Token from api gateway
                string authenticationToken=userDto.TokenString;
                AuthenticationTokenClass AuthenticationTokenClassObjRef = new AuthenticationTokenClass();
                TokenDto tokendto = new TokenDto();
                string tokenref = AuthenticationTokenClassObjRef.AuthenticationToken(tokendto).Result ;
                object jsonObject = JsonConvert.DeserializeObject(tokenref);
                var ParsedJsonObject= JObject.Parse(tokenref);
                string tokenserver= ParsedJsonObject["tokenstored"].ToString(); 
                dynamic dynJson = JsonConvert.DeserializeObject(tokenserver);
               //checking whether token is avaibale in tokenserver
                foreach (var item in dynJson)
                {
                  String str =  item.tokenstored;
                  if(authenticationToken == str)
                  {
                        var user = _userService.Authenticate(userDto.Email,userDto.Password);
                        if (user == null)
                        return Unauthorized();
                        // return basic user info (without password) and token to store client side
                        return Ok(new {
                            Id = user.UserId,
                            Username = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Token = authenticationToken
                        });
                  }
                }
               
                return StatusCode(500);    
       
        }    

    }
}
