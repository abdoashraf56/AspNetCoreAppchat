using System.Diagnostics;
using System.Threading.Tasks;
using AspNetCoreChatapp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreChatapp.Data ;
using System;
using System.Linq ;
using Microsoft.EntityFrameworkCore;
namespace AspNetCoreChatapp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ChatController(UserManager<ApplicationUser> userManager 
        , ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string OtherUserId)
        {
            var CurrentUser = await _userManager.GetUserAsync(User);

            if(CurrentUser != null && !String.IsNullOrEmpty(OtherUserId)){

                //Get chat with same ids of two users
                var ChatQuery = from c in _context.Chats 
                            where c.User1ID == CurrentUser.Id && c.User2ID == OtherUserId
                            select c ;
                var chat = await ChatQuery.FirstOrDefaultAsync();

                //if chat is not exits create new one 
                if(chat == null){
                    var c = new Chat{
                        ID = new Guid(),
                        User1ID = CurrentUser.Id,
                        User2ID = OtherUserId,
                    };
                    _context.Chats.Add(c);
                    Debug.WriteLine(c.ID);
                    await _context.SaveChangesAsync();
                }else{
                    Debug.WriteLine(chat.ID);
                }


            }
            return View();
        }

    
    }
}