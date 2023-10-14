using BAL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRN221_MVC.Models;
using System.Diagnostics;
using System.Net;
using System.Security;
using System.Security.Claims;

namespace PRN221_MVC.Controllers
{
    public class CartController : Controller
    {

        ICartService CartService;
        IUserService UserService;
        IProductService ProductService;

        public CartController(ICartService cartService, IUserService userService, IProductService productService)
        {
            this.CartService = cartService;
            this.UserService = userService;
            this.ProductService = productService;
        }

        public IActionResult Index()
        {
            string? email = HttpContext.Session.GetString("_Email");
            if (email != null)
            {
                User? User = UserService.GetUserByEmail(email);
                if (User != null)
                {
                    ViewData["carts"] = CartService.GetCartList(User);
                }
            }
            return View("/Views/Cart/Cart.cshtml");
        }

        [HttpPost]
        public JsonResult Add()
        {
            User? user = null;
            if (HttpContext.Session.GetString("_Email") == null)
            {
                return Json(new { Status = "Error", Message = "Please login again to continue." });
            }
            else
            {
                string email = HttpContext.Session.GetString("_Email")!;
                user = UserService.GetUserByEmail(email);
            }
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    var cartModel = JsonConvert.DeserializeObject<CartViewModel>(requestBody);
                    Product product = ProductService.GetProductById(cartModel.ProductID);
                    Cart cart = CartService.GetCartByProductAndUser(product, user);
                    if (cart != null)
                    {
                        CartService.UpdateQuantity(cart, cart.Quantity + cartModel.Quantity);
                    }
                    else
                    {
                        cart = new Cart();
                        cart.Quantity = cartModel.Quantity;
                        cart.User = user;
                        cart.Product = product;
                        CartService.AddItem(cart);
                    }
                    return Json(new { Status = "Success", Message = product.Name + " has been added to your cart" });
                }
            }
            return Json(new { Status = "Error", Message = "Something went wrong. Please try again" });
        }

        [HttpGet]
        public JsonResult InCart()
        {
            User? user = null;
            if (HttpContext.Session.GetString("_Email") == null)
            {
                return Json(new { Status = "Error", Message = "Please login again to continue." });
            }
            else
            {
                string email = HttpContext.Session.GetString("_Email")!;
                user = UserService.GetUserByEmail(email);
            }
            var carts = CartService.GetCartList(user!);
            return Json(carts);
        }

        [HttpPost]
        public ActionResult Delete()
        {
            User? user = null;
            if (HttpContext.Session.GetString("_Email") == null)
            {
                return Json(new { Status = "Error", Message = "Please login again to continue." });
            }
            else
            {
                string email = HttpContext.Session.GetString("_Email")!;
                user = UserService.GetUserByEmail(email);
            }
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    //TODO - to be updated
                    try
                    {
                        var cartModel = JsonConvert.DeserializeObject<CartViewModel>(requestBody);
                        if (cartModel != null)
                        {
                            Product product = ProductService.GetProductById(cartModel.ProductID);
                            if (product == null) { throw new Exception("Product not found. Please try again."); }
                            Cart cart = CartService.GetCartByProductAndUser(product, user);
                            if (cart != null)
                            {
                                CartService.DeleteItem(cart);
                                return Json(new { Status = "Success", Message = "Deleted successful" });
                            }
                            else
                            {
                                throw new Exception("Cart item not found.Please try again later");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        return Json(new { Status = "Error", e.Message });
                    }
                }
            }
            return Json(new { Status = "Error", Message = "Something went wrong. Please try again" });
        }

        [HttpPatch]
        public ActionResult Update()
        {
            User? user = null;
            if (HttpContext.Session.GetString("_Email") == null)
            {
                return Json(new { Status = "Error", Message = "Please login again to continue." });
            }
            else
            {
                string email = HttpContext.Session.GetString("_Email")!;
                user = UserService.GetUserByEmail(email);
            }
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
                        var cartModel = JsonConvert.DeserializeObject<CartViewModel>(requestBody);
                        if (cartModel != null)
                        {
                            Product product = ProductService.GetProductById(cartModel.ProductID);
                            if (product == null) { throw new Exception("Product not found. Please try again later"); }
                            Cart cart = CartService.GetCartByProductAndUser(product, user);
                            if (cart != null)
                            {
                                CartService.UpdateQuantity(cart, cartModel.Quantity);
                                return Json(new { Status = "Success", Message = "Updated successful" });
                            }
                            else
                            {
                                throw new Exception("Cart item not found. Please try again later.");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        return Json(new { Status = "Error", e.Message});
                    }
                }
            }
            return Json(new { Status = "Error", Message = "Something went wrong. Please try again" });
        }
    }
}
