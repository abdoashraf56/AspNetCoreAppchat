using AspNetCoreChatapp.Models;
using AspNetCoreChatapp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreChatapp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ///Get Current User
            var CurrentUser = await _userManager.GetUserAsync(User);

            var vm = new UsersViewModel();

            if (CurrentUser != null)
            {
                //Get All other Users than the current one
                var users = from user in _userManager.Users
                            where user.Id != CurrentUser.Id
                            select new UserViewModel
                            {
                                ID = user.Id,
                                Name = user.Email
                            };
                
                vm.Users = await users.ToListAsync();
            }
            return View(vm);
        }
    }
}