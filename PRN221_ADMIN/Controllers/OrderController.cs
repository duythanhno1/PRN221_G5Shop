
using DAL;
using DAL.Entities;
using DAL.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PRN221_MVC.Controllers {
    public class OrderController : Controller {
        private readonly IOrdersService _ordersService;
        private UserManager<User> _userManager;

        FRMDbContext context = new FRMDbContext();

        public OrderController(IOrdersService ordersService, UserManager<User> userManager) {
            _ordersService = ordersService;
            _userManager = userManager;
        }
        // GET: OrderController
        public ActionResult Index() {
            return View();
        }
        public IActionResult WishList() {
            var session = HttpContext.Session;

            // Retrieve the wishlist from session state
            var wishlistJson = session.GetString("Wishlist");
            var listProduct = !string.IsNullOrEmpty(wishlistJson) ? JsonConvert.DeserializeObject<List<Product>>(wishlistJson) : new List<Product>();

            // Pass the wishlist to the view
            return View(listProduct);
        }
        [HttpPost]
        public IActionResult RemoveFromWishList(int id) {
            // Retrieve the current wishlist from session state
            var wishlistJson = HttpContext.Session.GetString("Wishlist");
            var wishlist = !string.IsNullOrEmpty(wishlistJson) ? JsonConvert.DeserializeObject<List<Product>>(wishlistJson) : new List<Product>();

            // Remove the product with the specified ID from the wishlist
            var productToRemove = wishlist.FirstOrDefault(p => p.ID == id);
            if (productToRemove != null) {
                wishlist.Remove(productToRemove);

                // Store the updated wishlist back in session state
                HttpContext.Session.SetString("Wishlist", JsonConvert.SerializeObject(wishlist));
            }

            // Redirect back to the wishlist page
            return RedirectToAction("WishList");
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create() {
            return View();
        }

        public async Task<ActionResult> OrderList(string id) {
            var user = context.Users.FirstOrDefault(x => x.Email == id);
            var getList = _ordersService.GetOrdersById(Guid.Parse(user.Id));
            if (getList != null) {
                return View(model: getList);
            }
            else {
                return View();
            }
        }

        // POST: OrderController/Create
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

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: OrderController/Edit/5
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

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: OrderController/Delete/5
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

