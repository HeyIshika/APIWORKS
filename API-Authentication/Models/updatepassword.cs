using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace Password.Models
{
    public class UpdatePassword
    {
        [Key]
       
        public string Email{get;set;}
        public string PasswordSalt{ get; set; }
        public string PasswordHash{ get;set;}

        
        
    }
}