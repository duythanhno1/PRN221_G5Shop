using DAL;
using BAL.Model;
using DAL.Entities;
using PRN221_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using DAL.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PRN221_MVC.Controllers {
    public class ShopOwnerController : Controller {
        private UserManager<User> _userManager;
        private readonly IOrdersService ordersService;
        FRMDbContext context=new FRMDbContext();
        public ShopOwnerController(IOrdersService ordersService, UserManager<User> userManager) {
            this.ordersService = ordersService;
            _userManager = userManager;
        }

        // GET: ShopOwnerController
        public async Task<ActionResult> IndexShopOwner(int year) {
            string email = HttpContext.Session.GetString("Email");
            if (email == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            User user = await _userManager.FindByEmailAsync(email);
            bool isInRoleCustomer = await _userManager.IsInRoleAsync(user, "ShopOwner");
            if (!isInRoleCustomer)
            {
                return RedirectToAction("Login", "Admin");
            }
            var totalToday = ordersService.GetTotalOrderToday();
            var totalWeek = ordersService.GetTotalOrdersWeek();
            var total30Days = ordersService.GetTotalOrderLastThirtyDays();
            var countOrders30Days = ordersService.CountOrderLastThirtyDays();
            var monthLySalesData = ordersService.GetMonthlySalesData(year: year);
            var topSellingProductByMonth = ordersService.GetTopSellingProductsByMonth();
            var topSellingProductByWeek = ordersService.GetTopSellingProductsByWeek();
            var orderValuesInEachMonth = ordersService.GetOrderValuesInEachMonth();
            var salesDataInEachMonth = ordersService.GetSalesDataMonthly(year: year);

            foreach (var item in orderValuesInEachMonth) {
                Console.WriteLine("Month | Total Amount | Total Quantity");

                Console.WriteLine(item.Key);
                Console.WriteLine(item.Value);
            }


            ViewBag.TotalSalesToday = totalToday;

            ViewBag.TotalSalesWeek = totalWeek;

            ViewBag.Total30Days = total30Days;

            ViewBag.CountOrders30Days = countOrders30Days;

            ViewBag.TopSellingProductByMonth = topSellingProductByMonth;

            ViewBag.TopSellingProductByWeek = topSellingProductByWeek;

            ViewBag.MonthLySalesData = monthLySalesData;

            ViewBag.OrderValuesInEachMonth = orderValuesInEachMonth;

            ViewBag.SalesDataInEachMonth = salesDataInEachMonth;

            return View();

        }

        [HttpGet]

        public async Task<ActionResult> ProductList()
        {
            string email = HttpContext.Session.GetString("Email");
            if (email == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            User user = await _userManager.FindByEmailAsync(email);
            bool isInRoleCustomer = await _userManager.IsInRoleAsync(user, "ShopOwner");
            if (!isInRoleCustomer)
            {
                return RedirectToAction("Login", "Admin");
            }

            var productList = await context.Product.Include(x => x.Category).ToListAsync();
            return View(productList);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            string email = HttpContext.Session.GetString("Email");
            if (email == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            User user = await _userManager.FindByEmailAsync(email);
            bool isInRoleCustomer = await _userManager.IsInRoleAsync(user, "ShopOwner");
            if (!isInRoleCustomer)
            {
                return RedirectToAction("Login", "Admin");
            }
            var category = await context.Category.ToListAsync();
            ViewBag.ErrorMessage = "A product with this name already exists.";
            ViewBag.Category = category;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductModel product)
        {
            string email = HttpContext.Session.GetString("Email");
            if (email == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            User user = await _userManager.FindByEmailAsync(email);
            bool isInRoleCustomer = await _userManager.IsInRoleAsync(user, "ShopOwner");
            if (!isInRoleCustomer)
            {
                return RedirectToAction("Login","Admin");
            }

            ViewBag.Category = context.Category.ToList();

            if (context.Product.Any(p => p.Name == product.Name))
            {
                ModelState.AddModelError("Name", "A product with this name already exists.");
                ViewBag.ErrorMessage = "A product with this name already exists.";
                return View();
            }
            if (ModelState.IsValid)
            {

                Category category = context.Category.FirstOrDefault(x => x.ID == product.CategoryID);
                var products = new Product()
                {
                    Name = product.Name,
                    Price = product.Price,
                    imgPath = "~/client/image/cache/catalog/product/" + product.imgPath,
                    isAvailable = product.isAvailable,
                    isDeleted = product.isDeleted,
                    Category = category,

                };
                await context.Product.AddAsync(products);
                await context.SaveChangesAsync();
                return RedirectToAction("CreateProduct");
            }
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> ProductDetail(int id)
        {
            string email = HttpContext.Session.GetString("Email");
            if (email == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            User user = await _userManager.FindByEmailAsync(email);
            bool isInRoleCustomer = await _userManager.IsInRoleAsync(user, "ShopOwner");
            if (!isInRoleCustomer)
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.CategorySelective = context.Category.ToList();

            var productDetail = await context.Product.Include(x => x.Category).FirstOrDefaultAsync(x => x.ID == id);
            return View(productDetail);
        }


        [HttpPost]
        public async Task<IActionResult> ProductDetail(Product product)
        {
            string email = HttpContext.Session.GetString("Email");
            if (email == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            User user = await _userManager.FindByEmailAsync(email);
            bool isInRoleCustomer = await _userManager.IsInRoleAsync(user, "ShopOwner");
            if (!isInRoleCustomer)
            {
                return RedirectToAction("Login", "Admin");
            }

            ViewBag.CategorySelective = context.Category.ToList();

            var productDetail = await context.Product.FindAsync(product.ID);
            if (productDetail != null)
            {
                productDetail.Name = product.Name;
                productDetail.Price = product.Price;
                if (product.imgPath == null)
                {
                    productDetail.imgPath = productDetail.imgPath;
                }else
                {
                    productDetail.imgPath = "~/client/image/cache/catalog/product/" + product.imgPath;
                }
                productDetail.isDeleted = product.isDeleted;
                productDetail.isAvailable = product.isAvailable;
                var category = await context.Category.FindAsync(product.Category.ID);
                if (category != null)
                {
                    productDetail.Category = category;
                }
                await context.SaveChangesAsync();
            }
            return RedirectToAction("ProductList");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Product product)
        {
            string email = HttpContext.Session.GetString("Email");
            if (email == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            User user = await _userManager.FindByEmailAsync(email);
            bool isInRoleCustomer = await _userManager.IsInRoleAsync(user, "ShopOwner");
            if (!isInRoleCustomer)
            {
                return RedirectToAction("Login", "Admin");
            }
            var productdetail = await context.Product.FindAsync(product.ID);
            if (productdetail != null)
            {
                productdetail.isDeleted = true;
                context.Product.Update(productdetail);
                await context.SaveChangesAsync();
                return RedirectToAction("ProductList");
            }
            else { return RedirectToAction("ProductList"); }
        }



        public ActionResult LogOutShopOwner() {
            HttpContext.Session.Remove("Email");
            return RedirectToAction("Login", "Admin");
        }

        public async Task<ActionResult> ShopOwnerProfile() {
            string email = HttpContext.Session.GetString("Email");
            if (email == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            User user = await _userManager.FindByEmailAsync(email);
            bool isInRoleCustomer = await _userManager.IsInRoleAsync(user, "ShopOwner");
            if (!isInRoleCustomer)
            {
                return RedirectToAction("Login", "Admin");
            }
            User appUser = await _userManager.FindByEmailAsync(email);
            Console.WriteLine(appUser.Email);
            ViewBag.User = appUser;
            return View();
        }

        public async Task<ActionResult> OrderHistory() {

            string email = HttpContext.Session.GetString("Email");
            if (email == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            User user = await _userManager.FindByEmailAsync(email);
            bool isInRoleCustomer = await _userManager.IsInRoleAsync(user, "ShopOwner");
            if (!isInRoleCustomer)
            {
                return RedirectToAction("Login", "Admin");
            }
            var orders = ordersService.GetOrders();


            ViewBag.Orders = orders;

            return View();
        }

        public async Task<ActionResult> OrderDetails(Guid id) {
            string email = HttpContext.Session.GetString("Email");
            if (email == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            User user = await _userManager.FindByEmailAsync(email);
            bool isInRoleCustomer = await _userManager.IsInRoleAsync(user, "ShopOwner");
            if (!isInRoleCustomer)
            {
                return RedirectToAction("Login", "Admin");
            }
            var orderDetails = ordersService.GetOrderDetailsByOrderId(id);
            var order = ordersService.GetOrderById(id);
            float subtotals = 0;
            foreach (var item in orderDetails) {
                subtotals += +item.Product.Price * item.Quantity;
            }

            ViewBag.Order = order;
            ViewBag.OrderDetails = orderDetails;
            ViewBag.SubTotals = subtotals;

            return View();
        }

        // GET: ShopOwnerController/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: ShopOwnerController/Create
        public ActionResult Create() {
            return View();
        }

        // POST: ShopOwnerController/Create
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

        // GET: ShopOwnerController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: ShopOwnerController/Edit/5
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

        // GET: ShopOwnerController/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: ShopOwnerController/Delete/5
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
