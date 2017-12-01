using System;
using WebApi.GoogleEntities;
using WebApi.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace dataservice
{
    public interface IDataService
    {
        UserDetails Create(UserDetails user);
       
    }
    public class DataServiceClass : IDataService
    {
         private GoogleDataContext _context;

        public DataServiceClass(GoogleDataContext context)
        {
            _context = context;
        }
         public UserDetails Create(UserDetails user)
        {
           var str =_context.UserDetails.SingleOrDefault(x => x.Email == user.Email );
           if(str!=null)
           {
               return str;
           }
           else
           {
             _context.UserDetails.Add(user);          
            _context.SaveChanges();
             return user;
           }
        }
    }
}

