using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PRN221_MVC.Controllers {
    public class EmailController : Controller {
        private UserManager<User> userManager;
        public EmailController(UserManager<User> usrMgr) {
            userManager = usrMgr;
        }

        public async Task<IActionResult> ConfirmEmail(string token, string email) {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return View("Error");

            var result = await userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? "/Views/Client/Email/ConfirmEmail.cshtml" : "/Views/Client/Email/Error.cshtml");
        }

        public IActionResult Unconfirm() {
            return View("/Views/Client/Email/Unconfirm.cshtml");
        }
    }
}
