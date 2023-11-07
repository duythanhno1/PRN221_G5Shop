using BAL.Services.Interface;
using DAL;
using DAL.Entities;
using DAL.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN221_MVC.Models;

namespace PRN221_MVC.Controllers {
    public class CommentController : Controller {
        private UserManager<User> userManager;
        private ICommentService commentService;

        FRMDbContext _dbContext = new FRMDbContext();
        public CommentController(UserManager<User> userMgr, ICommentService commentService) {
            userManager = userMgr;
            this.commentService = commentService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CommentViewModel comment) {
            string email = HttpContext.Session.GetString("_Email");
            if (email == null) {
                return RedirectToAction("Login", "User");
            }
            User user = await userManager.FindByEmailAsync(email);
            bool isInRoleCustomer = await userManager.IsInRoleAsync(user, "Customer");
            if (!isInRoleCustomer) {
                return View("/Views/Shared/Error401.cshtml", new ErrorViewModel { RequestId = "401" });
            }

            // only 1 time each product

            commentService.Create(new Comment {
                Content = comment.Content,
                isDeleted = false,
                UserId = user.Id,
                ProductId = comment.productId
            });
            return RedirectToAction("Details", "Product", comment.productId);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CommentViewModel comment) {
            string email = HttpContext.Session.GetString("_Email");
            if (email == null) {
                return RedirectToAction("Login", "User");
            }
            User user = await userManager.FindByEmailAsync(email);
            bool isInRoleCustomer = await userManager.IsInRoleAsync(user, "Customer");
            if (!isInRoleCustomer) {
                return View("/Views/Shared/Error401.cshtml", new ErrorViewModel { RequestId = "401" });
            }

            // only 1 time each product

            commentService.Update(new Comment {
                ID = comment.ID,
                Content = comment.Content,
                UserId = user.Id,
                isDeleted = false,
                ProductId = comment.productId
            });
            return Redirect("/Product/Details/" + comment.productId);
        }

        public ActionResult DeleteComment(Guid commentId, long productId)
        {
            var comment = _dbContext.Comment.FirstOrDefault(x => x.ID == commentId);
            if (comment != null)
            {
                var db = new FRMDbContext();
                comment.isDeleted = true;
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Product", new { id = productId });
            }
            else
            {
                return RedirectToAction("Details");
            }
        }
    }
}
