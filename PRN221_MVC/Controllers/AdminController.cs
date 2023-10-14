using DAL;
using DAL.Entities;
using DAL.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN221_MVC.Models;
using System.Security.Cryptography;
using System.Text;

namespace PRN221_MVC.Controllers {
    //[Authorize(Roles = "Administrator")]
    public class AdminController : Controller {
        private readonly IUserService _userService;
        private UserManager<User> _userManager;
        private SignInManager<User> signInManager;

        FRMDbContext context = new FRMDbContext();
        public AdminController(IUserService userService, UserManager<User> userManager, SignInManager<User> signInManager) {
            _userService = userService;
            _userManager = userManager;
            this.signInManager = signInManager;
        }
        // GET: AdminController
        public ActionResult Index() {
            if (HttpContext.Session.GetString("user") == null) {
                return RedirectToAction("Login");
            }
            else {
                return View();
            }
        }

        public ActionResult Login() {
            return View();
        }

        public List<User> users { get; set; }

        // GET: AdminController/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create() {
            return View();
        }

        public ActionResult ErrAdd() {
            ViewBag.CheckErr = "Email is already existed";
            return View("Create");
        }
        public async Task<ActionResult> UserList() {
            List<User> users = await _userService.GetAll();
            return View(model: users);
        }
        public ActionResult Users() {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
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

            return RedirectToAction("Login");
        }
        [HttpPost]
        public async Task<ActionResult> AsyncLogin(LoginUserViewModel login) {
            if (ModelState.IsValid) {
                try {
                    User appUser = await _userManager.FindByEmailAsync(login.Email);
                    var roleStore = new RoleStore<IdentityRole>(context);
                    var roles = roleStore.Roles.ToList();


                    if (appUser != null) {
                        var role = await _userManager.GetRolesAsync(appUser);

                        var matchingRole = roles.FirstOrDefault(r => role.Contains(r.Name)).Name;

                        await signInManager.SignOutAsync();
                        Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, login.Password, false, false);

                        if (result.Succeeded) {
                            if (matchingRole != null && matchingRole.Equals("Administrator")) {
                                //ViewBag.User = appUser;
                                HttpContext.Session.SetString("user", appUser.Id.ToString());
                                return RedirectToAction("AdminProfile");
                            }

                            if (matchingRole != null && matchingRole.Equals("ShopOwner")) {
                                HttpContext.Session.SetString("Email", appUser.Email);
                                return RedirectToAction("IndexShopOwner", "ShopOwner");
                            }
                        }

                        //return Redirect(login.ReturnUrl ?? "/");

                        // Two Factor Authentication
                        if (result.RequiresTwoFactor) {
                            //return RedirectToAction("LoginTwoStep", new { appUser.Email, login.ReturnUrl });
                            return RedirectToAction("LoginTwoStep", new { appUser.Email });
                        }

                        // Email confirmation 
                        bool emailStatus = await _userManager.IsEmailConfirmedAsync(appUser);
                        if (emailStatus == false) {
                            ModelState.AddModelError(nameof(login.Email), "Email is unconfirmed, please confirm it first");
                        }
                        // https://www.yogihosting.com/aspnet-core-identity-user-lockout/
                        /*if (result.IsLockedOut)
                            ModelState.AddModelError("", "Your account is locked out. Kindly wait for 10 minutes and try again");*/
                    }
                    ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password");
                }
                catch {
                    //var err = "Email is already existed";
                    //return RedirectToAction("Create", new {value = err});
                    Console.WriteLine("Error Email");
                }

            }
            return RedirectToAction("Login");
        }
        public ActionResult Recover_Password() {
            return View();
        }

        public async Task<ActionResult> UserDetail(Guid id) {
            var user = await _userService.GetById(id.ToString());
            if (user != null) {
                return View(model: user);
            }
            else {
                return View();
            }
        }
        public ActionResult Error() {
            return View();
        }

        public async Task<ActionResult> AdminProfile() {
            try {
                var user = await _userService.GetById(HttpContext.Session.GetString("user"));
                if (user != null) {
                    ViewBag.User = user;
                    return View();
                }
                else {
                    return RedirectToAction("Login");
                }
            }
            catch {
                return RedirectToAction("Login");

            }
        }
        public ActionResult Sales_Analytics() {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection) {
            try {
                return RedirectToAction("Index");
            }
            catch {
                return View();
            }
        }

        public async Task<IActionResult> UserAdd() {
            try {
                string getemail = HttpContext.Request.Form["useremail"];
                var checkEmail = await context.Users.SingleOrDefaultAsync(u => u.Email == getemail);
                if (checkEmail != null) {
                    return RedirectToAction("ErrAdd");
                }
                string password = HttpContext.Request.Form["userpassword"];
                var newUser = new User {
                    UserName = HttpContext.Request.Form["username"],
                    Email = HttpContext.Request.Form["useremail"],
                    PhoneNumber = HttpContext.Request.Form["mobilenumber"],
                    DoB = DateTime.Parse("01/01/2000"),
                    Gender = "F",
                    Address = "NaN",
                    isDeleted = false,
                    Name = "erererere",
                };

                IdentityResult re = await _userManager.CreateAsync(newUser, password);
                //IdentityResult re1 = await _userManager.AddToRoleAsync(newUser, "admin");
                //IdentityResult a = await _roleManager.CreateAsync(new IdentityUserRole<string>().RoleId = newUser.Id);
                await _userManager.AddToRoleAsync(newUser, "Administrator");
                return RedirectToAction("Index");
            }
            catch {
                Console.WriteLine("Error");
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }


        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, IFormCollection collection) {
            try {

                return View();
            }
            catch {
                return View();
            }
        }

        public async Task<IActionResult> ChangePass() {
            try {
                string userid = HttpContext.Request.Form["idhidden"];
                var user = await _userManager.FindByIdAsync(userid);
                string currentPass = HttpContext.Request.Form["oldpass"];
                string newPass = HttpContext.Request.Form["newpass"];

                string decodeHash = base64Decode(user.PasswordHash);

                string newHashPassword = _userManager.PasswordHasher.HashPassword(user, newPass);
                if (user == null) {
                    return BadRequest("User not found");
                }
                var result = await _userManager.ChangePasswordAsync(user, currentPass, newPass);
                if (!result.Succeeded) {
                    return BadRequest(result.Errors);
                }
                return RedirectToAction("Index");
            }
            catch {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public async Task<ActionResult> Delete(Guid id) {
            var user = await _userService.GetById(id.ToString());
            if (user != null) {
                var db = new FRMDbContext();
                user.isDeleted = true;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserList");
            }
            else {
                return RedirectToAction("UserList");
            }

        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) {
            try {
                var getUserbyId = users.FirstOrDefault(u => Int32.Parse(u.Id) == id);
                if (getUserbyId != null) {
                    using (var db = new FRMDbContext()) {
                        db.Users.Remove(getUserbyId);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("UserList");
            }
            catch {
                return View();
            }
        }


        public string hashPassword(string password) {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPass = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPass);
        }

        public static string base64Decode(string sData) //Decode
        {
            try {
                var encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception ex) {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }

    }
}
