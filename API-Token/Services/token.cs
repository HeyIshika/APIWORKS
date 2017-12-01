using System;
using  WebApi.TokenDtos;
using WebApi.Helpers;
using System.Linq;

namespace tokenstorage
{
    public class tokenstorageservice 
    {
        public DataContext _context;
 
        public tokenstorageservice(DataContext context)
        {
         
            _context = context;
        
        }
 
        public TokenDto tokenstore(TokenDto tokendto,string token)
        {
          
          tokendto.tokenstored=token;
          _context.TokenDto.Add(tokendto); 
          _context.SaveChanges();
          return tokendto;
        }
       
    }
}