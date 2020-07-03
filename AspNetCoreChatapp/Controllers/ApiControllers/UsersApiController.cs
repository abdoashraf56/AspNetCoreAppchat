using System.Threading.Tasks;
using AspNetCoreChatapp.Models;
using AspNetCoreChatapp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq ;
using Microsoft.EntityFrameworkCore ;
using System.Security.Claims;
using System.Diagnostics;

namespace AspNetCoreChatapp.Controllers
{
    [Authorize]
    public class UserApiController : Controller
    {

        private UserManager<ApplicationUser> _userManager;

        public UserApiController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Route("api/users")]
        public async Task<UsersViewModel> GetUsers (ClaimsPrincipal user)
        {
            //Check if the call is from controller or not to determin the current user
            var CurrentUser = await _userManager.GetUserAsync(user);
            
            //if null try the actual call of api from url api/users
            if(CurrentUser == null){
                CurrentUser = await _userManager.GetUserAsync(User);
            }
            var vm = new UsersViewModel();

            if (CurrentUser != null)
            {
                //Get All other Users other than the current one
                var users = from u in _userManager.Users
                            where u.Id != CurrentUser.Id
                            select new UserViewModel
                            {
                                ID = u.Id,
                                Name = u.Email
                            };
                
                vm.Users = await users.ToListAsync();
            }
            return vm ;
        }
    }
}