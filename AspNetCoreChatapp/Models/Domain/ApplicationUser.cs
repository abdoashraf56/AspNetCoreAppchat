using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AspNetCoreChatapp.Models
{
    public class ApplicationUser : IdentityUser {

        public virtual ICollection<Chat> Chats {get; set;}
        
        public virtual ICollection<Message> Messages {get; set;}
    }
}