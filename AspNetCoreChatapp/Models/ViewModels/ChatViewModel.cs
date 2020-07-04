using System;
using System.Collections.Generic;
using AspNetCoreChatapp.Models;

namespace AspNetCoreChatapp.ViewModels
{
    public class ChatViewModel {
        public Guid ID {get; set;}
        public string User1ID {get; set;}
        public string User2ID {get; set;}
        public string Username1 {get; set;}
        public string Username2 {get; set;}
        public IEnumerable<MessageViewModel> Messages {get; set;}
    }
}