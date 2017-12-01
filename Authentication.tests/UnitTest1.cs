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

namespace Authentication.tests
{
    public class UnitTest1
    {
        private static DbContextOptions<DataContext> CreateNewContextOptions()
       {
           // Create a fresh service provider, and therefore a fresh
           // InMemory database instance.
           var serviceProvider = new ServiceCollection()
               .AddEntityFrameworkInMemoryDatabase()
               .BuildServiceProvider();

           // Create a new options instance telling the context to use an
           // InMemory database and the new service provider.
           var builder = new DbContextOptionsBuilder<DataContext>();
           builder.UseInMemoryDatabase("testauthentication")
               .UseInternalServiceProvider(serviceProvider);

           return builder.Options;
       }
       
        
        [Fact]
        public void Test1()
        {
            //Arrange
              using (var context = new DataContext(CreateNewContextOptions()))
          {
            updatepasswordController obj = new updatepasswordController(context);
            emaildto obj1 = new emaildto()
            {
                Emailid = "abc@gmail.com",
                password = "abcd",
                Confirmpassword = "abcd"
            };

            //Act
            string checkencryptionstring="hiii how to encrypt me";
            string returnencryption = updatepasswordController.Encrypt(checkencryptionstring);
            Task<string> result = obj.Update(obj1);
    
            

            //Assert
            Assert.IsType<string>(returnencryption);
            Assert.IsType<Task<string>>(result);
        }
        }
    

       [Fact]
        public void Test2()
        {
            //Arrange
              using (var context = new DataContext(CreateNewContextOptions()))
          {
            UserService userServiceObjRef = new UserService(context);
            UserDetails ud = new UserDetails(){
            FirstName="sumanth",
            LastName="reddy",
            Username="Sumanth",
            Email="sumanth@gmail.com"

            };
            

            var user = userServiceObjRef.Create(ud,"password");
             Assert.IsType<UserDetails>(user);

              
            

            
          }
        }
        [Fact]
        public void Test3()
        {
            
    }
}
