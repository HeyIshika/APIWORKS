using System;
using WebApi.FacebookEntities;
using WebApi.Helpers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.FacebookDatatransfer;
using AutoMapper;
using dataservice;

namespace SocialEntities.controller
{
    [Route("api/[controller]")]
    public class FacebookapiController : Controller
    {
        private IDataService _userService;
        private IMapper _mapper;
       
        public  FacebookapiController(  IMapper mapper,IDataService userService)
        {
            _userService = userService;
            _mapper = mapper;
        }
            
        [HttpPost]
        public IActionResult FacebookPost([FromBody] FacebookDto facebookdto)
        {
            
             var user = _mapper.Map<UserDetails>(facebookdto);
             user.Username=facebookdto.Name;
             user.PhotoUrl="dummy-dp";
             var fbuser= _userService.Create(user);
             return Ok(new {
                            Id = fbuser.UserId,
                            Email = fbuser.Email,
                            Username= fbuser.Username,
                            FirstName = fbuser.FirstName,
                            LastName = fbuser.LastName
                        
                        });
        }
    }
}