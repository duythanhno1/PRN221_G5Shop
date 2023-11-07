using BAL.Model;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PRN221_MVC.Models;
using System.Diagnostics;

namespace PRN221_MVC.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly FRMDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, FRMDbContext _dbContext) {
            this._dbContext = _dbContext;
            _logger = logger;
        }

        public IActionResult Index() {
            CateAndProductViewModel model = new CateAndProductViewModel();
            model.Products = _dbContext.Product.ToList();
            model.Categories = _dbContext.Category.ToList();
            string name = HttpContext.Session.GetString("_Name");
            string email = HttpContext.Session.GetString("_Email");
            ViewBag.ProductList = _dbContext.Product.ToList();
            ViewData["_Name"] = name;
            ViewData["_Email"] = email;
            return View(model);
        }
        public IActionResult Register() {
            return View();
        }
        public IActionResult ForgotPassword() {
            return View();
        }
        public IActionResult LoginClient() {
            return View();
        }
        [HttpPost]
        public IActionResult AddToWishList(int id) {

            var product = _dbContext.Product.Include(x => x.Category).FirstOrDefault(x => x.ID == id);
            if (product == null) {
                return NotFound();
            }

            // Get the existing wishlist from the session, or create a new one if it doesn't exist
            List<Product> wishlist;
            var wishlistJson = HttpContext.Session.GetString("Wishlist");
            if (wishlistJson == null) {
                wishlist = new List<Product>();
            }
            else {
                wishlist = JsonConvert.DeserializeObject<List<Product>>(wishlistJson);
            }

            // Check if the product is already in the wishlist
            if (wishlist.Any(p => p.ID == product.ID)) {
                return RedirectToAction("Category", new { id = product.Category.ID });
            }

            // Add the product to the wishlist
            var wishlistProduct = new Product {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                imgPath = product.imgPath,
                Category = product.Category,
                // Add any other product properties you need
            };
            wishlist.Add(wishlistProduct);

            // Save the updated wishlist to the session
            HttpContext.Session.SetString("Wishlist", JsonConvert.SerializeObject(wishlist));

            return RedirectToAction("Category", new { id = product.Category.ID });
        }


        [HttpGet]
        public IActionResult Category(int sortId, int id) {
            List<Product> sort = new List<Product>();
            Category category = new Category();

            if (id == 0) {
                return RedirectToAction("Index");
            }
            switch (sortId) {

                case 1:
                    sort = _dbContext.Product.Include(x => x.Category).Where(x => x.Category.ID == id).OrderBy(x => x.Name).ToList();
                    ViewBag.TotalProduct = sort.Count();
                    break;
                case 2:
                    sort = _dbContext.Product.Include(x => x.Category).Where(x => x.Category.ID == id).OrderByDescending(x => x.Name).ToList();
                    ViewBag.TotalProduct = sort.Count();
                    break;
                case 3:
                    sort = _dbContext.Product.Include(x => x.Category).Where(x => x.Category.ID == id).OrderBy(x => x.Price).ToList();
                    ViewBag.TotalProduct = sort.Count();
                    break;
                case 4:
                    sort = _dbContext.Product.Include(x => x.Category).Where(x => x.Category.ID == id).OrderByDescending(x => x.Price).ToList();
                    ViewBag.TotalProduct = sort.Count();
                    break;
                default:
                    sort = _dbContext.Product.Include(x => x.Category).Where(x => x.Category.ID == id).ToList();
                    category = _dbContext.Category.FirstOrDefault(x => x.ID == id);
                    foreach (var item in sort) {
                        item.Category = category;
                    }
                    ViewBag.TotalProduct = sort.Count();
                    break;


            }


            return View(sort);
        }

        public IActionResult Privacy() {
            return View();
        }
        public IActionResult Details(int id) {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
