using System; 
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using WebApi.Dtos;
using gateway;
using System.Globalization;






namespace ApiGateway.Controllers{

    [Route("api/[controller]")]
    public class EmailController : Controller

    {
         [HttpPost]
        public void sendemail([FromBody]emaildto dto)
        {  
            string email =dto.Emailid.ToLower();
            Console.WriteLine("email is "+ email);
            var check=IsValidEmail(email);
            bool IsValidEmail(string emailid)
           {
            try {
                var addr = new System.Net.Mail.MailAddress(emailid);
                return addr.Address == emailid;
            
                }
           catch {
                     return false;
                 }     
}
            if(check){
            sendingmails sendEmail=new sendingmails();
            sendEmail.emailsent(email);
            }
            
           
            
        }

    }

}