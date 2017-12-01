using Password.Models;
using WebApi.updatepasswordControllers;
using WebApi.Dtosupdate;
using System.Threading.Tasks;
using System;
using Xunit;
using WebApi.Entities;
using WebApi.Helpers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using  Webapi.changepasswordControllers;

namespace ChangePassword.Tests
{
    public class UnitTest1
    {

        private static DbContextOptions<updatepasswordContext> CreateNewContextOptions()
       {
           // Create a fresh service provider, and therefore a fresh
           // InMemory database instance.
           var serviceProvider = new ServiceCollection()
               .AddEntityFrameworkInMemoryDatabase()
               .BuildServiceProvider();

           // Create a new options instance telling the context to use an
           // InMemory database and the new service provider.
           var builder = new DbContextOptionsBuilder<updatepasswordContext>();
           builder.UseInMemoryDatabase("testupdatePassword")
               .UseInternalServiceProvider(serviceProvider);

           return builder.Options;
       }
        [Fact]
        public void Test1()
        {
            //Arrange
              using (var context = new updatepasswordContext(CreateNewContextOptions()))
          {
            changepasswordController obj = new changepasswordController(context);
            UpdatePassword obj1 = new UpdatePassword()
            {
                Email = "abc@gmail.com",
                PasswordSalt = "abcd",
                PasswordHash = "abcd"
            };

            //Act
            
                 var result = obj.GetAll();
    
            

            //Assert
           
                 Assert.IsType<Task<List<UpdatePassword>>>(result);
        }
        }
    
        
    }
}
