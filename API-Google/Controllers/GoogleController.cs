using System;
using WebApi.GoogleEntities;
using WebApi.Helpers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.GoogleDtonamespace;
using AutoMapper;
using dataservice;

namespace SocialEntities.controller
{
    [Route("api/[controller]")]
    public class GoogleController : Controller
    {
        private IDataService _userService;
        private IMapper _mapper;
       
        public  GoogleController(  IMapper mapper,IDataService userService)
        {
            _userService = userService;
            _mapper = mapper;
        }
            
        [HttpPost]
        public IActionResult FacebookPost([FromBody] GoogleDto googledto)
        {
            
             var user = _mapper.Map<UserDetails>(googledto);
             user.Username=googledto.Name;
             user.FirstName=googledto.Name;
             user.LastName=googledto.Name;
             user.PhotoUrl="dummy-dp";
             var fbuser= _userService.Create(user);
             return Ok(new {
                            Id = fbuser.UserId,
                            Username = fbuser.Username,
                            Email=fbuser.Email,
                            FirstName = fbuser.FirstName,
                            LastName = fbuser.LastName
                           
                        });
        }
    }
}