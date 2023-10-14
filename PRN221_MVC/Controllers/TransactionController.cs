using DAL.Entities;
using Newtonsoft.Json;
using PRN221_MVC.Models;
using BAL.Services.Interface;
using BAL.Services.Implements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DAL.Repositories.Interface;

namespace PRN221_MVC.Controllers
{
    public class TransactionController : Controller
    {
        private IUserService UserService;
        private ICartService CartService;
        private ITransactionService TransactionService;

        public TransactionController(IUserService userService, ICartService cartService, ITransactionService transactionService)
        {
            this.UserService = userService;
            this.CartService = cartService;
            this.TransactionService = transactionService;
        }
        // GET: TransactionController
        public ActionResult Index() {
            return View();
        }
        public IActionResult CheckOut()
        {
            string email = HttpContext.Session.GetString("_Email")!;
            User User = UserService.GetUserByEmail(email)!;
            Models.CheckoutViewModel.AccountData account = new Models.CheckoutViewModel.AccountData(User.Name, User.Email, User.Address);
            var orderDetails = CartService.GetCartList(User).ConvertAll((cart) => new Models.CheckoutViewModel.OrderDetail(cart.Product.ID,cart.Product.Name, cart.Quantity, cart.Product.Price));
            var checkoutModel = new Models.CheckoutViewModel(account, orderDetails);
            return View("/Views/Transaction/Checkout.cshtml", checkoutModel);
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    try
                    {
                        var model = JsonConvert.DeserializeObject<Models.CheckoutViewModel>(requestBody);
                        User user = UserService.GetUserByEmail(model.Account.Email);
                        if (user == null) throw new Exception("Empty email");
                        var carts = CartService.GetCartList(user);
                        if (carts == null || carts.Count == 0)
                        {
                            throw new Exception("Empty cart");
                        }
                        TransactionService.Save(user, model.Total, carts);
                        CartService.RemoveCartList(user);
                        return Json(new { Status = "Success", Message = "Your order are being processed and deliveried." });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return Json(new { Status = "Error", Message = e.Message });
                    }
                }
            }
            return Json(new { Status = "Error", Message = "Something went wrong. Please try again" });
        }
        // GET: TransactionController/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: TransactionController/Create
        public ActionResult Create() {
            return View();
        }

        // POST: TransactionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: TransactionController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: TransactionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: TransactionController/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: TransactionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
    }
}
