using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using YallaNakol.Data.Models;

namespace YallaNakol.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly UserManager<ApplicationUser> userManager;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, 
                            ILogger<LogoutModel> logger,
                            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            this.userManager = userManager;
        }

        public void OnGet()
        {
        }
       
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var user = await userManager.GetUserAsync(this.User);
            await _signInManager.SignOutAsync();
            _logger.LogInformation($"User details that logged out: Email ={ user.Email}| Id ={user?.Id ?? "null"}.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
