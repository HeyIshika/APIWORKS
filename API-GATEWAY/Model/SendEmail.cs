using MailKit.Net.Smtp; 
using MimeKit; 
using System; 
using WebApi.Dtos;
using ApiGateway.Controllers;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Configuration;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace gateway{

public class sendingmails{

    public void emailsent(string Emailid)
    {
  
         Console.WriteLine(Emailid);

         
               try 
            {    
                
                //From Address 
                string FromAddress = "cgiinsight@gmail.com"; 
                string FromAdressTitle = "Email from Insight"; 

                string strNewPassword = GeneratePassword().ToString();
                string ConfirmPassword = strNewPassword;
                //To Address 
                string ToAddress =Emailid; 
                string ToAdressTitle = "Microsoft ASP.NET Core"; 
                string Subject = "Hello World - Sending email using ASP.NET Core 1.1"; 
                string BodyContent = "Your New Password is: "+strNewPassword +". Change it as soon as possible for security reasons." ;


                 Console.WriteLine("Before Function");
                PostDataToAuth( Emailid ,strNewPassword ,ConfirmPassword);
                Console.WriteLine("After Function");
                
    
                //Smtp Server 
                string SmtpServer = "smtp.gmail.com"; 
                //Smtp Port Number 
                int SmtpPortNumber = 587; 
    
                var mimeMessage = new MimeMessage(); 
                mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress)); 
                mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress)); 
                mimeMessage.Subject = Subject; 
                mimeMessage.Body = new TextPart("plain") 
                { 
                    Text = BodyContent 
    
                }; 
                
    
                using (var client = new SmtpClient()) 
                { 
    
                    client.Connect(SmtpServer, SmtpPortNumber, false); 
                    // Note: only needed if the SMTP server requires authentication 
                    // Error 5.5.1 Authentication  
                    client.Authenticate("cgiinsight@gmail.com", "ishikapaul"); 
                    client.Send(mimeMessage); 
                    Console.WriteLine("The mail has been sent successfully !!"); 
                    Console.ReadLine(); 
                    client.Disconnect(true); 
    
                }
                
                     } 
            catch (Exception ex) 
            { 
                throw ex; 
            } 
} 
       
       public string GeneratePassword()
         {  
                      
            string PasswordLength = "15";  
            string NewPassword = "";  
            string allowedChars = "";  
            allowedChars = "1,2,3,4,5,6,7,8,9,0";  
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";  
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";  
  
            char[] sep = { ',' };  
            string[] arr = allowedChars.Split(sep);  
            string IDString = "";  
            string temp = "";  
            Random rand = new Random();  
            for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
             {  
                 temp = arr[rand.Next(0, arr.Length)];  
                 IDString += temp;  
                 NewPassword = IDString;  
  
             }  
            return NewPassword;  
        }

        public void PostDataToAuth(string Emailid,string strNewPassword , string ConfirmPassword)
        {
                Console.WriteLine(Emailid);
                Console.WriteLine(strNewPassword); 
                Console.WriteLine(ConfirmPassword);
                emaildto dto =new emaildto();
                dto.Emailid=Emailid;
                dto.password=strNewPassword;
                dto.Confirmpassword=ConfirmPassword;
                // let headers = new Headers({ 'Content-Type': 'application/json' });
                // let options = new RequestOptions({ headers: headers });
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));  
                AppConfig api=new AppConfig();          
                var result = client.PutAsync( api.serverurl + "api/updatepassword",new StringContent(JsonConvert.SerializeObject(dto),Encoding.UTF8,"application/json"));
         }


    }
}


