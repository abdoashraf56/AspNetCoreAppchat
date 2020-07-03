using System;
using AspNetCoreChatapp.Models;

namespace AspNetCoreChatapp.ViewModels
{
    public class ChatViewModel {
        public Guid ID {get; set;}
        public string User1ID {get; set;}
        public string User2ID {get; set;}
        public string Username1 {get; set;}
        public string Username2 {get; set;}
    }
}