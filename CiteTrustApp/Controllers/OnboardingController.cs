using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using CiteTrustApp.Models;
using System.Web;

namespace CiteTrustApp.Controllers
{
    [Authorize]
    public class OnboardingController : Controller
    {
        private readonly AcademicEvidenceModel _db = new AcademicEvidenceModel();

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set { _userManager = value; }
        }

        // GET: /Onboarding
        public ActionResult Index()
        {
            // load categories to show as interests
            var categories = _db.Categories
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() })
                .ToList();

            ViewBag.Categories = categories;
            return View();
        }

        // POST: /Onboarding
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(int[] selectedCategories)
        {
            var userId = User.Identity.GetUserId();
            if (userId == null) return new HttpUnauthorizedResult();

            var user = await UserManager.FindByIdAsync(userId);
            if (user == null) return HttpNotFound();

            // store choices as comma-separated category ids
            if (selectedCategories != null && selectedCategories.Length > 0)
            {
                user.InterestCategoryIds = string.Join(",", selectedCategories);
            }
            else
            {
                user.InterestCategoryIds = null;
            }

            user.IsOnboarded = true;
            var result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to save preferences. Please try again.");
                // reload categories
                ViewBag.Categories = _db.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() })
                    .ToList();
                return View();
            }

            // redirect to home (or returnUrl)
            return RedirectToAction("Index", "Evidence");
        }
    }
}