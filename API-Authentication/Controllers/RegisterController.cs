using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Dtos;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Helpers;
using WebApi.HelpersException;
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

namespace WebApi.RegisterControllers
{
   [Route("api/[controller]")]
   public class RegisterController : Controller
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        public RegisterController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
             _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
       
        [HttpPost]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            Console.WriteLine("pic "+ userDto.photoUrl);
            //token from api gateway
           string authenticationTokenRegister=userDto.TokenString;
           AuthenticationTokenClass AuthenticationTokenClassObjRef = new AuthenticationTokenClass();
           TokenDto tokendto = new TokenDto();
           string tokenref = AuthenticationTokenClassObjRef.AuthenticationToken(tokendto).Result ;
           object jsonObject = JsonConvert.DeserializeObject(tokenref);
           var ParsedJsonObject= JObject.Parse(tokenref);
           string tokenserver= ParsedJsonObject["tokenstored"].ToString(); 
             dynamic dynJson = JsonConvert.DeserializeObject(tokenserver);
                foreach (var item in dynJson)
                {
                  String tokenstored=  item.tokenstored;
                 
                  if(authenticationTokenRegister == tokenstored)
                  {
           
                    try 
                    {
                        var user = _mapper.Map<UserDetails>(userDto);
                        var pswd = userDto.Password;
                        string encryptedpassword = Encrypt(pswd);
                        user.CreatedBy= userDto.FirstName;
                        user.PhotoUrl=userDto.photoUrl;
                        _userService.Create(user, encryptedpassword);
                        return Ok();
                    } 
                    catch(AppException ex)
                    {
                        return BadRequest(ex.Message);
                    }
                  }
                }
           return BadRequest();
        }
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "abc123";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }   

    }

}
