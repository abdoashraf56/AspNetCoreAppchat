using AspNetCoreChatapp.Models;
using AspNetCoreChatapp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreChatapp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private UserApiController _userApi;

        public UsersController(UserApiController userApi)
        {
            _userApi = userApi;
        }

        public async Task<IActionResult> Index()
        {
            var vm = await _userApi.GetUsers(User);
            return View(vm);
        }

    }
}