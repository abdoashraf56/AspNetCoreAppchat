using System;

namespace AspNetCoreChatapp.ViewModels
{
    public class MessageViewModel {
        public Guid ID {get; set;}
        public string UserID {get; set;}
        public string Text {get; set;}
        public DateTime CreateAt {get; set;}
    }
}