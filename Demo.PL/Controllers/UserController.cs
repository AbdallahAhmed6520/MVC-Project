using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var users = await _userManager.Users.Select(
                    U => new UserViewModel()
                    {
                        Id = U.Id,
                        FName = U.FName,
                        LName = U.LName,
                        Email = U.Email,
                        Roles = _userManager.GetRolesAsync(U).Result
                    }).ToListAsync();
                return View(users);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(SearchValue);
                var MappedUser = new UserViewModel()
                {
                    Id = user.Id,
                    FName = user.FName,
                    LName = user.LName,
                    Email = user.Email,
                    Roles = _userManager.GetRolesAsync(user).Result
                };
                return View(new List<UserViewModel> { MappedUser });
            }
        }

        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            var MappedUser = _mapper.Map<UserViewModel>(user);
            return View(ViewName, MappedUser);

        }
    }
}
