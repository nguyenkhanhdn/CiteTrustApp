using CiteTrustApp.Models;
using CiteTrustApp.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CiteTrustApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AcademicEvidenceModel _db = new AcademicEvidenceModel();
        private readonly CTSDbContext db = new CTSDbContext();
        public ActionResult Index()
        {
            // If user authenticated and has interests, show simple suggestions
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var appUser = userManager?.FindById(userId);
                if (appUser != null && !string.IsNullOrWhiteSpace(appUser.InterestCategoryIds))
                {
                    var catIds = appUser.InterestCategoryIds
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => {
                            int v;
                            return int.TryParse(s, out v) ? (int?)v : null;
                        })
                        .Where(i => i.HasValue)
                        .Select(i => i.Value)
                        .ToList();

                    if (catIds.Any())
                    {
                        var suggestions = _db.Evidences
                            .Where(e => e.CategoryId.HasValue && catIds.Contains(e.CategoryId.Value))
                            .OrderByDescending(e => e.CreatedAt)
                            .Take(5)
                            .ToList() // Materialize the query before using null-propagating operator
                            .Select(e => new {
                                Title = e.Source != null ? e.Source.Title : (e.Content ?? "").Substring(0, Math.Min(120, (e.Content ?? "").Length)),
                                Snippet = ((e.Content ?? (e.Source != null ? e.Source.Title : "")) ?? "").Length > 200
                                    ? (((e.Content ?? (e.Source != null ? e.Source.Title : "")) ?? "").Substring(0, 200) + "...")
                                    : ((e.Content ?? (e.Source != null ? e.Source.Title : "")) ?? "")
                            })
                            .ToList();

                        ViewBag.Suggestions = suggestions;
                    }
                }
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TrainModel()
        {
            MLRecommendationService ml = new MLRecommendationService(db,null);
            ml.TrainMatrixFactorization();
            ViewBag.Message = "Training model ...";

            return View();
        }
    }
}