using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreChatapp.Models
{
    public class Chat
    {
        public Guid ID {get; set;}


        [Required]
        public string User1ID {get; set;} 

        [Required]
        public string User2ID {get; set;} 
        
        public virtual ApplicationUser User1 {get; set;} 
        public virtual ApplicationUser User2 {get; set;} 


        public virtual ICollection<Message> Messages {get; set;}
    }
}