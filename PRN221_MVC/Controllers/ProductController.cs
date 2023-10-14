using DAL;
using BAL.Model;
using PagedList;
using System.Data;
using DAL.Entities;
using BAL.Services.Implements;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace PRN221_MVC.Controllers {
    public class ProductController : Controller {
        readonly IProductService productService = new ProductService();
        private readonly FRMDbContext _dbContext;

        public ProductController(FRMDbContext dbContext) {
            _dbContext = dbContext;
        }

        // GET: ProductController
        public ActionResult Index() {
            return View();
        }

        public ActionResult Details(int id) {
            var productDetail = _dbContext.Product
                                            .Include(v => v.Comments)
                                            .FirstOrDefault(x => x.ID == id);
            if (productDetail == null) {
                return RedirectToAction("Index", "Home");
            }
            return View(productDetail);
        }

        public ActionResult DeleteComment() {
            return RedirectToAction("Details");
        }

        [BindProperty]
        public Product Product { get; set; }

        

        public ActionResult SearchProduct(string? search) {
            List<Product> items = new List<Product>();

            try {

                items = productService.Search(search.Trim());
                if (items.Count == 0) {
                    //ViewBag.Message = "Nothing Found";
                    //TempData["Message"] = ViewBag.Message;
                    //return View("/Views/Home/Index.cshtml");
                    ViewBag.Error = "No Item Found";
                }
                else {
                    ViewBag.Count = items.Count;
                    ViewBag.Search = items;
                }
            }
            catch {
                items = _dbContext.Product.ToList();
                ViewBag.Search = items;
                ViewBag.Count = items.Count;

                return View("Filter", items);
            }
            return View("Filter");
        }

        public ActionResult Filter(int sortId, int id, List<Product> list) {
            var products = productService.Filter(id);
            if (products.Count > 0) {
                ViewBag.Count = products.Count;
                ViewBag.Show = products;
            }
            else {
                ViewBag.Error = "No Item Found";
            }
            List<Product> sort = new List<Product>();
            List<Product> listProduct = _dbContext.Product.ToList();
            Category category = new Category();
            var cate = _dbContext.Category.FirstOrDefault(x => x.ID == id);
            if (id == 0) {
                return NotFound();
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
        [HttpGet]
        public IActionResult Sort(int sortId) {

            List<Product> sort = new List<Product>();
            //switch (sortId)
            //{

            //    case 1:
            //           sort =list.OrderBy(x=>x.Name).ToList();
            //        break;
            //    case 2:
            //           sort =list.OrderByDescending(x=>x.Name).ToList();

            //        break;
            //    case 3:
            //           sort =list.OrderBy(x=>x.Price).ToList();

            //        break;
            //    case 4:
            //           sort =list.OrderByDescending(x=>x.Price).ToList();

            //        break;
            //    default:
            //        sort = list.ToList();
            //        break;


            //}
            switch (sortId) {

                case 1:
                    sort = _dbContext.Product.OrderBy(x => x.Name).ToList();
                    ViewBag.TotalProduct = sort.Count();
                    break;
                case 2:
                    sort = _dbContext.Product.OrderByDescending(x => x.Name).ToList();
                    ViewBag.TotalProduct = sort.Count();
                    break;
                case 3:
                    sort = _dbContext.Product.OrderBy(x => x.Price).ToList();
                    ViewBag.TotalProduct = sort.Count();
                    break;
                case 4:
                    sort = _dbContext.Product.OrderByDescending(x => x.Price).ToList();
                    ViewBag.TotalProduct = sort.Count();
                    break;
                default:
                    sort = _dbContext.Product.ToList();
                    ViewBag.TotalProduct = sort.Count();
                    break;


            }
            ViewBag.Search = sort;

            return View("Filter", sort);
        }

        public ActionResult ProSort() {

            return View();
        }
    }
}
