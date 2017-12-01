using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.GoogleEntities
{
    public class UserDetails
    {   
       [Key] 
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }  
        public string Username {get; set;}
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string PhotoUrl { get; set; }
        public string Provider { get; set; }
        public DateTime DOB { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDelete { get; set; }
        public DateTime ModifiedOn {get; set;}

    }
}