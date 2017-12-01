using System;
using Xunit;
using WebApi.Helpers;
using Token.Controllers;
using WebApi.TokenDtos;
using Microsoft.EntityFrameworkCore;
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
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using tokenstorage;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using WebApi.TokenReturn;
namespace API_Token.Tests
{
    public class UnitTest1
    {
        
        public DataContext _context;
        [Fact]
        public void Test1()
        {
            //Arrange
            TokenController TokenControllerObjRef = new TokenController(_context);
            TokenDto TokenDtoObjRef = new TokenDto();
            IActionResult result = TokenControllerObjRef.TokenRetreiver(TokenDtoObjRef);
            //Assert
            Assert.IsType<BadRequestResult>(result);
            
        }
    
    } 
}
