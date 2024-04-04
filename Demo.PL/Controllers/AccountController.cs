using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		//Register
		// BaseUrl/Account/Register
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					UserName = model.Email.Split('@')[0],
					Email = model.Email,
					FName = model.FName,
					LName = model.LName,
					IsAgree = model.IsAgree,
				};

				var Result = await _userManager.CreateAsync(user, model.Password);

				if (Result.Succeeded)
				{
					return RedirectToAction(nameof(Login));
				}
				else
				{
					foreach (var error in Result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}
			return View(model);
		}


		//LOGIN
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					var Result = await _userManager.CheckPasswordAsync(user, model.Password);

					if (Result)
					{
						//Login
						var LoginResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

						if (LoginResult.Succeeded)
						{
							return RedirectToAction("Index", "Home");
						}
					}
					else
					{
						ModelState.AddModelError(string.Empty, "Password is not Correct");
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Email is not Exists");
				}
			}
			return View(model);
		}

		//SIGN OUT
		public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
	}
}
