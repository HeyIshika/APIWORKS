using System; 
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using WebApi.Dtosupdate;
using WebApi.Services;
using AutoMapper;
using Password.Models;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.HelpersException;
using System.Security.Cryptography;

namespace WebApi.updatepasswordControllers
{
    [Route("api/[controller]")]
   public class updatepasswordController : Controller{

       private readonly DataContext _context;

       
         public updatepasswordController (DataContext context)
              {
                  _context=context;
              }
         // Password sent to the email updating the database .
       [HttpPut]
        public async Task<string> Update([FromBody] emaildto dto)
                {
                      if (_context.UserDetails.Any(x => x.Email == dto.Emailid)){

                            var user = _context.UserDetails.FirstOrDefault(t => t.Email ==dto.Emailid);
                            user.Email=dto.Emailid;
                            string pswd = Encrypt(dto.password);
                            user.PasswordHash=pswd;
                            user.PasswordSalt=pswd;
                            try{
                                
                                _context.UserDetails.Update(user);
                                 await _context.SaveChangesAsync();
                                   }
                                catch(Exception ex){
                                throw new Exception(ex.Message);
                                }
                      }
                      else
                      {
                          throw new Exception("Email doesn't exist");


                          }
                          return dto.Emailid;
                      }
           //Used for the Encryption of passoword
             public static string Encrypt(string clearText)
                 {  
                     string EncryptionKey = "abc123";
                     byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                     using (Aes encryptor = Aes.Create())
                        {
                          Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                          encryptor.Key = pdb.GetBytes(32);
                          encryptor.IV = pdb.GetBytes(16);
                          using (MemoryStream ms = new MemoryStream())
                                {
                                     using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                                        {
                                          cs.Write(clearBytes, 0, clearBytes.Length);
                                          cs.Close();
                                        }
                                     clearText = Convert.ToBase64String(ms.ToArray());
                                }
                        } 
                          return clearText;
                }  

                     
                    
                   
              

        }
        
   }

