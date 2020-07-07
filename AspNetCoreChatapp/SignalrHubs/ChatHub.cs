using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AspNetCoreChatapp.Data;
using AspNetCoreChatapp.Models;
using AspNetCoreChatapp.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreChatapp.Hubs
{
    //This is hub to control real connection with two users
    //When send messages between them
    public class ChatHub : Hub
    {
        private ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string UserID, string OtherUserID, Guid ChatID, string Text)
        {
            //Create new Message and add to database 
            var message = new Message
            {
                ID = new Guid(),
                Text = Text,
                UserID = UserID,
                ChatID = ChatID,
                CreateAt = DateTime.Now
            };
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            //Create ViewModel 
            MessageViewModel vm = new MessageViewModel { ID = message.ID, 
            UserID = message.UserID, 
            Text = message.Text ,
            CreateAt = message.CreateAt
            };

            //Send Back message to the sender of message 
            await Clients.User(UserID).SendAsync("ReceiveMessage", vm, "1");

            //Send Back to the other user 
            await Clients.User(OtherUserID).SendAsync("ReceiveMessage", vm, "2");
        }
    }
}