using System.Threading.Tasks;
using AspNetCoreChatapp.Data;
using AspNetCoreChatapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;
using AspNetCoreChatapp.ViewModels;

namespace AspNetCoreChatapp.Controllers
{
    public class ChatApiController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ChatApiController(UserManager<ApplicationUser> userManager
        , ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("api/chat")]
        //Get user's chats
        public async Task<IEnumerable<ChatViewModel>> GetChats(ClaimsPrincipal user)
        {
            //Check if the call is from controller or not to determin the current user
            var CurrentUser = await _userManager.GetUserAsync(user);

            //if null try the actual call of api from url api/users
            if (CurrentUser == null)
            {
                CurrentUser = await _userManager.GetUserAsync(User);
            }

            //Get user's chats
            var ChatQuery = from c in _context.Chats
                            where c.User1ID == CurrentUser.Id
                            select new ChatViewModel
                            {
                                ID = c.ID,
                                User1ID = c.User1ID,
                                User2ID = c.User2ID,
                                Username1 = c.User1.Email,
                                Username2 = c.User2.Email
                            };
            var chats = await ChatQuery.ToArrayAsync();
            return chats;
        }

        [Route("api/chat/{OtherUserId}")]
        //Get user chat with other chat
        public async Task<ChatViewModel> GetChatWithOtherUser(ClaimsPrincipal user, string OtherUserId)
        {
            //Check if the call is from controller or not to determin the current user
            var CurrentUser = await _userManager.GetUserAsync(user);

            //if null try the actual call of api from url api/users
            if (CurrentUser == null)
            {
                CurrentUser = await _userManager.GetUserAsync(User);
            }

            if (CurrentUser != null && !String.IsNullOrEmpty(OtherUserId))
            {

                //Get chat with same ids of two users
                var ChatQuery = from c in _context.Chats
                                where c.User1ID == CurrentUser.Id && c.User2ID == OtherUserId
                                select new ChatViewModel
                                {
                                    ID = c.ID,
                                    User1ID = c.User1ID,
                                    User2ID = c.User2ID,
                                    Username1 = c.User1.Email,
                                    Username2 = c.User2.Email
                                };

                var chat = await ChatQuery.FirstOrDefaultAsync();

                //if chat is not exits create new one 
                if (chat == null)
                {
                    var ChatDatabaseObject = new Chat
                    {
                        ID = new Guid(),
                        User1ID = CurrentUser.Id,
                        User2ID = OtherUserId,
                    };
                    _context.Chats.Add(ChatDatabaseObject);
                    await _context.SaveChangesAsync();
                    chat = await ChatQuery.FirstOrDefaultAsync();
                }
                return chat;
            }
            else
            {
                return null;
            }
        }
    }
}