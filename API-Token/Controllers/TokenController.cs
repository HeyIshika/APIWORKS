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
using WebApi.Helpers;
using WebApi.TokenDtos;
using tokenstorage;

namespace Token.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private DataContext _context;
        public TokenController(DataContext context)
        {
            _context = context;    
        }
          // POST api/values
        [HttpPost("tokengenerator")]
        public IActionResult TokenPost([FromBody] TokenDto tokendto)
        {
           
           int Id= 123;
           string secret= "REPLACE THIS WITH YOUR OWN SECRET, IT CAN BE ANY STRING";
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            Console.WriteLine("token"+tokenString);
            tokenstorageservice tss = new tokenstorageservice(_context);
            var user =tss.tokenstore(tokendto,tokenString);
            Console.WriteLine("passes");
            return Ok(new {
                Id = tokendto.Id,
                Token = tokenString
               
            });
           
        }
        
        [HttpPost("tokenretreiver")]
        public IActionResult TokenRetreiver([FromBody] TokenDto tokendto)
        {  
          
          var token = _context.TokenDto;
          Console.WriteLine("passes2");
            return Ok(new {
                 Id = tokendto.Id,
                tokenstored = token
                
            });
        }
        
       
    }
}
