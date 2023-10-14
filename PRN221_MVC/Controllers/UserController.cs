using BAL.Helpers;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PRN221_MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace PRN221_MVC.Controllers {
    public class UserController : Controller {
        private UserManager<User> userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<User> signInManager;
        private readonly FRMDbContext _dbContext;

        public UserController(UserManager<User> userMgr, SignInManager<User> signinMgr, IPasswordHasher<User> passwordHasher, RoleManager<IdentityRole> roleMgr, FRMDbContext dbContext) {
            userManager = userMgr;
            signInManager = signinMgr;
            _dbContext = dbContext;
            roleManager = roleMgr;
        }

        public async Task<IActionResult> Logout() {
            await signInManager.SignOutAsync();
            // Remove session cookie of user login info

            HttpContext.Session.Remove("UserInfo.Session");
            HttpContext.Session.Remove("user");

            Response.Cookies.Delete("UserInfo.Session");
            ISession session = HttpContext.Session;
            session.Remove(".AdventureWorks.Session");
            session.Remove("UserInfo.Session");
            session.Remove("user");


            session.Clear();

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register() {
            var err = TempData["RegisterError"] as string;
            if (err != null) {
                List<IdentityError> errors = System.Text.Json.JsonSerializer.Deserialize<List<IdentityError>>(err);
                ViewData["error"] = errors;
            }
            return View("/Views/Client/User/Register.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel user) {
            if (ModelState.IsValid) {
                User appUser = new User {
                    Name = user.Name,
                    UserName = user.Username,
                    Email = user.Email,
                    //TwoFactorEnabled = true,
                    isDeleted = false
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);

                if (result.Succeeded) {
                    // Add role Customer to new User
                    //var userFind = await userManager.FindByNameAsync(user.Username);
                    var userFind = await userManager.FindByEmailAsync(appUser.Email);
                    if (userFind != null) {
                        await userManager.AddToRoleAsync(appUser, "Customer");
                        await userManager.AddClaimAsync(userFind, new Claim(ClaimTypes.Role, "Customer"));
                    }

                    var customerRole = await roleManager.FindByNameAsync("Customer");
                    await roleManager.AddClaimAsync(customerRole, new Claim(ClaimTypes.Email, appUser.Email));
                    await roleManager.AddClaimAsync(customerRole, new Claim(ClaimTypes.Role, "Customer"));

                    var token = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);

                    EmailHelper emailHelper = new EmailHelper();
                    bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);

                    if (emailResponse)
                        return RedirectToAction("Index", "Home");
                    else {
                        // log email failed 
                        return RedirectToAction("Unconfirm", "Email");
                    }
                }
                else {
                    // Input fields are not valid
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                    TempData["RegisterError"] = System.Text.Json.JsonSerializer.Serialize(result.Errors);
                    return RedirectToAction("Register", user);
                }
            }
            else {
                // Null fields
                // send back inputed value
                return RedirectToAction("Register");
            }
        }

        [AllowAnonymous]
        public IActionResult GoogleLogin() {
            string redirectUrl = Url.Action("GoogleResponse", "User");
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }
        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse() {
            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login));
            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            string name = info.Principal.FindFirst(ClaimTypes.Name)!.Value;
            string email = info.Principal.FindFirst(ClaimTypes.Email)!.Value;
            string[] userInfo = { name, email };
            // Set user info to session
            HttpContext.Session.SetString("_Name", name);
            HttpContext.Session.SetString("_Email", email);
            // login with Google if user is not existed -> create
            // redirect to userInfo View (return user info JSON)
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            //return View(userInfo);
            else {
                User user = new User {
                    Email = email,
                    Name = name,
                    UserName = email,
                    isDeleted = false
                };

                IdentityResult identResult = await userManager.CreateAsync(user);

                if (identResult.Succeeded) {

                    identResult = await userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded) {
                        await signInManager.SignInAsync(user, false);
                        // Add role customer
                        await userManager.AddToRoleAsync(user, "Customer");
                        using (var context = new FRMDbContext()) {
                            // save to db
                            context.SaveChanges();
                        }
                        //EmailHelper emailHelper = new EmailHelper();
                        //var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        //var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
                        //emailHelper.SendEmail(email, confirmationLink);
                        return RedirectToAction("Index", "Home");
                    }
                }
                // Access denied
                return View("/Views/Client/User/LoginClient.cshtml");
            }
        }

        [AllowAnonymous]
        public IActionResult Login() {
            return View("/Views/Client/User/LoginClient.cshtml");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel login) {
            if (ModelState.IsValid) {
                User appUser = await userManager.FindByEmailAsync(login.Email);
                if (appUser != null) {
                    await signInManager.SignOutAsync();
                    SignInResult result = await signInManager.PasswordSignInAsync(appUser, login.Password, false, true);

                    if (result.Succeeded) {
                        HttpContext.Session.SetString("_Name", appUser.Name);
                        HttpContext.Session.SetString("_Email", appUser.Email);
                        return RedirectToAction("Index", "Home");
                    }
                    else {
                        ModelState.AddModelError("", "Incorrect Email or Password.");
                    }

                    // Two Factor Authentication

                    if (result.RequiresTwoFactor) {
                        return RedirectToAction("LoginTwoStep", new { appUser.Email });
                    }

                    // Locked out: LockedEndTime Store in Coordinated Universal Time (UTC)
                    if (result.IsLockedOut) {
                        ModelState.AddModelError("", "Your account is locked out. Kindly wait for 10 minutes and try again");
                        TempData["LoginError"] = "Your account is locked out. Kindly wait for 10 minutes and try again";
                    }

                    // Email confirmation 
                    bool emailStatus = await userManager.IsEmailConfirmedAsync(appUser);
                    if (emailStatus == false) {
                        ModelState.AddModelError(nameof(login.Email), "Email is unconfirmed, please confirm it first");
                        TempData["LoginError"] = "Email is unconfirmed, please confirm it first";
                    }
                }
                else {
                    ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email");
                    ModelState.AddModelError("", "Login Failed: Invalid Email");
                    TempData["LoginError"] = "Login Failed: Invalid Email";
                }
            }
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        //public async Task<IActionResult> LoginTwoStep(string email, string returnUrl) {
        public async Task<IActionResult> LoginTwoStep(string email) {
            var user = await userManager.FindByEmailAsync(email);
            var token = await userManager.GenerateTwoFactorTokenAsync(user, "Email");

            HttpContext.Session.SetString("_Name", user.Name);
            HttpContext.Session.SetString("_Email", email);
            var userS = await _dbContext.Users.FirstOrDefaultAsync(x => x.Name == HttpContext.Session.GetString("_Name"));
            var userRole = await _dbContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == userS.Id);
            var userRoleName = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
            HttpContext.Session.SetString("UserRole", userRoleName.NormalizedName);
            EmailHelper emailHelper = new EmailHelper();
            emailHelper.SendEmailTwoFactorCode(user.Email, token);

            return View("/Views/Client/User/LoginTwoStep.cshtml");
        }

        [HttpPost]
        [AllowAnonymous]
        //public async Task<IActionResult> LoginTwoStep(TwoFactor twoFactor, string returnUrl) {
        public async Task<IActionResult> LoginTwoStep(TwoFactor twoFactor) {
            if (!ModelState.IsValid) {
                return View(twoFactor.TwoFactorCode);
            }
            var result = await signInManager.TwoFactorSignInAsync("Email", twoFactor.TwoFactorCode, false, false);
            // if code no valid return error
            // redirect to userInfo View (return user info JSON)
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            //return Redirect(returnUrl ?? "/");
            else {
                // Remove session cookie of user login info
                HttpContext.Session.Remove("UserInfo.Session");
                Response.Cookies.Delete("UserInfo.Session");
                ModelState.AddModelError("", "Invalid Login Attempt");
                return View("/Views/Client/User/LoginTwoStep.cshtml");
            }
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword() {
            return View("/Views/Client/User/ForgotPassword.cshtml");
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required] string email) {
            if (!ModelState.IsValid)
                return View(email);

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "User", new { token, email = user.Email }, Request.Scheme);

            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = emailHelper.SendEmailPasswordReset(user.Email, link);

            if (emailResponse)
                return RedirectToAction("ForgotPasswordConfirmation");
            else {
                // log email failed 
            }
            return View(email);
        }
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation() {
            return View("/Views/Client/User/ForgotPasswordConfirmation.cshtml");
        }
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email) {
            var model = new ResetPassword { Token = token, Email = email };
            return View("/Views/Client/User/ResetPassword.cshtml", model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword) {
            if (!ModelState.IsValid)
                return View("/Views/Client/User/ResetPassword.cshtml", resetPassword);

            var user = await userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                RedirectToAction("ResetPasswordConfirmation");

            var resetPassResult = await userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!resetPassResult.Succeeded) {
                foreach (var error in resetPassResult.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return RedirectToAction("ResetPassword");
            }

            return RedirectToAction("ResetPasswordConfirmation");
        }

        public IActionResult ResetPasswordConfirmation() {
            return View("/Views/Client/User/ResetPasswordConfirmation.cshtml");
        }


        public async Task<IActionResult> Detail() {
            string email = HttpContext.Session.GetString("_Email");
            if (email == null) {
                return RedirectToAction("Login", "User");
            }
            User user = await userManager.FindByEmailAsync(email);
            bool isInRoleCustomer = await userManager.IsInRoleAsync(user, "Customer");
            if (!isInRoleCustomer) {
                return View("/Views/Shared/Error401.cshtml", new ErrorViewModel { RequestId = "401" });
            }
            EditUserViewModel editUser = new EditUserViewModel {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                DoB = user.DoB,
                Gender = user.Gender
            };
            return View("/Views/Client/User/Detail.cshtml", editUser);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Edit() {
            string email = HttpContext.Session.GetString("_Email");
            if (email == null) {
                return RedirectToAction("Login", "User");
            }
            User user = await userManager.FindByEmailAsync(email);
            bool isInRoleCustomer = await userManager.IsInRoleAsync(user, "Customer");
            if (!isInRoleCustomer) {
                return View("/Views/Shared/Error401.cshtml", new ErrorViewModel { RequestId = "401" });
            }
            EditUserViewModel editUser = new EditUserViewModel {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                DoB = user.DoB,
                Gender = user.Gender
            };
            return View("/Views/Client/User/Edit.cshtml", editUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel editUser) {
            if (!ModelState.IsValid)
                return View("/Views/Client/User/Edit.cshtml", editUser);
            string email = HttpContext.Session.GetString("_Email");
            if (email == null) {
                return RedirectToAction("Login", "User");
            }
            User user = await userManager.FindByEmailAsync(email);
            bool isInRoleCustomer = await userManager.IsInRoleAsync(user, "Customer");
            if (!isInRoleCustomer) {
                return View("/Views/Shared/Error401.cshtml", new ErrorViewModel { RequestId = "401" });
            }
            else {
                user.Name = editUser.Name;
                user.Email = editUser.Email;
                user.UserName = editUser.Username;
                user.PhoneNumber = editUser.PhoneNumber;
                user.Address = editUser.Address;
                user.DoB = editUser.DoB;
                user.Gender = editUser.Gender;
                IdentityResult result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Detail", "User");
                return RedirectToAction("Edit", editUser);
            }
        }
    }
}
