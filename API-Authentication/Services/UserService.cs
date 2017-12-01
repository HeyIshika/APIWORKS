using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.HelpersException;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using WebApi.Helpers;


namespace WebApi.Services
{
   public interface IUserService
    {
        UserDetails Authenticate(string username, string password);
      
      
        UserDetails Create(UserDetails user, string password);
       
    }

    public class UserService : IUserService
    {

        public int UserID;
        private DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        
        
        public UserDetails Authenticate(string email, string password)
        {
            var pswd = password;
            string encryptedpassword = Encrypt(pswd);
            var user = _context.UserDetails.SingleOrDefault(x => x.Email == email && x.PasswordHash== encryptedpassword);
           
            if (user == null)
             return null;

            return user;
        }
    //      public static string Decrypt(string cipherText)
    // {
    //     string EncryptionKey = "abc123";
    //     cipherText = cipherText.Replace(" ", "+");
    //     byte[] cipherBytes = Convert.FromBase64String(cipherText);
    //     using (Aes encryptor = Aes.Create())
    //     {
    //         Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
    //         encryptor.Key = pdb.GetBytes(32);
    //         encryptor.IV = pdb.GetBytes(16);
    //         using (MemoryStream ms = new MemoryStream())
    //         {
    //             using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
    //             {
    //                 cs.Write(cipherBytes, 0, cipherBytes.Length);
    //                 cs.Close();
    //             }
    //             cipherText = Encoding.Unicode.GetString(ms.ToArray());
    //         }
    //     }
    //     return cipherText;
    // }

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

       
        
        public UserDetails Create(UserDetails user, string password)
        {
            if (_context.UserDetails.Any(x => x.Email == user.Email))
            {

                    throw new AppException("Email " + user.Email + " is already taken");
            }
            
            user.PasswordHash = password;
            user.PasswordSalt = password;
            
            _context.UserDetails.Add(user);         
            _context.SaveChanges();
            int ID = user.UserId;
            Func(ID);
            return user; 
        }

        public void Func(int id)
        {
            UserID = id;
            Console.WriteLine(UserID);
        }

     
        
    }
}