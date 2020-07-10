using System.Diagnostics;
using System.Threading.Tasks;
using AspNetCoreChatapp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreChatapp.Data;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace AspNetCoreChatapp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private ChatApiController _chatApi;

        public ChatController(ChatApiController chatApi)
        {
            _chatApi = chatApi;
        }

        public async Task<IActionResult> Index(string OtherUserId)
        {
            if (!String.IsNullOrEmpty(OtherUserId))
            {
               
            }
            else
            {
                Debug.WriteLine("Error");
            }
            var vm = await _chatApi.GetChatWithOtherUser(User, OtherUserId);
            return View(vm);
        }

        public async Task<IActionResult> AllChat()
        {
            var vm = await _chatApi.GetChats(User);
            return View(vm);
        }

    }
}