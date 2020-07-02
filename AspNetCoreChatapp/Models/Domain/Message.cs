using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreChatapp.Models
{
    public class Message
    {
        public Guid ID {get; set;}

        [Required]
        public Guid ChatID {get; set;}

        [Required]
        public string UserID {get; set;}

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateAt {get; set;}

        [Required]
        public string Text {get; set;}

        public virtual Chat Chat {get; set;}
        public virtual ApplicationUser User {get; set;}
    }
}